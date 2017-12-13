using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieSearch.Models;

namespace MovieSearch.MovieApiService
{
    public class MovieSearchService
    {
        private IApiMovieRequest _movieApi;

        public MovieSearchService()
        {
            MovieSettings ApiConnection = new MovieSettings();
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
         
        }

        public async Task<List<MovieDetails>> GetMoviesByTitle(string name)
        {
            List<MovieDetails> _movieList = new List<MovieDetails>();
            if (string.IsNullOrEmpty(name))
            {
                return new List<MovieDetails>();
            }
            ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(name);
            _movieList = await GetMovies(response.Results);
            return _movieList;
        }

        public async Task<List<MovieDetails>> GetTopRatedMovies()
        {
            List<MovieDetails> _movieList = new List<MovieDetails>();
            ApiSearchResponse<MovieInfo> response = await _movieApi.GetTopRatedAsync();
            _movieList = await GetMovies(response.Results);
            return _movieList;
        }
        public async Task<MovieDetails> GetDetailedMovie(MovieDetails movie)
        {
            ApiQueryResponse<Movie> movieDetailedResponse = await _movieApi.FindByIdAsync(movie.Id);
            movie.ImagePoster = "http://image.tmdb.org/t/p/original/" + movieDetailedResponse.Item.BackdropPath;
            movie.Description = movieDetailedResponse.Item.Overview;
            movie.Genre = (from x in movieDetailedResponse.Item.Genres select x.Name).ToList();
            movie.RunTime = movieDetailedResponse.Item.Runtime + "Min";
            return movie;

        }
        private async Task<List<MovieDetails>> GetMovies(IReadOnlyList<MovieInfo> response)
        {
            List<MovieDetails> _movieList = new List<MovieDetails>();
            var result = (from x in response select x).ToList();
            var Movies = new List<MovieDetails>();

            foreach (MovieInfo info in result)
            {
                var MovieDetails = new MovieDetails()
                {
                    Title = info.Title + " (" + info.ReleaseDate.Year + ")",
                    Id = info.Id,
                    Genre = (from x in info.Genres select x.Name).ToList(),
                    ImagePath = "http://image.tmdb.org/t/p/original/" + info.PosterPath
                };
                _movieList.Add(MovieDetails);
            }
            return _movieList;
        }
        public async Task<List<MovieDetails>> getActors(List<MovieDetails> movies)
        {
            foreach (MovieDetails mov in movies)
            {
                ApiQueryResponse<MovieCredit> response = await _movieApi.GetCreditsAsync(mov.Id);
                try
                {
                    var actors = (from x in response.Item.CastMembers select x.Name).Take(3).ToList();
                    string a = "";
                    foreach (var x in actors)
                    {
                        if (actors.IndexOf(x) == actors.Count - 1)
                        {
                            a += x;
                        }
                        else
                        {
                            a += x + ", ";
                        }
                    }
                    mov.Actors = a;

                }
                catch (NullReferenceException e)
                {
                    //WriteLine(e);
                    mov.Actors = "";
                }

            }
            return movies;
 

        }
    }
}
