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
            this.View.BackgroundColor = UIColor.White;

            var title = MovieTitleLabel();
            var genres = GenresLabel();
            var description = MovieDescription();
            var img = posterImg();

            this.View.AddSubviews(new UIView[ ] { title, description, genres, img });

        }

        private UILabel MovieTitleLabel()
        {
            return new UILabel()
            {
                Frame = new CGRect(startX, startY, this.View.Bounds.Width - 2 * startX, height),
                Text = this.Movie.Title + " (" + this.Movie.ReleaseDate.Year + ")",
                Font = UIFont.FromName("Helvetica-Bold", 16f)
            };
        }

        private UILabel GenresLabel()
        {
            string genres ="";
            foreach (var x in this.Movie.Genre)
            {
                genres += (x + ", ");
            }
            return new UILabel()
            {
                Frame = new CGRect(startX, 100, this.View.Bounds.Width - 2 * startX, height),
                Text = genres,
                Font = UIFont.FromName("Helvetica", 13f),
                TextColor = UIColor.Gray
            };
        }


        private UITextView MovieDescription()
        {
            return new UITextView()
            {
                Frame = new CGRect(65, 150, this.View.Bounds.Width - 75, height*4),
                Text = this.Movie.Description,
                Font = UIFont.FromName("Helvetica", 11f)
            };
        }

        private UIImageView posterImg() {
            return new UIImageView()
            {
                Frame = new CGRect(5, 150, 60, 60),
                Image = UIImage.FromFile(Movie.ImagePath)
            };
        }
    }
}
