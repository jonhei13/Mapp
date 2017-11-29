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
                Font = UIFont.FromName("Helvetica-Bold", 14f)
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
                Font = UIFont.FromName("Helvetica", 12f),
                TextColor = UIColor.Gray
            };
        }


        private UITextView MovieDescription()
        {
            return new UITextView()
            {
                Frame = new CGRect(55, 150, this.View.Bounds.Width - 65, height*4),
                Text = this.Movie.Description,
                Font = UIFont.FromName("Helvetica", 10f)
            };
        }

        private UIImageView posterImg() {
            return new UIImageView()
            {
                Frame = new CGRect(5, 150, 50, 50),
                Image = UIImage.FromFile(Movie.ImagePath)
            };
        }
    }
}
