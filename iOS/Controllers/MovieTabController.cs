using System;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class MovieTabController : UITabBarController 
    {
        
        public override void ViewDidLoad(){
            base.ViewDidLoad();

            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.TabBar.TintColor = UIColor.Black;

            this.SelectedIndex = 0;
        }

    }
}
