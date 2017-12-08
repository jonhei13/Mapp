using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    public class TopRatedFragment : Fragment
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _movieService;
        private ListView _listview;
        private View _rootView;
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
            _rootView = inflater.Inflate(Resource.Layout.TopRated, container, false);
            _listview = _rootView.FindViewById<ListView>(Resource.Id.list);
            this._listview.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this.Activity, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetail", JsonConvert.SerializeObject(this._movieList[args.Position]));
                this.StartActivity(intent);
            };
            _listview.Adapter = new MovieListAdapter(this.Activity, this._movieList);
            return _rootView;
        }
        public async void getMovies()
        {
            var progressBar = _rootView.FindViewById<ProgressBar>(Resource.Id.TopRatedProgress);
            progressBar.Visibility = ViewStates.Visible;
            _movieList = await _movieService.GetTopRatedMovies();
            _listview.Adapter = new MovieListAdapter(this.Activity, this._movieList);
            progressBar.Visibility = ViewStates.Gone;
        }

    }

}