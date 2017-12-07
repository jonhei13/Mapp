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
        private ListView _listview;
        public TopRatedFragment(MovieSearchService movieService) {
            this._movieService = movieService;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this._movieList = new List<MovieDetails>();

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
    
            base.OnCreate(savedInstanceState);
            var rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);
            _listview = rootView.FindViewById<ListView>(Resource.Id.list);

            _listview.Adapter = new MovieListAdapter(this.Activity, this._movieList);




            return rootView;
            /*
                if (e.Item.TitleFormatted.Equals())
                getMovies();
                var intent = new Intent(this.Context, typeof(MovieListActvity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(_movieList));
                this.StartActivity(intent);
            */


        }
        public async void getMovies()
        {
            _movieList = await _movieService.GetMoviesByTitle("fargo");
            _listview.Adapter = new MovieListAdapter(this.Activity, this._movieList);
        }

    }

}