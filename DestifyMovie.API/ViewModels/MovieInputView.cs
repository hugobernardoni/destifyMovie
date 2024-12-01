using System.ComponentModel.DataAnnotations;

namespace DestifyMovie.API.ViewModels
{
    public class MovieInputView
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        public List<int> ActorIds { get; set; } = new();
    }
}
