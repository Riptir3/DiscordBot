using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DcYoutubeBot.Config
{
    public class BotConfigReader
    {
        public string Token { get; private set; }
        public string Prefix { get; private set; }
        public List<string> ForbiddenWords { get; private set; }

        public async Task ReadJson()
        {
            using (var reader = new StreamReader("ClientConfig.json"))
            {
                var json = await reader.ReadToEndAsync();
                JsonStruct data = JsonConvert.DeserializeObject<JsonStruct>(json);

                Token = data.Token;
                Prefix = data.Prefix;
                ForbiddenWords = data.ForbiddenWords;
            }
        }
    }

    internal sealed class JsonStruct
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public List<string> ForbiddenWords { get; set; }
    }

}
