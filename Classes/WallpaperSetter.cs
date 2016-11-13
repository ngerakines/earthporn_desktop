using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EpApp.Classes
{
    public interface IWallpaperSetter
    {
        void SetWallpaper(string fileName);
    }

    public class WindowsWallpaperImageSetter : IWallpaperSetter
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

    public class MacOSWallpaperImageSetter : IWallpaperSetter
    {
        public void SetWallpaper(string fileName)
        {
            var command = "osascript";
            var args = $"-e \"tell application \\\"Finder\\\" to set desktop picture to POSIX file \\\"{fileName}\\\"\"";
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = command;
            ExternalProcess.StartInfo.Arguments = args;
            ExternalProcess.StartInfo.CreateNoWindow = true;
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();
        }
    }
}
