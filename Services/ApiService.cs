using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Truckonline_Demo_Widget.Models;

namespace Truckonline_Demo_Widget.Services
{
    class ApiService
    {

        public FleetDetails Fleet()
        {
            Debug.WriteLine("Get fleet");
            string service = "/apis/rest/v2.2/fleet";

            string url = Settings.EndPoint + service;
            HttpResponseMessage message = DoRequest(url, Type.GET, null);
            Debug.WriteLine(message);
            if (message != null && message.StatusCode == HttpStatusCode.OK)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<FleetDetails>(json);
            }

            return null;
        }

        public ImpersonationResponseInfo Impersonate(string login)
        {
            Debug.WriteLine("Impersonate");
            string service = "/apis/rest/v2.2/access_token/impersonate/" + login;

            string url = Settings.EndPoint + service;
            HttpResponseMessage message = DoRequest(url, Type.GET, null);
            Debug.WriteLine(message);
            if (message != null && message.StatusCode == HttpStatusCode.OK)
            {
                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<ImpersonationResponseInfo>(json);
            }

            return null;
        }

        private HttpResponseMessage DoRequest(string uri, Type type, HttpContent content)
        {
            using (HttpClient client = new HttpClient())
            {
                this.SignRequest(client, uri, type);

                try
                {
                    switch (type)
                    {
                        case Type.GET:
                            return client.GetAsync(new Uri(uri)).Result;
                        case Type.POST:
                            return client.PostAsync(new Uri(uri), content).Result;
                        case Type.PUT:
                            return client.PutAsync(new Uri(uri), content).Result;
                        case Type.DELETE:
                            return client.DeleteAsync(new Uri(uri)).Result;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }

            return null;
        }

        private HttpClient SignRequest(HttpClient client, string uri, Type type)
        {
            String timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

            String method;
            switch (type)
            {
                case Type.POST:
                    method = "POST";
                    break;
                case Type.PUT:
                    method = "PUT";
                    break;
                case Type.DELETE:
                    method = "DELETE";
                    break;
                case Type.GET:
                default:
                    method = "GET";
                    break;
            }

            String baseString = createBaseString(Settings.Id, uri, method, timestamp);
            String hash = computeHmacSha1(baseString, Settings.Key);

            client.DefaultRequestHeaders.Add("x-tonl-timestamp", timestamp);
            client.DefaultRequestHeaders.Add("x-tonl-client-id", Settings.Id);
            client.DefaultRequestHeaders.Add("x-tonl-signature", hash);

            return client;
        }

        private static byte[] getBytes(string str)
        {
            Encoding enc = Encoding.UTF8;
            return enc.GetBytes(str);
        }
        private static String createBaseString(String id, String url, String method, String timestamp)
        {
            Uri uri = new Uri(url);
            return id + uri.Authority + method + uri.PathAndQuery + timestamp;
        }
        private static String getBase64StringFromByteArray(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        private static String computeHmacSha1(String baseString, String privateKey)
        {
            HMACSHA1 hmac = new HMACSHA1(getBytes(privateKey));
            hmac.Initialize();
            return getBase64StringFromByteArray(hmac.ComputeHash(getBytes(baseString)));
        }

        enum Type
        {
            POST,
            GET,
            PUT,
            DELETE
        }
    }
}
