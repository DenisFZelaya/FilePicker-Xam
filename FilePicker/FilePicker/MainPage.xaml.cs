using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Diagnostics;

namespace FilePicker
{
    public partial class MainPage : ContentPage
    {
        string nombreImg;
        string locationString;
        public MainPage()
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace( Preferences.Get("imgDefault", string.Empty)))
            {
                resultImage.Source = Preferences.Get("imgDefault", string.Empty);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var pickResult = await Xamarin.Essentials.FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image"
            } 
            );

            Info(pickResult.FileName, pickResult.FullPath);

            lblNameImage.Text = pickResult.FileName;
            lblRouteImage.Text = pickResult.FullPath;

            Debug.WriteLine("-----------------------------------------------");
            Debug.WriteLine(pickResult.FileName);
            Debug.WriteLine(pickResult.FullPath);
            Debug.WriteLine(pickResult.ContentType);

            Preferences.Set("imgDefault", pickResult.FullPath);

            if (pickResult != null)
            {
                var stream = await pickResult.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() =>  stream);
            }
        }

        private void lblNameImage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            lblNameImage.Text = nombreImg;
            lblRouteImage.Text = locationString;
        }

        protected void Info(string nombre, string location)
        {
            nombreImg = nombre;
            locationString = location;

            lblNameImage.Text = nombreImg;
            lblRouteImage.Text = locationString;
        }
    }
}