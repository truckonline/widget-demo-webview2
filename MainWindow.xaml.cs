using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Truckonline_Demo_Widget.Services;
using Truckonline_Demo_Widget.Models;

namespace Truckonline_Demo_Widget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApiService api;
        private WebBridgeService bridge;

        public MainWindow()
        {
            InitializeComponent();

            this.api = new ApiService();
            this.bridge = new WebBridgeService();
            this.bridge.Register(webView1);

            this.LoadData();
            datePicker.SelectedDate = DateTime.Today;
        }

        private void LoadData()
        {
            Debug.WriteLine("Loading Api Data");

            FleetDetails fleet = this.api.Fleet();

            comboDrivers.Items.Clear();
            var selected = true;
            foreach (var driver in fleet.drivers)
            {
                if (!driver.driverEnabled)
                    continue;
                comboDrivers.Items.Add(
                    new ComboBoxItem { Content = driver.driverLastName + " " + driver.driverFirstName + " - " + driver.driverUid, Tag = driver, IsSelected = selected }
                );
                selected = false;
            }
        }

        private async void DriverSelected(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Driver selection");
            string result = await this.bridge.SelectDriver((this.comboDrivers.SelectedItem as ComboBoxItem).Tag as DriverStatusDetails);
            Debug.WriteLine(result);
        }

        private async void DateSelected(object sender, SelectionChangedEventArgs e)
        {
            if (this.datePicker.SelectedDate.HasValue)
            {
                Debug.WriteLine("Date selection");
                string result = await this.bridge.SelectDate(this.datePicker.SelectedDate.Value, (this.comboPeriod.SelectedItem as ComboBoxItem).Tag as string);
                Debug.WriteLine(result);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.login.Text != null && this.login.Text.Length > 0)
            {
                var impersonateResponse = this.api.Impersonate(this.login.Text);

                var parameters = new Dictionary<string, string>();

                parameters["disable"] = "nav,stv";
                parameters["cols"] = "TIC,ACC,TTD,TTW,TAT,TTS,TSA,KM,EL";
                parameters["read_only"] = "1";
                parameters["driver"] = ((this.comboDrivers.SelectedItem as ComboBoxItem).Tag as DriverStatusDetails).driverUid;
                parameters["time_period"] = (this.comboPeriod.SelectedItem as ComboBoxItem).Tag as string;
                parameters["date"] = this.datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
                //parameters["map_options"] = "HOV";
                parameters["authorization_code"] = impersonateResponse.authorizationCode;

                //var finalUrl = new Uri(Settings.EndPoint + "/frontend/fr/map?" + string.Join("&", parameters.Select(x => x.Key + "=" + x.Value)));
                var finalUrl = new Uri(Settings.EndPoint + "/frontend/fr/activities?" + string.Join("&", parameters.Select(x => x.Key + "=" + x.Value)));

                Debug.WriteLine(finalUrl);

                this.webView1.Source = finalUrl;
            }
        }
    }
}
