using Android.Content;
using Android.Graphics;
using SunmiXamPrint.Interfaces;
using Com.Sunmi.Peripheral.Printer;
using Com.Sunmi.Library;


[assembly: Xamarin.Forms.Dependency(typeof(IPrinterService))]
namespace SunmiXamPrint.Droid
{
    public class PrinterService : IPrinterService
    {
        Context currentContext = Android.App.Application.Context;

       // private SunmiPrinterService sunmiPrinterService;
        public PrinterService()
        {
        }

        /**
     * print text
     * setPrinterStyle:Api require V4.2.22 or later, So use esc cmd instead when not supported
     *  More settings reference documentation {@link WoyouConsts}
     * printTextWithFont:
     *  Custom fonts require V4.14.0 or later!
     *  You can put the custom font in the 'assets' directory and Specify the font name parameters
     *  in the Api.
     */

        public void PrintText(string content, float size, bool isBold, bool isUnderLine,
                          string typeface)
        {
            //example
            //sunmiPrinterService.printBarCode("{C1234567890123456", 8, 90, 2, 2, null);
            //if (sunmiPrinterService == null)
            //{
            //    //TODO Service disconnection processing
            //    return;
            //}
            //sunmiPrinterService.PrintTextWithFont(content, typeface, size, null);
        }
    }
}