namespace DestifyMovie.API.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public List<ActorBasicViewModel> Actors { get; set; } = new();
        public List<MovieRatingViewModel> Ratings { get; set; } = new();
    }
}
