
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassGenerator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Strength : ContentPage
    {

        public Strength()
        {
            InitializeComponent();
            setStrengthValues();
        }

        public void setStrengthValues()
        {
            Console.WriteLine(MainPage.final);

            double final = MainPage.final;

            if (final < 40)
            {
                StrengthMeter.ProgressTo(0.2, 1000, Easing.SinInOut);
                StrengthMeter.ProgressColor = Color.Red;
                rating.Text = "Poor";
            }
            else if (final >= 40 && final < 60)
            {
                StrengthMeter.ProgressTo(0.5, 2000, Easing.SinInOut);
                rating.Text = "Fair";
                StrengthMeter.ProgressColor = Color.Orange;
            }
            else if (final >= 60 && final < 80)
            {
                StrengthMeter.ProgressTo(0.8, 3000, Easing.SinInOut);
                rating.Text = "Strong";
                StrengthMeter.ProgressColor = Color.LightGreen;
            }
            else
            {
                StrengthMeter.ProgressTo(1, 4000, Easing.SinInOut);
                rating.Text = "Excellent";
                StrengthMeter.ProgressColor = Color.DarkGreen;
            }
        }
    }
}