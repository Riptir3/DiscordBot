using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace DcYoutubeBot.Config
{
    public class BotConfigReader
    {
        public string Token { get; set; }
        public string Prefix { get; set; }

        public async Task ReadJson()
        {
            using (var reader = new StreamReader("ClientConfig.json"))
            {
                var json = await reader.ReadToEndAsync();
                JsonStruct data = JsonConvert.DeserializeObject<JsonStruct>(json);

                Token = data.Token;
                Prefix = data.Prefix;
            }
        }
    }

    internal sealed class JsonStruct
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
    }

}
