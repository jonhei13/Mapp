using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views.InputMethods;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using System.Linq;
using MovieSearch.iOS;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            MovieSettings ApiConnection = new MovieSettings();
            IApiMovieRequest _movieApi;
           _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);



    
        // Get our button from the layout resource,
        // and attach an event to it
            var titleButton = this.FindViewById<Button>(Resource.Id.getMovieButton);
            var titleText = this.FindViewById<EditText>(Resource.Id.searchMovieText);
            var titleResult = this.FindViewById<TextView>(Resource.Id.searchResult);

            titleButton.Click += async (object sender, EventArgs e) =>
            {
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(titleText.WindowToken, 0);
                ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(titleText.Text);
                titleResult.Text = (from x in response.Results select x.Title).FirstOrDefault();
  
      
            };

         //   Button button = FindViewById<Button>(Resource.Id.myButton);

         //   button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}

