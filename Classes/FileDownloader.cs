using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EpApp.Classes
{
    public interface IFileSaver
    {
        string Save(string source, string extension = "jpeg");
        void SaveTo(string source, string destination);
    }

    public class FileDownloader : IFileSaver
    {
        public string Save(string source, string extension = "jpeg")
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

            source = source.Replace("&amp;", "&");

            //Create a temporary file.
            string temp = Path.GetTempFileName();
            temp = Path.ChangeExtension(temp, extension);

            SaveTo(source, temp);

            return temp;
        }

        public void SaveTo(string source, string destination)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            DownloadFileAsync(source, destination).Wait();
        }

        private async Task DownloadFileAsync(string source, string destination)
        {
            using (var httpClient = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, source))
                {
                    using (Stream contentStream = await 
                        (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
                                stream = new FileStream(destination, FileMode.Create, FileAccess.Write))
                    {
                        await contentStream.CopyToAsync(stream);
                    }
                }
        }
    }
}