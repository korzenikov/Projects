using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MovieTest
    {
        [TestMethod]
        public void GetMovieRecommendationsTest()
        {
            var a = new Movie(1, 1.2f);
            var b = new Movie(2, 2.4f);
            var c = new Movie(3, 3.6f);
            var d = new Movie(4, 4.8f);

            // A
            a.AddSimilarMovie(b);
            a.AddSimilarMovie(c);

            // B
            b.AddSimilarMovie(d);

            // C
            c.AddSimilarMovie(d);

            a.GetMovieRecommendations(2).Should().BeEquivalentTo(new[] { c, d }); 
            a.GetMovieRecommendations(4).Should().BeEquivalentTo(new[] { a, b, c, d }); 
            a.GetMovieRecommendations(1).Should().BeEquivalentTo(new[] { d }); 
        }
    }
}
