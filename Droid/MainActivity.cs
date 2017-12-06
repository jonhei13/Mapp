using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using System.Collections.Generic;
using Android.Content;
using Newtonsoft.Json;
using MovieSearch.Models;
using MovieSearch.MovieApiService;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V4.App;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace MovieSearch.Droid
{
    [Activity(Label = "MovieSearch", Theme = "@style/MyTheme")]
    public class MainActivity : FragmentActivity
    {
        private List<MovieDetails> _movieList;
        private MovieSearchService _movieService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this._movieList = new List<MovieDetails>();
            base.OnCreate(savedInstanceState);
            this._movieService = new MovieSearchService();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var fragments = new Fragment[]
            {
                new TitleInputFragment(),
                new TopRatedFragment()
            };
            var titles = CharSequence.ArrayFromStringArray(new[] { "Search", "Top Rated" });

            
            var viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);
            
            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "My Toolbar";

        
        }


    } 
}


