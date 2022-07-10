using SunmiXamPrint.Model;
using System.Collections.Generic;

namespace SunmiXamPrint.Interfaces
{
    public interface IBluetoothPrinterService
    {
        BluetoothDeviceInfo GetCurrentDevice();
        List<BluetoothDeviceInfo> GetAvailableDevices();
        bool SetCurrentDevice(string printerName);
        void PrintText(byte[] data);
        void Print(byte[] data);
        void PrintQR(List<byte> bytes);
        void PrintQR(string content);
        bool IsBluetoothEnabled();

    }
}
