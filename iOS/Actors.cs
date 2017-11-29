using System;
using System.Collections.Generic;

namespace MovieSearch.iOS
{
    public class Actors
    {
        public Actors()
        {
        }
        public int Id
        {
            get;
            set;
        }
        public int MovieId
        {
            get;
            set;
        }
        public List<string> actors
        {
            get;
            set;
        }
    }
}
