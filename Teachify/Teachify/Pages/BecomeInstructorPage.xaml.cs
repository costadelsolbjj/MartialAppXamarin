﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartialApp.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Teachify.Helpers;
using Teachify.Models;
using Teachify.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Teachify.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BecomeInstructorPage : ContentPage
    {
        private MediaFile file;
        public BecomeInstructorPage()
        {
            InitializeComponent();
        }

        private async void TapCamera_OnTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Custom,
                CustomPhotoSize = 30,
                CompressionQuality = 60,    
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            ImgProfile.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void BtnApply_OnClicked(object sender, EventArgs e)
        {
            //var imageArray = FilesHelper.ReadFully(file.GetStream());
            //file.Dispose();
            var instructor = new Trainers()
            {
                Name = EntName.Text,
                UserName = EntUserName.Text,
                LastName = EntLastName.Text,
                //Gender = PickerGender.Items[PickerGender.SelectedIndex],
                Phone = EntPhone.Text,
                Email = EntEmail.Text,
                SchoolId = 1,//EntAcademy.Text,
                BeltId = 1,//PickerBelt.Items[PickerBelt.SelectedIndex],
                Importe = Convert.ToDouble(PickerRate.Items[PickerRate.SelectedIndex]),
                Tarifa = PickerTarifa.Items[PickerTarifa.SelectedIndex],
                //City = PickerCity.Items[PickerCity.SelectedIndex],
                //OneLineTitle = EntOneLineTitle.Text,
                //First = EdtDescription.Text
                //,
                //ImageArray = imageArray,
            };
            ApiService apiService = new ApiService();
            var response = await apiService.BecomeAnInstructor(instructor);
            if (!response)
            {
                await DisplayAlert("Oops", "Something wrong...", "Cancel");
            }
            else
            {
                await DisplayAlert("Congratulations", "You're now an instructor at teachify.", "Alright");
            }
        }
    }
}