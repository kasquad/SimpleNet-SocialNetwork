using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleNet.Domain.Models;

public class Note
{
    [Key]
    public Guid Id { get; set; }
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    [Required]
    [MaxLength(700)]
    public string Body { get; set; }
    [Required]
    public DateTime DateTime { get; set; }
    [Required] 
    public bool IsPersonal { get; set; } 
}