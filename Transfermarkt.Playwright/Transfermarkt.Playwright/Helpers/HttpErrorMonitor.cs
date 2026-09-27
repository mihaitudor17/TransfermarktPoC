using Microsoft.Playwright;

namespace Transfermarkt.Playwright.Helpers;

public class HttpErrorMonitor
{
    private readonly List<(string Url, int Status)> _errors = new();

    public IReadOnlyCollection<(string Url, int Status)> Errors => _errors;

    public void Attach(IPage page)
    {
        page.Response += (_, response) =>
        {
            if (response.Status >= 500)
            {
                _errors.Add((response.Url, response.Status));
            }
        };
    }
}