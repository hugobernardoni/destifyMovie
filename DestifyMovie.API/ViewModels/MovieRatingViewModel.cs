namespace DestifyMovie.API.ViewModels
{
    public class MovieRatingViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
