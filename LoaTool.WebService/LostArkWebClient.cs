using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace LoaTool.WebService;
public class LostArkWebClient
{
    private readonly HttpClient client;

    public LostArkWebClient()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://developer-lostark.game.onstove.com/");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAuthorization());

        this.client = client;
    }

    public async void getData()
    {
        try
        {
            string nick = "맹무";
            string encode = HttpUtility.UrlEncode(nick);
            string url = "armories/characters/" + encode;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
    }

    //public async APIData call<APIData>(String url) where APIData : DataBase
    //{
    //    HttpResponseMessage response = await client.GetAsync(url);
    //    response.EnsureSuccessStatusCode();

    //    string responseBody = await response.Content.ReadAsStringAsync();

    //    return 
    //}

    private static string GetAuthorization()
    {
        string? apiKey = ConfigurationManager.AppSettings["API_KEY"];

        if(apiKey != null)
        {
            return apiKey;
        }

        throw new KeyNotFoundException("API_KEY is not dectected.");
    }
}
