using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Views.InputMethods;
using Newtonsoft.Json;
using MovieSearch.Models;
using MovieSearch.MovieApiService;

namespace MovieSearch.Droid
{
    public class TitleInputFragment : Fragment
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _movieService;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this._movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);
            this._movieService = new MovieSearchService();

            var rootView = inflater.Inflate(Resource.Layout.TitleInput, container, false);
            var titleText = rootView.FindViewById<EditText>(Resource.Id.searchMovieText);
            var nameListButton = rootView.FindViewById<Button>(Resource.Id.nameListButton);
            var progressBar = rootView.FindViewById<ProgressBar>(Resource.Id.MovieProgress);
            progressBar.Visibility = ViewStates.Invisible;
            nameListButton.Click += async (sender, args) =>
            {
                progressBar.Visibility = ViewStates.Visible;
                var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(titleText.WindowToken, 0);
                _movieList = await _movieService.GetMoviesByTitle(titleText.Text);

                progressBar.Visibility = ViewStates.Gone;
                var intent = new Intent(this.Context, typeof(MovieListActvity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieList));
                this.StartActivity(intent);
            };

            return rootView;
        }
    }
}