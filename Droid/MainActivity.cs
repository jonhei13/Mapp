using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views.InputMethods;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using System.Linq;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using MovieSearch.iOS;
using System.Threading.Tasks;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme")]
    public class MainActivity : Activity
    {

        private List<MovieDetails> _movieList;
        private IApiMovieRequest _movieApi;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            MovieSettings ApiConnection = new MovieSettings();
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            _movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);

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
                        ImagePath = mov.PosterPath,
         
                    };
                    movieDetails.Actors = await GetCredits(movieDetails.Id, _movieApi);
                    if (movieDetails != null)
                    {
                        _movieList.Add(movieDetails);
                    }
                }

                progressBar.Visibility = Android.Views.ViewStates.Gone;
                var intent = new Intent(this, typeof(MovieListActvity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieList));
                this.StartActivity(intent);

            };
        }
        private async Task<List<string>> GetCredits(int movieId, IApiMovieRequest _movieApi)
        {
            ApiQueryResponse<MovieCredit> response = await _movieApi.GetCreditsAsync(movieId);
            try
            {
                var actors = (from x in response.Item.CastMembers select x.Name).Take(3).ToList();
                return actors;

            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                return null;
            }

        }
    }
}

