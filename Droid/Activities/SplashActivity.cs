using Android.App;
using Android.OS;
using MovieSearch.MovieApiService;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
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