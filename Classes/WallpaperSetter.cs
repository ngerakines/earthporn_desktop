using System.Runtime.InteropServices;

namespace EpApp.Classes
{
    public interface IWallpaperSetter
    {
        void SetWallpaper(string fileName);
    }

    public class WallpaperImageSetter : IWallpaperSetter
    {
        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);
        const uint SPI_SETDESKWALLPAPER = 0x14;
        const uint SPIF_UPDATEINIFILE = 0x01;

        public void SetWallpaper(string fileName)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, fileName, SPIF_UPDATEINIFILE);
        }
    }
}