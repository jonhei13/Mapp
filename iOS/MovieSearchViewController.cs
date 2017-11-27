using System;
using CoreGraphics;
using UIKit;
using DM.MovieApi;
using Foundation;
using DM.MovieApi.MovieDb.Movies;

namespace MovieSearch.iOS
{
    public partial class MovieSearchViewController : UIViewController
    {
        public MovieSearchViewController() :  base("MovieSearchViewController", null)
        {
            MovieDbFactory.RegisterSettings(new MovieSettings());
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

            var greetingButton = UIButton.FromType(UIButtonType.RoundedRect);
            greetingButton.Frame = new CGRect(0, 180, this.View.Bounds.Width / 2, 50);
            greetingButton.SetTitle("GetMovie", UIControlState.Normal);

            this.View.AddSubview(greetingButton);

            var greetingLabel = new UILabel()
            {
                Frame = new CGRect(0, 230, this.View.Bounds.Width, 50),
                Text = "Hello"
            };

            greetingButton.TouchUpInside += (sender, args) =>
            {
                nameField.ResignFirstResponder();
                greetingLabel.Text = "Hello " + nameField.Text;
            };
        }

    }
}
