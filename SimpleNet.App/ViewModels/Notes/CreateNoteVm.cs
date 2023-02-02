using System.ComponentModel.DataAnnotations;

namespace SimpleNet.ViewModels.Notes;

public class CreateNoteVm
{
    [MaxLength(700)]
    [MinLength(2)]
    public string Body { get; set; }
    [Required]
    public bool IsPersonal { get; set; }
}