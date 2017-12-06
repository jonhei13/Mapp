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
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Android.Views.InputMethods;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    public class TopRatedFragment : Fragment
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
    
            base.OnCreate(savedInstanceState);

            this._movieList = new List<MovieDetails>();
            this._movieService = new MovieSearchService();
            var rootView = inflater.Inflate(Resource.Layout.Main, container, false);
            var toolbar = rootView.FindViewById<Toolbar>(Resource.Id.toolbar);
            /*
                if (e.Item.TitleFormatted.Equals())
                getMovies();
                var intent = new Intent(this.Context, typeof(MovieListActvity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieList));
                this.StartActivity(intent);
            */
        
            return rootView;
        }
        public async void getMovies()
        {
            _movieList = await _movieService.GetTopRatedMovies();
        }

    }

}