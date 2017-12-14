using MovieSearch.Models;
using MovieSearchForms.ViewModels;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieSearchForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridView : ListView
    {
        private UserControl _userControl;
        private List<MovieDetails> _movieList = new List<MovieDetails>();
        public GridView()
        {
         //   this._userControl = new UserControl(this.Navigation);
            this.BindingContext = this._userControl;
            InitializeComponent();
        }
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(MovieList), typeof(List<MovieDetails>), typeof(UserControl));

        public List<MovieDetails> MovieList
        {
            get
            {
                var x = (List<MovieDetails>)GetValue(TextProperty);
                if (x == null)
                {
                    return new List<MovieDetails>();
                }
                return x;
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }
        public List<MovieDetails> adaw
        {
            get => this._movieList;

            set
            {
                this._movieList = value;
                this._userControl = new UserControl(this.Navigation, this._movieList);
                OnPropertyChanged();
            }
        }

    }
}