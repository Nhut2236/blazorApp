using System.Text.Json;
using Microsoft.JSInterop;

namespace BlazorApp.Middlewares;

public static class SessionManager
{
    private const string SessionKey = "Session";

    public static async Task<Session> GetSession(IJSRuntime jsRuntime)
    {
        var existSession = await GetStoredSession(jsRuntime);
        
        if (existSession == null)
        {
            var session = new Session { Id = GenerateSessionId() };
            await StoreSession(jsRuntime, session);
            return session;
        }

        return existSession;
    }

    private static async Task<Session> GetStoredSession(IJSRuntime jsRuntime)
    {
       var session = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", SessionKey);

       if (!string.IsNullOrEmpty(session))
           return JsonSerializer.Deserialize<Session>(session);

       return null;
    }

    public static async Task StoreSession(IJSRuntime jsRuntime, Session session)
    {
        var sessionJsonString = JsonSerializer.Serialize(session);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", SessionKey, sessionJsonString);
    }

    private static string GenerateSessionId()
    {
        // Generate a unique session ID using a GUID or any other desired approach
        return Guid.NewGuid().ToString();
    }
}