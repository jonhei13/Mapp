using System;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class MovieTabController : UITabBarController 
    {
        
        public override void ViewDidLoad(){
            base.ViewDidLoad();

            //tchis.TabBar.BackgroundColor = UIColor.Black;
            //this.TabBar.BackgroundImage = UIImage.FromFile("Background");

  //          this.TabBar.BackgroundColor = UIColor.Clear ;
            this.TabBar.BarTintColor = UIColor.Black;
            this.SelectedIndex = 0;
        }

    }
}
