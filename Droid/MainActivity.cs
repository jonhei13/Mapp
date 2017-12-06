using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using MovieSearch.MovieApiService;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _movieService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this._movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);
            this._movieService = new MovieSearchService();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var titleText = this.FindViewById<EditText>(Resource.Id.searchMovieText);
            var nameListButton = this.FindViewById<Button>(Resource.Id.nameListButton);
            var progressBar = this.FindViewById<ProgressBar>(Resource.Id.MovieProgress);
            progressBar.Visibility = Android.Views.ViewStates.Invisible;
            nameListButton.Click += async (sender, args) =>
            {
                progressBar.Visibility = Android.Views.ViewStates.Visible;
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(titleText.WindowToken, 0);
                _movieList = await _movieService.GetMoviesByTitle(titleText.Text);

                progressBar.Visibility = Android.Views.ViewStates.Gone;
                var intent = new Intent(this, typeof(MovieListActvity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieList));
                this.StartActivity(intent);
            };
        }
    }
}

