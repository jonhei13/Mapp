using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using Foundation;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Linq;

namespace MovieSearch.iOS
{
    public class MovieSearchViewsController : UIViewController
    {
       
        public MovieSearchViewsController()
        {
            MovieDbFactory.RegisterSettings(new MovieSettings());
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.White;

            var promptLabel = new UILabel()
            {
                Frame = new CGRect(0, 80, this.View.Bounds.Width, 50),
                Text = "Enter Words In Movie Title: "
            };

            this.View.AddSubview(promptLabel);

            var nameField = new UITextField()
            {
                Frame = new CGRect(0, 130, this.View.Bounds.Width, 50),
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            this.View.AddSubview(nameField);

            var SearchTitleButton = UIButton.FromType(UIButtonType.RoundedRect);
            SearchTitleButton.Frame = new CGRect(0, 180, this.View.Bounds.Width / 2, 50);
            SearchTitleButton.SetTitle("GetMovie", UIControlState.Normal);

            this.View.AddSubview(SearchTitleButton);

            var movieTitleLabel = new UILabel()
            {
                Frame = new CGRect(0, 230, this.View.Bounds.Width, 50),
      
            };
            this.View.AddSubview(movieTitleLabel);
            SearchTitleButton.TouchUpInside += async (sender, args) =>
            {
                nameField.ResignFirstResponder();
                ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(nameField.Text);

                var result = (from x in response.Results select x.Title).FirstOrDefault();
                if (result == null)
                {
                    movieTitleLabel.Text = "Could Not Find Movie";
                }
                else{
                    movieTitleLabel.Text = result;
                }


            };
        }

    }
}

