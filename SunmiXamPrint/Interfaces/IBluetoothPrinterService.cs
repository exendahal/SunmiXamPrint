using SunmiXamPrint.Model;
using System.Collections.Generic;

namespace SunmiXamPrint.Interfaces
{
    public interface IBluetoothPrinterService
    {
        BluetoothDeviceInfo GetCurrentDevice();
        List<BluetoothDeviceInfo> GetAvailableDevices();
        bool SetCurrentDevice(string printerName);
        void PrintText(string content);
        void PrintQR(string content);
        bool IsBluetoothEnabled();

    }
}
