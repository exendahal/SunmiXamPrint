

using Com.Sunmi.Peripheral.Printer;

namespace SunmiXamPrint.Droid
{
    public class SunmiPrintHelper
    {
        public static int NoSunmiPrinter = 0x00000000;
        public static int CheckSunmiPrinter = 0x00000001;
        public static int FoundSunmiPrinter = 0x00000002;
        public static int LostSunmiPrinter = 0x00000003;

        /**
     *  sunmiPrinter means checking the printer connection status
     */
        public int sunmiPrinter = CheckSunmiPrinter;

        /**
    *  SunmiPrinterService for API
    */
        //private SunmiPrinterService sunmiPrinterService;

        private static SunmiPrintHelper helper = new SunmiPrintHelper();

        private SunmiPrintHelper() { }

        public static SunmiPrintHelper GetInstance()
        {
            return helper;
        }

        

     }
}