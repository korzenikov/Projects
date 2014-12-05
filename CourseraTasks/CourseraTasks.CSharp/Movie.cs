using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class Movie
    {
        public Movie(int movieId, float rating)
        {
            Id = movieId;
            Rating = rating;
            SimilarMovies = new List<Movie>();
        }

        public int Id { get; private set; }

        public float Rating { get; private set; }

        public List<Movie> SimilarMovies { get; private set; }

        public void AddSimilarMovie(Movie movie)
        {
            SimilarMovies.Add(movie);
            movie.SimilarMovies.Add(this);
        }

        public IList<Movie> GetMovieRecommendations(int numTopRatedSimilarMovies)
        {
            var discoveredMovies = new Queue<Movie>();
            discoveredMovies.Enqueue(this);
            var exploredMovies = new HashSet<int>();
            var topRatedMovies = new List<Movie>();

            while (discoveredMovies.Count != 0)
            {
                var currentMovie = discoveredMovies.Dequeue();
                if (exploredMovies.Contains(currentMovie.Id))
                {
                    continue;
                }

                exploredMovies.Add(currentMovie.Id);

                // Check movie rating
                if (topRatedMovies.Count < numTopRatedSimilarMovies)
                {
                    topRatedMovies.Add(currentMovie);
                    BubbleUp(topRatedMovies, topRatedMovies.Count - 1);
                }
                else if (topRatedMovies[0].Rating < currentMovie.Rating)
                {
                    topRatedMovies[0] = currentMovie;
                    MoveDown(topRatedMovies, 0);
                }

                foreach (var similarMovie in currentMovie.SimilarMovies.Where(similarMovie => !exploredMovies.Contains(similarMovie.Id)))
                {
                    discoveredMovies.Enqueue(similarMovie);
                }
            }

            return topRatedMovies;
        }

        private static void BubbleUp(IList<Movie> heap, int index)
        {
            var parent = (index - 1) / 2;
            while (parent >= 0 && heap[parent].Rating > heap[index].Rating)
            {
                SwapElements(heap, parent, index);
                index = parent;
                parent = (index - 1) / 2;
            }
        }

        private static void MoveDown(IList<Movie> heap, int index)
        {
            while (true)
            {
                var left = 2 * index + 1;
                var right = 2 * index + 2;
                var minElementIndex = index;
                if (left < heap.Count && heap[left].Rating < heap[minElementIndex].Rating)
                {
                    minElementIndex = left;
                }

                if (right < heap.Count && heap[right].Rating < heap[minElementIndex].Rating)
                {
                    minElementIndex = right;
                }

                if (index == minElementIndex)
                {
                    break;
                }
                
                SwapElements(heap, minElementIndex, index);
                index = minElementIndex;
            }
        }

        private static void SwapElements<T>(IList<T> heap, int first, int second)
        {
            var temp = heap[first];
            heap[first] = heap[second];
            heap[second] = temp;
        }
    }
}
