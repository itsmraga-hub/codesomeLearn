using codesome.Shared.Models;


namespace codesome.Client.Pages.Modules
{
    public partial class Modules
    {
        private List<Module> modules = new List<Module>();

        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }
    }
}
