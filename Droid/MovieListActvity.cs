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

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieList", Theme = "@style/MyTheme")]
    public class MovieListActvity : Activity
    {
        private List<MovieDetails> _movies;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }
    }
}