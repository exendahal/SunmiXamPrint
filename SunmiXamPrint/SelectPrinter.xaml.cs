using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using SunmiXamPrint.Interfaces;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SunmiXamPrint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPrinter : ContentPage
    {
        public SelectPrinter()
        {
            InitializeComponent();
        }

        private async void selectPrinterButton_Clicked(object sender, EventArgs e)
        {
            if (DeviceInfo.Platform.ToString() == "Android" && DeviceInfo.Model.ToString() == "V2")
            {
                if (DependencyService.Get<IBluetoothPrinterService>().IsBluetoothEnabled())
                {
                    var devices = DependencyService.Get<IBluetoothPrinterService>().GetAvailableDevices();
                    if (devices != null && devices.Count > 0)
                    {
                        var choices = devices.Select(d => d.Title).ToArray();
                        string action = await Application.Current.MainPage.DisplayActionSheet("Select printer device.", "Cancel", null, choices);
                        if (choices.Contains(action))
                        {
                            SelectDevice(action);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Select Printer", "No device.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("No bluetooth", "Please turn bluetooth on", "OK");
                }
            }


        }

        private void printQrButton_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IBluetoothPrinterService>().PrintQR(printBox.Text);
        }

        private void printTextButton_Clicked(object sender, EventArgs args)
        {
            var e = new EPSON();
            var buffer = ByteSplicer.Combine(
                e.CenterAlign(),
                e.SetStyles(PrintStyle.Bold),
                e.PrintLine(printBox.Text)                
                );

            DependencyService.Get<IBluetoothPrinterService>().PrintText(buffer);
        }

        void SelectDevice(string printerName)
        {
            if (DependencyService.Get<IBluetoothPrinterService>().SetCurrentDevice(printerName))
            {
                var current = DependencyService.Get<IBluetoothPrinterService>().GetCurrentDevice();
                if (current != null)
                {
                    printerNameEntry.Text = current.Title;
                    printQrButton.IsEnabled = printTextButton.IsEnabled = true;
                }
            }
        }
    }
}