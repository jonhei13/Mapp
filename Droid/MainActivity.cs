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
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {

        private List<MovieDetails> _movieList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
         
            MovieSettings ApiConnection = new MovieSettings();
            IApiMovieRequest _movieApi;
           _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            _movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var titleButton = this.FindViewById<Button>(Resource.Id.getMovieButton);
            var titleText = this.FindViewById<EditText>(Resource.Id.searchMovieText);
            var titleResult = this.FindViewById<TextView>(Resource.Id.searchResult);
            var nameListButton = this.FindViewById<Button>(Resource.Id.nameListButton);

            titleButton.Click += async (object sender, EventArgs e) =>
            {
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(titleText.WindowToken, 0);
                ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(titleText.Text);
                titleResult.Text = (from x in response.Results select x.Title).FirstOrDefault();      
            };



            nameListButton.Click += async (sender, args) =>
            {
                ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(titleText.Text);
                var result = (from x in response.Results select x).ToList();
               
                foreach (MovieInfo mov in result)
                {
                    var movieDetails = new MovieDetails
                    {
                        Title = mov.Title,
                        Description = mov.Overview,
                        Id = mov.Id,
                        Genre = (from x in mov.Genres select x.Name).ToList(),
                        ReleaseDate = mov.ReleaseDate,
                        ImagePath = "",
                        Actors = null
                    };
                    _movieList.Add(movieDetails);
                }


                var intent = new Intent(this, typeof(MovieListActvity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieList));
                this.StartActivity(intent);
            };
        }
    }
}

