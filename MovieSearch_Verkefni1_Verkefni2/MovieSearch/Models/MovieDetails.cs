using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieSearch.Models
{
    public class MovieDetails : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _actors;
        public int Id
        {
            get;
            set;
        }
        public MovieDetails()
        {
            _actors = "";
        }
        public string Title
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
        public string Genre
        {
            get;
            set;
        }
        public string Actors
        {
            get { return this._actors;  }
            set
            {
                if (value != null)
                {
                    this._actors = value;
                    OnPropertyChanged();
                }
     
            }
        }
        public string ImagePath
        {
            get;
            set;
        }
        public string RunTime { get; set; }
        public string ImagePoster { get; set; }
        public string BackDropText { get; set; }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
