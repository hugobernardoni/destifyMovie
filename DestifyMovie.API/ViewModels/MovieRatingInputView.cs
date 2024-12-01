using System.ComponentModel.DataAnnotations;

namespace DestifyMovie.API.ViewModels;

public class MovieRatingInputView
{
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;
}
