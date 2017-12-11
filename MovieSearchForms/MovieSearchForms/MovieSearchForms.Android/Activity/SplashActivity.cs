using Android.App;
using Android.OS;
using MovieSearch.MovieApiService;
using MovieSearchForms.Droid;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            this.StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}