using System;
using CoreGraphics;
using UIKit;

namespace MovieSearch.iOS.Controllers
{
    public class MovieDetailsController : UIViewController
    {
        private const double startX = 20;
        private const double startY = 80;
        private const double height = 50;

        private readonly MovieDetails Movie;

        public MovieDetailsController(MovieDetails movie)
        {
            this.Movie = movie;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var title = MovieTitleLabel();

        }

        private UILabel MovieTitleLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, startY + 2 * height, this.View.Bounds.Width - 2 + startX, height),
                Text = this.Movie.Title
            };
        }
    }
}
