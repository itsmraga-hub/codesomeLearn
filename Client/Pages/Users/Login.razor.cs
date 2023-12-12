using static System.Net.WebRequestMethods;
using System.Text;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using codesome.Shared.Models.DTOs.requests;
using codesome.Shared.Models.DTOs.responses;
using Blazorise;
using Blazorise.Snackbar;

namespace codesome.Client.Pages.Users
{
    public partial class Login
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        private LoginDTO loginModel = new LoginDTO();

        private Snackbar _snackbar = new();
        private SnackbarStack _snackbarStack = new();

        private async Task LoginUser()
        {
            // Add logic to handle login, e.g., calling a service or API
            // You can access the properties of the 'loginModel' object to get user input
            // For simplicity, let's print the login details to the console
            // Console.WriteLine($"Username: {loginModel.UserName}, Password: {loginModel.Password}");

            await SubmitAsync();
        }

        private async Task SubmitAsync()
        {
            var data = JsonConvert.SerializeObject(loginModel);
            var requestContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await Http.PostAsync("/api/v1/users/login", requestContent);
            // Console.WriteLine(JsonConvert.SerializeObject(response?.StatusCode));
            // Console.WriteLine(JsonConvert.SerializeObject(response));
            if (response?.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadFromJsonAsync<RegistrationLoginReponseDTO>();
                if (result?.StatusCode == "0")
                {
                    await _localStorage.SetItemAsync("isLoggedIn", true);
                    await _localStorage.SetItemAsync("userId", result.userId);
                    await _localStorage.SetItemAsync("userAccessToken", result.Token.ToString());
                    // await _localStorage.SetItemAsync("phoneNumber", result.PhoneNumber.ToString());
                    // await _localStorage.SetItemAsync("profileImageUrl", result.profileImageUrl.ToString());
                    await _localStorage.SetItemAsync("email", result.email);
                    // await _localStorage.SetItemAsync("username", result.username);
                    // await _localStorage.SetItemAsync("role", result.UserType);

                    // Show the success message in the snackbar
                    await _snackbarStack.PushAsync("Login Success!!!", SnackbarColor.Success);
                   
                    _navigator.NavigateTo("/dashboard");
                }
                else
                {
                    await _localStorage.SetItemAsync<bool>("isLoggedIn", false);
                    await _snackbarStack.PushAsync("Login Failed!!!", SnackbarColor.Danger);
                }
            }
            else
            {
                await _localStorage.SetItemAsync<bool>("isLoggedIn", false);
                
            }
        }
    }
}
