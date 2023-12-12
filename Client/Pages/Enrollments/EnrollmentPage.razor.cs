using codesome.Shared.Models;

namespace codesome.Client.Pages.Enrollments
{
    public partial class EnrollmentPage
    {
        private Enrollment enrollment = new Enrollment();

        private int Id { get; set; }


        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }
    }
}
