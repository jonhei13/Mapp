using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieSearch.MovieApiService;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            MainActivity.MovieService = new MovieSearchService();
            // Create your application here
            this.StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}