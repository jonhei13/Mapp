using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace MovieSearch.iOS
{
    public class MovieSettings : IMovieDbSettings
    {
        public string ApiKey => "0865a26248444f496c23cfd76c599a2c";


        public string ApiUrl => "http://api.themoviedb.org/3/";
        
        public MovieSettings()
        {
            MovieDbFactory.RegisterSettings(ApiKey,ApiUrl);
          
        }


        public async Task<string> getMovie(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return "Please Enter A Title";
            }
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(name);

            var result = (from x in response.Results select x.Title).FirstOrDefault();
            if (result == null)
            {
                return "Could Not Find Movie";
            }
            else
            {
                return result;
            }
        }
        public async Task<List<MovieDetails>> getMovies(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<MovieDetails>();
            }
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(name);

            var result = (from x in response.Results select x).ToList();
            var Movies = new List<MovieDetails>();

            foreach (MovieInfo info in result)
            {
                var MovieDetails = new MovieDetails(){
                    Title = info.Title,
                    Id = info.Id,
                    Genre = (from x in info.Genres select x.Name).ToList(),
                    ReleaseDate = info.ReleaseDate,
                    Description = info.Overview
                };
                MovieDetails.actors = await getCredits(MovieDetails.Id);
                Movies.Add(MovieDetails);

            }
            return Movies;
        }
        public async Task<List<string>> getCredits(int movieId)
        {

            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiQueryResponse<MovieCredit> response = await movieApi.GetCreditsAsync(movieId);


            var actors = (from x in response.Item.CastMembers select x.Name).Take(3).ToList();

            return actors;

        }

     

    }
}
