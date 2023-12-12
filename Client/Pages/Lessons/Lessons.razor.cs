using codesome.Shared.Models;


namespace codesome.Client.Pages.Lessons
{
    public partial class Lessons
    {
        private List<Lesson> lessons = new List<Lesson>();

        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
        }
    }
}
