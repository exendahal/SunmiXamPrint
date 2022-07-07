using Android.Bluetooth;
using Android.Content;
using SunmiXamPrint.Droid;
using SunmiXamPrint.Interfaces;
using SunmiXamPrint.Model;
using System;
using System.Collections.Generic;

[assembly: Xamarin.Forms.Dependency(typeof(PrinterService))]
namespace SunmiXamPrint.Droid
{
    public class PrinterService : IBluetoothPrinterService
    {
        Context currentContext = Android.App.Application.Context;
        private BluetoothDevice _connectedDevice;
        public PrinterService()
        {
        }

        public List<BluetoothDeviceInfo> GetAvailableDevices()
        {
            BluetoothManager bluetoothManager = (BluetoothManager)currentContext.GetSystemService(Context.BluetoothService);
            if (bluetoothManager.Adapter != null && bluetoothManager.Adapter.IsEnabled)
            {
                            List<BluetoothDeviceInfo> result = new List<BluetoothDeviceInfo>();
                            foreach (var pairedDevice in bluetoothManager.Adapter.BondedDevices)
                            {
                                result.Add(new BluetoothDeviceInfo
                                {
                                    Title = pairedDevice.Name,
                                    MacAddress = pairedDevice.Address
                                });
                            }
                return result;
            }
            return null;
        }

        public BluetoothDeviceInfo GetCurrentDevice()
        {
            if (_connectedDevice != null)
            {
                return new BluetoothDeviceInfo
                {
                    Title = _connectedDevice.Name,
                    MacAddress = _connectedDevice.Address
                };
            }
            return null;
        }

        public bool SetCurrentDevice(string printerName)
        {
            BluetoothManager bluetoothManager = (BluetoothManager)currentContext.GetSystemService(Context.BluetoothService);

            if (bluetoothManager.Adapter != null && bluetoothManager.Adapter.IsEnabled)
            {
                foreach (var pairedDevice in bluetoothManager.Adapter.BondedDevices)
                {
                    if (pairedDevice.Name == printerName)
                    {
                        _connectedDevice = pairedDevice;
                        return true;
                    }
                }
            }
            return false;
        }
        public void PrintQR(string content)
        {
            SendCommandToPrinter("qr", content, _connectedDevice);
        }

        public void PrintText(string content)
        {
            SendCommandToPrinter("plain", content, _connectedDevice);
        }
        async void SendCommandToPrinter(string type, string content, BluetoothDevice device)
        {
            if (string.IsNullOrEmpty(content)) return;
            Printer print = new Printer();
            if (device != null)
            {
                await print.Print(type, content, device);
            }
            else
            {
                throw new Exception("No selected device.");
            }
        }
        public bool IsBluetoothEnabled()
        {
            BluetoothManager bluetoothManager = (BluetoothManager)currentContext.GetSystemService(Context.BluetoothService);
            if (bluetoothManager.Adapter != null && bluetoothManager.Adapter.IsEnabled)
            {
                return true;
            }
            return false;
        }
    }
}