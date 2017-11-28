using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using Foundation;
using DM.MovieApi.MovieDb.Movies;
using DM.MovieApi.ApiResponse;
using System.Linq;
using System.Collections.Generic;

namespace MovieSearch.iOS
{
    public class MovieSearchViewsController : UIViewController
    {
        private const double startX = 20;
        private const double startY = 80;
        private const double height = 50;

        private MovieSettings _db;

        private List<string> _nameList;
        public MovieSearchViewsController(MovieSettings db)
        {
            _db = db;

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

            //  this.View.AddSubview(promptLabel);

            UITextField nameField = NameField();

            //this.View.AddSubview(nameField);

           
            //  this.View.AddSubview(SearchTitleButton);

            var navigationButton = NavigationButton(nameField);
            UILabel movieTitleLabel = MovieTitleLabel();
            var SearchTitleButton = SearchButton(nameField, movieTitleLabel);
           // this.View.AddSubview(movieTitleLabel);

            this.View.AddSubviews(new UIView[] { promptLabel, nameField, SearchTitleButton,movieTitleLabel, navigationButton });
        }

        private UIButton NavigationButton(UITextField nameField)
        {
            var navigateButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigateButton.Frame = new CGRect((System.nfloat)startX, (System.nfloat)(startY + 4 * height), this.View.Bounds.Width - 2 * startX, height);
            navigateButton.SetTitle("See name list", UIControlState.Normal);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                nameField.ResignFirstResponder();
                _nameList = await _db.getMovies(nameField.Text);
                this.NavigationController.PushViewController(new nameController(_nameList), true);
            };
            return navigateButton;
        }

        private UIButton SearchButton(UITextField nameField, UILabel movieTitleLabel)
        {
            var SearchTitleButton = UIButton.FromType(UIButtonType.RoundedRect);
            SearchTitleButton.Frame = new CGRect(startX, 180 - startY, this.View.Bounds.Width / 2, height);
            SearchTitleButton.SetTitle("GetMovie", UIControlState.Normal);

            SearchTitleButton.TouchUpInside += async (sender, args) =>
            {
                nameField.ResignFirstResponder();
                var result = await _db.getMovie(nameField.Text);
                movieTitleLabel.Text = result;
            };
            return SearchTitleButton;
        }

        private UILabel MovieTitleLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, 230 - startY, this.View.Bounds.Width, height),

            };
        }

        private UITextField NameField()
        {
            return new UITextField()
            {
                Frame = new CGRect(startX, 130 - startY, this.View.Bounds.Width, height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };
        }

        private UILabel PromptLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, startY, this.View.Bounds.Width, height),
                Text = "Enter Words In Movie Title: "
            };
        }
    }
}

