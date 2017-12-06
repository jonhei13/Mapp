using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using Foundation;
using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using MovieSearch.MovieApiService;
using MovieSearch.iOS.ApiService;
using MovieSearch.Models;

namespace MovieSearch.iOS.Controllers
{
    public class MovieController : UIViewController
    {
        private const double startX = 20;
        private const double startY = 80;
        private const double height = 50;

        private MovieSettings _apiConnection;
        private MovieService _apiService;

        private List<MovieDetails> _nameList;
        public MovieController(MovieSettings ApiConnection, MovieService ApiService)
        {
            _apiConnection = ApiConnection;
            _apiService = ApiService;
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search, 0);

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            base.ViewDidLoad();

            this.View.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background"));

            UILabel promptLabel = PromptLabel();

            UITextField nameField = NameField();
            TextFieldCustom(nameField);

            UILabel movieTitleLabel = MovieTitleLabel();

            var navigationButton = NavigationButton(nameField, movieTitleLabel);

            this.View.AddSubviews(new UIView[] { promptLabel, nameField, movieTitleLabel, navigationButton });
        }

        private static void TextFieldCustom(UITextField nameField)
        {
            nameField.Layer.BorderWidth = 2.0f;
            nameField.Layer.BorderColor = UIColor.Black.ColorWithAlpha(0.5f).CGColor;
            nameField.Layer.CornerRadius = 5;
        }

        private UIButton NavigationButton(UITextField nameField, UILabel MovieTitleLabel)
        {

            UIButton.Appearance.SetTitleColor(UIColor.Black, UIControlState.Normal);
            var loading = CreateLoading();
            this.View.AddSubview(loading);

            var navigateButton = UIButton.FromType(UIButtonType.Plain);
            CustomTransParentButton(navigateButton);

            navigateButton.TouchUpInside += async (sender, args) =>
            {
                await NavButtonClick(nameField, MovieTitleLabel, loading);
            };
            return navigateButton;
        }

        private async System.Threading.Tasks.Task NavButtonClick(UITextField nameField, UILabel MovieTitleLabel, UIActivityIndicatorView loading)
        {
            loading.StartAnimating();
            nameField.ResignFirstResponder();
            _nameList = await _apiService.GetMoviesByTitle(nameField.Text);
            MovieTitleLabel.Text = "";
            loading.StopAnimating();
            this.NavigationController.PushViewController(new TableController(_nameList), true);
            this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("Movie Search",
                                                                    UIBarButtonItemStyle.Plain, null);
        }

        private void CustomTransParentButton(UIButton navigateButton)
        {
            navigateButton.Layer.BorderWidth = 2.0f;
            var highlightedAttributedTitle = new NSAttributedString("Get Movies", foregroundColor: UIColor.LightGray);
            navigateButton.SetAttributedTitle(highlightedAttributedTitle, UIControlState.Highlighted);
            navigateButton.TintColor = UIColor.Red;
            navigateButton.Frame = new CGRect((System.nfloat)startX, (System.nfloat)(startY + 4 * height), this.View.Bounds.Width - 2 * startX, height);
            navigateButton.SetTitle("Get Movies", UIControlState.Normal);
            navigateButton.SetTitleColor(UIColor.LightGray, UIControlState.Normal);
            navigateButton.TitleLabel.Font = UIFont.FromName("Helvetica", 18f);
            navigateButton.TitleLabel.TextColor = UIColor.LightGray;
            navigateButton.Layer.BackgroundColor = UIColor.Black.ColorWithAlpha(0.7f).CGColor;
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
                BorderStyle = UITextBorderStyle.RoundedRect,
                BackgroundColor = UIColor.Black.ColorWithAlpha(0.2f),
                AttributedPlaceholder = new NSAttributedString("Search Movies", font: UIFont.FromName("Helvetica", 18f), foregroundColor: UIColor.LightGray),
                TextColor = UIColor.LightGray,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.FromName("Helvetica", 18f)
            };
        }

        private UILabel PromptLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, startY, this.View.Bounds.Width - 2 * startX, height),
                Text = "Enter words in a movie title: ",
                Font = UIFont.FromName("Helvetica-Bold", 20f),
                TextColor = UIColor.LightGray
            };
        }

        UIActivityIndicatorView CreateLoading()
        {
            var i = new UIActivityIndicatorView();
            i.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.WhiteLarge;
            i.Frame = new System.Drawing.RectangleF((float)startX, (float)(startY + 100), (float)(this.View.Bounds.Width - 2 * startX), (float)height);
            i.Color = UIColor.Gray;
            return i;
        }

    }
}
