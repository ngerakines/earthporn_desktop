using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EpApp.Classes
{
    public interface IFileSaver
    {
        string Save(string source, string extension = "jpeg");
    }

    public class FileDownloader : IFileSaver
    {
        public string Save(string source, string extension = "jpeg")
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

            source = SanitizeSource(source, extension);

            //Create a temporary file.
            string temp = Path.GetTempFileName();
            temp = Path.ChangeExtension(temp, extension);

            DownloadFileAsync(source, temp).Wait();

            return temp;
        }
        private string SanitizeSource(string source, string extension) 
        {
            //Get rid of the http and www
            source = source.Replace("http://", "")
                        .Replace("www.", "");

            //imgur links need to start with i. and end with the extension
            //to get the actual image.
            if (source.StartsWith("imgur"))
                source = string.Format("http://i.{0}.{1}", source, extension);
            else
                source = string.Format("http://", source);

            source = source.Replace("&amp;", "&");

            return source;
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