using codesome.Shared.Models;

namespace codesome.Client.Pages.Lessons
{
    public partial class LessonCreate
    {
        private Lesson lesson = new Lesson();


        protected override async Task OnInitializedAsync()
        {
            // courses = await Http<List<Course>>("WeatherForecast");

            await base.OnInitializedAsync();
        }

        private void CreateLesson()
        {

            // Add logic to handle course creation, e.g., calling a service or API
            // You can access the properties of the 'course' object to get user input

            _navigator.NavigateTo("lessons");
        }
    }
}
