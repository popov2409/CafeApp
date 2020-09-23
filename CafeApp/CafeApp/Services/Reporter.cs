using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace CafeApp.Services
{
    public class Reporter
    {
        public async Task SendReport(string data)
        {

            var message = new EmailMessage
            {
                Subject = "",
                Body = "",
                To = { "" }
            };

            var file = Path.Combine(FileSystem.CacheDirectory, $"report_{DateTime.Now.ToShortDateString().Replace('/', '_')}.txt");
            File.WriteAllText(file, data);
            message.Attachments.Add(new EmailAttachment(file));
            await Email.ComposeAsync(message);

        }
    }
}
