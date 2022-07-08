using Android.Content;
using Android.Graphics;
using Com.Sunmi.Sunmiv2.Services;
using SunmiXamPrint.Interfaces;

namespace SunmiXamPrint.Droid
{
    public class PrinterService : IPrinterService
    {
        Context currentContext = Android.App.Application.Context;

        private SunmiPrinter sunmiPrinterService;
        public PrinterService()
        {
        }

        public void printText(string content, float size, bool isBold, bool isUnderLine,
                          string typeface)
        {
            if (sunmiPrinterService == null)
            {
                //TODO Service disconnection processing
                return;
            }
            sunmiPrinterService.PrintTextWithFont(content, typeface, size, null);
        }

        public void PrintText(string content)
{
            //TODO Set paramater 
            sunmiPrinterService.PrintTextWithFont(content, null, 1, null);
        }
    }
}