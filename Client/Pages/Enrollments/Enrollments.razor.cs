using codesome.Shared.Models;

namespace codesome.Client.Pages.Enrollments
{
    public partial class Enrollments
    {
        private List<Enrollment> enrollments = new List<Enrollment>();



        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }
    }
}
