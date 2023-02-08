using CommonEnitity.Catalog;
using CommonEnitity.Users;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WebEcommerce.Models.Helper;
using WebEcommerce.Models.Interfaces;

namespace WebEcommerce.Models.ApiHelper;
public class ApiClientFactory: IApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClientFactory(IHttpClientFactory httpClient, string clintName)
    {
        _httpClient = httpClient.CreateClient(clintName);
        
        CookieHelper cookieHelper = new CookieHelper(new HttpContextAccessor());
        string? Token = cookieHelper.getTokenCookie();
        if (!string.IsNullOrEmpty(Token))
        {
            string jwt = Token;
            _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", jwt);
        }
    }

    public async Task<string> GetToken(AppUser userData)
    {
        string jwt = string.Empty;
        string strAPIUrl = $"Token/Login";

        var response = await
            _httpClient.PostAsJsonAsync<AppUser>(strAPIUrl, userData);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            jwt = apiResponse;
        }
        CookieHelper cookieHelper = new CookieHelper(new HttpContextAccessor());
        cookieHelper.setTokenCookie(jwt);
        return jwt;
    }
    public async Task<IEnumerable<T>> PostListAsync<T>(string apiPath,
        object jsonContent)
    {
        IEnumerable<T> objList;
        //string content = JsonSerializer.Serialize(jsonContent);
        //string strAPIUrl = $"Catalog/GetCatalogItemListAsync";
        var response = await
                _httpClient.PostAsJsonAsync(apiPath, jsonContent);


        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string apiResponse = await response.Content.ReadAsStringAsync();
            objList = JsonSerializer.Deserialize<IEnumerable<T>>(apiResponse, options);
            return objList;
        }
        else
        {
            objList = new List<T>();
            return objList;
        }        
    }

    public async Task<T> PostAsync<T>(string apiPath, object jsonContent)
    {
        //StringContent content = new
        //    StringContent(JsonSerializer.Serialize(jsonContent),
        //    Encoding.UTF8, "text/plain");

        T obj;
        var response = await _httpClient.PostAsJsonAsync(apiPath, jsonContent);
        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string apiResponse = await response.Content.ReadAsStringAsync();
            obj = JsonSerializer.Deserialize<T>(apiResponse, options);
            return obj;
        }
        else
        {
            obj = Activator.CreateInstance<T>();
            return obj;
        }
    }

    public async Task PostVoidAsync(string apiPath, object jsonContent)
    {
        var response = await _httpClient.PostAsJsonAsync(apiPath, jsonContent);        
    }

    public Task<T> GetAsync<T>()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>(string apiPath)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>(string apiPath, bool addClientHeader = false, bool accessToken = false)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>(string apiPath, Dictionary<string, string> headerParam, bool addClientHeader = false, bool accessToken = false)
    {
        throw new NotImplementedException();
    }

    

    public Task<T> PostAsync<T>(string apiPath, object jsonContent, bool addClientHeader = false, bool accessToken = false)
    {
        throw new NotImplementedException();
    }

    public Task<T> PostAsync<T>(string apiPath, object jsonContent, Dictionary<string, string> headerParam, bool addClientHeader = false, bool accessToken = false)
    {
        throw new NotImplementedException();
    }

    
}

