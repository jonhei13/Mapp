using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Newtonsoft.Json;
using MovieSearch.Models;
using Android.Widget;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme")]
    public class MovieListActvity : Activity
    {
        private List<MovieDetails> _movieList;
        private ListView _listview;
        private const string TITLE = "Movie List";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.SetContentView(Resource.Layout.ListView);
            _movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);
            this._listview = this.FindViewById<ListView>(Resource.Id.list);
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);


            var jsonStr = this.Intent.GetStringExtra("movieList");
            this._movieList = JsonConvert.DeserializeObject<List<MovieDetails>>(jsonStr);
            this._listview.Adapter = new MovieListAdapter(this, this._movieList);
            this._listview.SetBackgroundResource(Resource.Drawable.Background);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = TITLE;
            this._listview.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetail", JsonConvert.SerializeObject(this._movieList[args.Position]));
                this.StartActivity(intent);
            };




        }
    }
}