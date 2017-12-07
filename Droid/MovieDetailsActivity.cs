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
using Newtonsoft.Json;
using MovieSearch.Models;
using Com.Bumptech.Glide;

namespace MovieSearch.Droid
{
    [Activity(Theme = "@style/MyTheme")]
    public class MovieDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MovieDetails);
            var jsonStr = this.Intent.GetStringExtra("movieDetail");
            var movie = JsonConvert.DeserializeObject<MovieDetails>(jsonStr);

            var genre = "";
            foreach (var x in movie.Genre)
            {
                if (movie.Genre.IndexOf(x) == movie.Genre.Count - 1)
                {
                    genre += x;
                }
                else
                {
                    genre += x + ", ";
                }
            }
            this.FindViewById<TextView>(Resource.Id.movieTitle).Text = movie.Title + "(" + movie.ReleaseDate.Year.ToString() + ")";
            this.FindViewById<TextView>(Resource.Id.genre).Text = genre;
            this.FindViewById<TextView>(Resource.Id.description).Text = movie.Description;
            var imageView = this.FindViewById<ImageView>(Resource.Id.picture);
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Details";
            Glide.With(this).Load("http://image.tmdb.org/t/p/original/" + movie.ImagePath).Into(imageView);

        }
    }
}