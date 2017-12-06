using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Newtonsoft.Json;

namespace MovieSearch.Droid
{
    [Activity(Label = "Movie List", Theme = "@style/MyTheme")]
    public class MovieListActvity : ListActivity
    {
        private List<MovieDetails> _movieList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            _movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);

            var jsonStr = this.Intent.GetStringExtra("movieList");
            this._movieList = JsonConvert.DeserializeObject<List<MovieDetails>>(jsonStr);

            this.ListView.ItemClick += (sender, args) =>
            {
                var intent = new Intent(this, typeof(MovieDetailsActivity));
                intent.PutExtra("movieDetail", JsonConvert.SerializeObject(this._movieList[args.Position]));
                this.StartActivity(intent);
            };
            this.ListAdapter = new MovieListAdapter(this, this._movieList);

        }
    }
}