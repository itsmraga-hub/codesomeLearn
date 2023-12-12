using Blazorise.Snackbar;
using codesome.Shared.Models.DTOs.requests;
using codesome.Shared.Models.DTOs.responses;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace codesome.Client.Pages.Users
{
    public partial class Register
    {
        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
        }

        private SnackbarStack _snackbarStack = new();

        private UserRequestDTO registerModel = new UserRequestDTO();

        private async void RegisterUser()
        {
            // Add logic to handle registration, e.g., calling a service or API
            // You can access the properties of the 'registerModel' object to get user input
            // For simplicity, let's print the registration details to the console
            // Console.WriteLine($"First Name: {registerModel.FirstName}, Last Name: {registerModel.LastName}, Email: {registerModel.Email}, Phone: {registerModel.Phone}, Password: {registerModel.Password}, Confirm Password: {registerModel.ConfirmPassword}");
            
            await SubmitAsync();
        }

        private async Task SubmitAsync()
        {
            var data = JsonConvert.SerializeObject(registerModel);
            var requestContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await Http.PostAsync("/api/users/register", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RegistrationLoginReponseDTO>();
                Console.WriteLine(JsonConvert.SerializeObject(result));
                Console.WriteLine(JsonConvert.SerializeObject(result?.StatusCode));
                if (result?.StatusCode == "0")
                {

                    // Show the success message in the snackbar
                    await _snackbarStack.PushAsync("Registration Success!!!", SnackbarColor.Success);

                    _navigator.NavigateTo("/login");
                }
                else
                {
                    await _snackbarStack.PushAsync("Registration Failed!!!", SnackbarColor.Danger);
                }
            }
            else
            {
                await _snackbarStack.PushAsync("Registration Failed!!!", SnackbarColor.Danger);
            }
        }

        /* public class RegisterModel
         {
             public string FirstName { get; set; }
             public string LastName { get; set; }
             public string Email { get; set; }
             public string Phone { get; set; }
             public string Password { get; set; }
             public string ConfirmPassword { get; set; }
         }*/

    }
}
