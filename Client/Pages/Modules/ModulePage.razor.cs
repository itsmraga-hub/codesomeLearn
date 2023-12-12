using codesome.Shared.Models;

namespace codesome.Client.Pages.Modules
{
    public partial class ModulePage
    {
        private Module module = new Module();

        private int Id { get; set; }


        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }
    }
}
