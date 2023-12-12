using codesome.Shared.Models;


namespace codesome.Client.Pages.Courses
{
    public partial class Courses
    {
        private List<Course> courses = new List<Course>();

        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }
    }
}
