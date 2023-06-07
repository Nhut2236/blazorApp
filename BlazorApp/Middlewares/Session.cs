using BlazorApp.Constants;

namespace BlazorApp.Middlewares;

public class Session
{
    public string Id { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public LanguagesEnum Language { get; set; } = LanguagesEnum.En;
}