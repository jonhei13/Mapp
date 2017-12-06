using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MovieSearch.iOS;
using Com.Bumptech.Glide;

namespace MovieSearch.Droid
{
    class MovieListAdapter : BaseAdapter<MovieDetails>
    {

        private readonly Activity _context;
        private readonly List<MovieDetails> _movieList;


        public MovieListAdapter(Activity context, List<MovieDetails> movies)
        {
            this._context = context;
            this._movieList = movies;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;


            if (view == null)
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieListItem, null);

            var movie = this._movieList[position];
            var actors = "";
            foreach(var name in movie.Actors)
            {
                actors += name + ",";
            }
            view.FindViewById<TextView>(Resource.Id.name).Text = movie.Title;
            view.FindViewById<TextView>(Resource.Id.year).Text = movie.ReleaseDate.Year.ToString();
            view.FindViewById<TextView>(Resource.Id.actors).Text = actors;
            var imageView = view.FindViewById<ImageView>(Resource.Id.picture);
            Glide.With(this._context).Load("http://image.tmdb.org/t/p/original/"  + movie.ImagePath).Into(imageView);

            return view;

        }

        public override int Count => this._movieList.Count;
        public override MovieDetails this[int position] => this._movieList[position];
    }

}