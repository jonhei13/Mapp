using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using MovieDownload;

namespace MovieSearch.iOS.ApiService
{
    public class MovieService
    {
        private ImageDownloader _ImageDownloader;
        private IApiMovieRequest _movieApi;
        private List<MovieDetails> _movies;
        public MovieService(ImageDownloader downloader)
        {
            _ImageDownloader = downloader;
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            _movies = new List<MovieDetails>();
        }

        public async Task<List<MovieDetails>> getMoviesByTitle(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new List<MovieDetails>();
            }
            ApiSearchResponse<MovieInfo> response = await _movieApi.SearchByTitleAsync(name);
            _movies = await getMovies(response.Results);
            return _movies;   
        }

        public async Task<List<MovieDetails>> getTopRatedMovies()
        {
            ApiSearchResponse<MovieInfo> response = await _movieApi.GetTopRatedAsync();
            _movies = await getMovies(response.Results);
            return _movies;
        }

        private async Task<List<MovieDetails>> getMovies(IReadOnlyList<MovieInfo> response)
        {
            var result = (from x in response select x).ToList();
            var Movies = new List<MovieDetails>();
            var cancelToke = new CancellationTokenSource();
            CancellationToken token = cancelToke.Token;
            foreach (MovieInfo info in result)
            {
                var localPath = "";
                if (!string.IsNullOrEmpty(info.PosterPath))
                {
                    localPath = _ImageDownloader.LocalPathForFilename(info.PosterPath);
                    await _ImageDownloader.DownloadImage(info.PosterPath, localPath, token);
                }

                var MovieDetails = new MovieDetails()
                {
                    Title = info.Title,
                    Id = info.Id,
                    Genre = (from x in info.Genres select x.Name).ToList(),
                    ReleaseDate = info.ReleaseDate,
                    Description = info.Overview,
                    ImagePath = localPath
                };

                MovieDetails.actors = await getCredits(MovieDetails.Id);
                if (MovieDetails != null)
                {
                    _movies.Add(MovieDetails);
                }
            }
            return _movies;
        }
        private async Task<List<string>> getCredits(int? movieId)
        {
            ApiQueryResponse<MovieCredit> response = await _movieApi.GetCreditsAsync(movieId.Value);
            var actors = (from x in response.Item.CastMembers select x.Name).Take(3).ToList();
            return actors;
        }

      
    }
}
