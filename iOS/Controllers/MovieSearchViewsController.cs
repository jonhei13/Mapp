using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using Foundation;
using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using MovieSearch.iOS.ApiService;

namespace MovieSearch.iOS.Controllers
{
    public class MovieSearchViewsController : UIViewController
    {
        private const double startX = 20;
        private const double startY = 80;
        private const double height = 50;

        private MovieSettings _apiConnection;
        private MovieService _apiService;

        private List<MovieDetails> _nameList;
        public MovieSearchViewsController(MovieSettings ApiConnection, MovieService ApiService)
        {
            _apiConnection = ApiConnection;
            _apiService = ApiService;
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

            UILabel promptLabel = PromptLabel();

            UITextField nameField = NameField();

            UILabel movieTitleLabel = MovieTitleLabel();

            var navigationButton = NavigationButton(nameField, movieTitleLabel);

            this.View.AddSubviews(new UIView[] { promptLabel, nameField,movieTitleLabel, navigationButton });
        }

        private UIButton NavigationButton(UITextField nameField, UILabel MovieTitleLabel)
        {

            UIButton.Appearance.SetTitleColor(UIColor.Black, UIControlState.Normal);
            var loading = CreateLoading();
            this.View.AddSubview(loading);

            var navigateButton = UIButton.FromType(UIButtonType.Plain);
            navigateButton.Layer.BorderWidth = 2.0f;
            navigateButton.Layer.BorderColor = UIColor.LightGray.CGColor;
            navigateButton.ShowsTouchWhenHighlighted = true;
            navigateButton.Frame = new CGRect((System.nfloat)startX, (System.nfloat)(startY + 4 * height), this.View.Bounds.Width - 2 * startX, height);
            navigateButton.SetTitle("Get Movies", UIControlState.Normal);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                loading.StartAnimating();
                nameField.ResignFirstResponder();
                _nameList = await _apiService.getMovies(nameField.Text);
                MovieTitleLabel.Text = "";
                loading.StopAnimating();
                this.NavigationController.PushViewController(new nameController(_nameList), true);
                this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("Movie Search",
                                                                        UIBarButtonItemStyle.Plain, null);
            };
            return navigateButton;
        }


        private UILabel MovieTitleLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, startY + 2 * height, this.View.Bounds.Width - 2 + startX, height),

            };
        }

        private UITextField NameField()
        {
            return new UITextField()
            {
                Frame = new CGRect(startX, startY + height, this.View.Bounds.Width - 2 * startX, height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
        }

        private UILabel PromptLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, startY, this.View.Bounds.Width - 2 * startX, height),
                Text = "Enter Words In Movie Title: "
            };
        }

        UIActivityIndicatorView CreateLoading()
        {
            var i = new UIActivityIndicatorView();
            i.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge;
            i.Frame = new System.Drawing.RectangleF((float)startX, (float)(startY + 5 * height), (float)(this.View.Bounds.Width - 2 * startX), (float)height);
            i.Color = UIColor.Gray;
            return i;
        }

    }
}

