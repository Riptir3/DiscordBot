using AngleSharp.Io;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace DcYoutubeBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("help")]
        public async Task TestCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"Test command executed successfully by {ctx.User.Username}");
        }

        [Command("joke")]
        [Description("Get back a random fun from JokeApi")]
        public async Task JokeAsync(CommandContext ctx)
        {
            using (var http = new HttpClient())
            {
                var response = await http.GetStringAsync("https://v2.jokeapi.dev/joke/Any");
                System.Console.WriteLine(response);

                using (var doc = JsonDocument.Parse(response))
                {
                    var root = doc.RootElement;

                    string jokeText;

                    var type = root.GetProperty("type").GetString();
                    if (type == "single")
                    {
                        jokeText = root.GetProperty("joke").GetString();
                    }
                    else
                    {
                        var setup = root.GetProperty("setup").GetString();
                        var delivery = root.GetProperty("delivery").GetString();
                        jokeText = $"{setup}\n{delivery}";
                    }

                    await ctx.RespondAsync(jokeText);
                }
            }
        }

        [Command("weather")]
        public async Task WeatherCommand(CommandContext ctx, [RemainingText] string city)
        {
            await ctx.TriggerTypingAsync();

            if (string.IsNullOrWhiteSpace(city))
            {
                await ctx.RespondAsync("Kérlek, add meg a város nevét!");
                return;
            }
            try
            {
                string geoUrl = $"https://nominatim.openstreetmap.org/search?q={city}&format=json&limit=1";
                using (var http = new HttpClient())
                {
                    http.DefaultRequestHeaders.UserAgent.ParseAdd("DiscordBot/1.0 (example@example.com)");
                    var geoResponse = await http.GetStringAsync(geoUrl);
                    var geoJson = JsonDocument.Parse(geoResponse).RootElement;

                    if (geoJson.GetArrayLength() == 0)
                    {
                        await ctx.RespondAsync("Nem található a város!");
                        return;
                    }
                    string lat = geoJson[0].GetProperty("lat").GetString();
                    string lon = geoJson[0].GetProperty("lon").GetString();

                    string weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
                    var weatherResponse = await http.GetStringAsync(weatherUrl);

                    var weatherJson = JsonDocument.Parse(weatherResponse).RootElement;
                    var currentWeather = weatherJson.GetProperty("current_weather");
                    double temp = currentWeather.GetProperty("temperature").GetDouble();
                    double windSpeed = currentWeather.GetProperty("windspeed").GetDouble();
                    string weatherCode = currentWeather.GetProperty("weathercode").GetInt32().ToString();

                    await ctx.RespondAsync($"🌤 Weather in **{city}**:\nTemperature: {temp}°C\nWind speed: {windSpeed} km/h\nWeather code: {weatherCode}");
                }
            }
            catch
            {
                await ctx.RespondAsync("Hiba történt az időjárás lekérése közben.");
            }
        }

        [Command("recommend")]
        public async Task RecommendCommand(CommandContext ctx, [RemainingText] string movieName)
        {
            await ctx.TriggerTypingAsync();

            if (string.IsNullOrWhiteSpace(movieName))
            {
                await ctx.RespondAsync("Kérlek, add meg a film nevét!");
                return;
            }

            try
            {
                string apiKey = "";
                string searchUrl = $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={HttpUtility.UrlEncode(movieName)}";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    client.DefaultRequestHeaders.Host = "api.themoviedb.org";

                    var searchResponse = await client.GetStringAsync(searchUrl);

                    var searchJson = JsonDocument.Parse(searchResponse);
                    var searchResults = searchJson.RootElement.GetProperty("results");

                    if (searchResults.GetArrayLength() == 0)
                    {
                        await ctx.RespondAsync("Nem található film ezzel a névvel.");
                        return;
                    }

                    var firstMovie = searchResults[0];
                    int movieId = firstMovie.GetProperty("id").GetInt32();
                    string title = firstMovie.GetProperty("title").GetString();

                    string recommendUrl = $"https://api.themoviedb.org/3/movie/{movieId}/recommendations";

                    var recommendResponse = await client.GetStringAsync(recommendUrl);

                    var recommendJson = JsonDocument.Parse(recommendResponse);
                    Console.WriteLine(recommendJson);
                    var recommendations = recommendJson.RootElement.GetProperty("results");
     
                    if (recommendations.GetArrayLength() == 0)
                    {
                        await ctx.RespondAsync($"Nincs ajánlás a(z) **{title}** című filmhez.");
                        return;
                    }

                    string message = $"🎬 Ajánlott filmek a(z) **{title}** alapján:\n";
                    int count = 0;
                    foreach (var movie in recommendations.EnumerateArray())
                    {
                        string recTitle = movie.GetProperty("title").GetString();
                        message += $"- {recTitle}\n";
                        count++;
                        if (count >= 5) break;
                    }

                    await ctx.RespondAsync(message);
                }
            }
            catch
            {
                await ctx.RespondAsync("Hiba történt a filmek lekérése közben.");
            }
        }
    }
}
