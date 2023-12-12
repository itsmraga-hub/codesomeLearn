using codesome.Shared.Models;

namespace codesome.Client.Pages.Lessons
{
    public partial class LessonPage
    {
        private Course course = new Course();


        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }
    }
}
