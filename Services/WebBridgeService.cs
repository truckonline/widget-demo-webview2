using Microsoft.Toolkit.Wpf.UI.Controls;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truckonline_Demo_Widget.Models;

namespace Truckonline_Demo_Widget.Services
{
    class WebBridgeService
    {
        private WebView2 webView;

        public void Register(WebView2 webView)
        {
            this.webView = webView;
        }

        //public async Task<string> SelectOperation(OperationInfo operation)
        //{
        //    return await this.PostMessage("{\"action\": \"operation-info-event-select\", \"operation\": " + JsonConvert.SerializeObject(operation) + "}");
        //}

        public async Task<string> SelectDriver(DriverStatusDetails driver)
        {
            return await this.PostMessage("{\"action\": \"driver-info-event-select\", \"driver\": " + JsonConvert.SerializeObject(driver) + "}");
        }

        //public async Task<string> SelectVehicle(VehicleStatusDetails vehicle)
        //{
        //    return await this.PostMessage("{\"action\": \"vehicle-info-event-select\", \"vehicle\": " + JsonConvert.SerializeObject(vehicle) + "}");
        //}

        //public async Task<string> SelectActivity(ActivityInfo activity)
        //{
        //    return await this.PostMessage("{\"action\": \"group-info-event-select\", \"group\": " + JsonConvert.SerializeObject(activity) + "}");
        //}

        public async Task<string> SelectDate(DateTime datetime, string period)
        {
            return await this.PostMessage("{\"action\": \"date-event-select\", \"date\": \"" + datetime.ToString("yyyy-MM-dd") + "\", \"time_period\": \"" + period + "\"}");
        }


        private async Task<string> PostMessage(string message)
        {
            Debug.WriteLine("New postMessage : ");
            Debug.WriteLine(message);
            try
            {
                return await this.webView.ExecuteScriptAsync("postMessage(" + message +", \"*\")");
            }
            catch(Exception e)
            {
                Debug.WriteLine("Exception while postMessage");
                Debug.WriteLine(e);
            }

            return null;
        }

    }
}
