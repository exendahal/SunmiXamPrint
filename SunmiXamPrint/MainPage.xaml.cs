using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using SunmiXamPrint.Interfaces;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SunmiXamPrint
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }     

        private void printQrButton_Clicked(object sender, EventArgs e)
        {
            byte[] qrBytes = System.Text.Encoding.ASCII.GetBytes("This is QR");
            int dataLength = qrBytes.Length + 3;
            byte dataPL = (byte)(dataLength % 256);
            byte dataPH = (byte)(dataLength / 256);
            var bytes = new List<byte>();

            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x04, 0x00, 0x31, 0x41, 0x32, 0x00 }); // Select model
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, 0x08 });  // Set module size (8)
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45, 0x30 });  // Set error correction
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, dataPL, dataPH, 0x31, 0x50, 0x30 }); // Start store qr data.
            bytes.AddRange(qrBytes);
            bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x51, 0x30 }); // Print qr data from previous 80 code.
            DependencyService.Get<IBluetoothPrinterService>().PrintQR(bytes);
        }

        private void printTextButton_Clicked(object sender, EventArgs args)
        {
            var e = new EPSON();
            var buffer = ByteSplicer.Combine(
                e.CenterAlign(),
                e.SetStyles(PrintStyle.Bold),
                e.PrintLine("Bio Care Premium"),
                e.SetStyles(PrintStyle.None),
                e.CenterAlign(),
                e.PrintLine("Address : Sankhamul,Kathmandu"),
                e.PrintLine("Mobile: 9800000000"),
                e.PrintLine("................................."),
                e.SetStyles(PrintStyle.Bold),
                e.PrintLine("S.N  Description       Qty.  Rs."),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(".................................")
                );
            DependencyService.Get<IBluetoothPrinterService>().Print(buffer);           
        }

        private void printBarCode_Clicked(object sender, EventArgs args)
        {
            var e = new EPSON();
            var buffer = ByteSplicer.Combine(
                e.CenterAlign(),               
                e.SetBarcodeHeightInDots(48), 
                e.PrintBarcode(BarcodeType.CODE39, "ABC"), 
                e.PrintLine("")                
                );
            DependencyService.Get<IBluetoothPrinterService>().Print(buffer);
        }

        private async void selectPrinter_Clicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SelectPrinter());
        }

    }
}
