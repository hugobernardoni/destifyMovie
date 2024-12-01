using System.ComponentModel.DataAnnotations;

namespace DestifyMovie.API.ViewModels;

public class ActorInputView
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
