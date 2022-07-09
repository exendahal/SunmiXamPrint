
namespace SunmiXamPrint.Interfaces
{
    public interface IPrinterService
    {
        void PrintText(string content, float size, bool isBold, bool isUnderLine,
                          string typeface);
    }
}
