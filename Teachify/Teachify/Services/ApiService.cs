using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MartialApp.Models;
using Newtonsoft.Json;
using Teachify.Models;
using Xamarin.Essentials;

namespace Teachify.Services
{
    public class ApiService
    {
        private  HttpClient httpClient { get; }

        public ApiService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };

        }
        public async Task<bool> RegisterUser(string email, string password, string confirmPassword)
        {
            var registerModel = new RegisterModel()
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://instructorsapi.azurewebsites.net/api/Account/Register", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<TokenResponse> GetToken(string email, string password)
        {
            var content = new StringContent($"grant_type=password&username={email}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync("https://instructorsapi.azurewebsites.net//Token", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);
            return result;
        }

        public async Task<bool> PasswordRecovery(string email)
        {
            var recoverPasswordModel = new PasswordRecoveryModel()
            {
                Email = email
            };
            var json = JsonConvert.SerializeObject(recoverPasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://instructorsapi.azurewebsites.net/Users/PasswordRecovery", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var changePasswordModel = new ChangePasswordModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };
            var json = JsonConvert.SerializeObject(changePasswordModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.PostAsync("https://instructorsapi.azurewebsites.net/Account/ChangePassword", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> BecomeAnInstructor(Trainers instructor)
        {
            var json = JsonConvert.SerializeObject(instructor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.PostAsync("api/Students", content);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public async Task<List<Trainers>> GetInstructors()
        {
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync("/api/Students");
            return JsonConvert.DeserializeObject<List<Trainers>>(response);
        }

        public async Task<Trainers> GetInstructor(int id)
        {
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync("/api/Students/" + id);
            return JsonConvert.DeserializeObject<Trainers>(response);
        }

        public async Task<List<Instructor>> SearchInstructors(string subject, string gender, string city)
        {
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", ""));
            var response = await httpClient.GetStringAsync("https://instructorsapi.azurewebsites.net/api/instructors?subject=" + subject + "&gender=" + gender + "&city=" + city);
            return JsonConvert.DeserializeObject<List<Instructor>>(response);
        }

        public async Task<List<City>> GetCities()
        {
            var response = await httpClient.GetStringAsync("https://instructorsapi.azurewebsites.net/api/cities");
            return JsonConvert.DeserializeObject<List<City>>(response);
        }

        public async Task<List<Course>> GetCourses()
        {
            var response = await httpClient.GetStringAsync("https://instructorsapi.azurewebsites.net/api/courses");
            return JsonConvert.DeserializeObject<List<Course>>(response);
        }

    }
}
