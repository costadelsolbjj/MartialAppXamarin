﻿using System;
using Teachify.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Teachify
{
    public partial class App : Application
    {
        public static string AzureBackendUrl =

            DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.1.141:45456/" : "http://localhost:5000";

        public App()
        {
            InitializeComponent();
            //if (!string.IsNullOrEmpty(Preferences.Get("accesstoken", "")))
            //{
                MainPage = new MasterPage();
            //}
            //else if (string.IsNullOrEmpty(Preferences.Get("useremail", "")) && string.IsNullOrEmpty(Preferences.Get("password", "")))
            //{
            //    MainPage = new NavigationPage(new LoginPage());
            //}
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
