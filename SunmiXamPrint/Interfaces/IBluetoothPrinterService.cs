using SunmiXamPrint.Model;
using System.Collections.Generic;
using static SunmiXamPrint.Model.ContentType;

namespace SunmiXamPrint.Interfaces
{
    public interface IBluetoothPrinterService
    {
        BluetoothDeviceInfo GetCurrentDevice();
        List<BluetoothDeviceInfo> GetAvailableDevices();
        bool SetCurrentDevice(string printerName);
        void PrintText(string content, TextContentType type);
        void PrintQR(string content);
        bool IsBluetoothEnabled();

    }
}
