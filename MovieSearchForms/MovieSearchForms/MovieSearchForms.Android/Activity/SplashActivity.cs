using Android.App;
using Android.OS;
using Android.Support.V7.App;
using MovieSearch.MovieApiService;
using MovieSearchForms.Droid;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/splashscreen", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            // Create your application here
            this.StartActivity(typeof(MainActivity));
        }
    }
}