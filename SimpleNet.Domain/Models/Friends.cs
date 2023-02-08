using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleNet.Domain.Models;

public class Friends
{
    [Required]
    public Guid ProposingUserId { get; set; }
    public virtual User ProposingUser { get; set; }
    public Guid AcceptingUserId { get; set; }
    [Required]
    public virtual User AcceptingUser { get; set; }
    [Required]
    public bool IsAccept { get; set; }
    
    
    [NotMapped]
    public virtual Chat Chat { get; set; }
    [NotMapped]
    public Guid ChatId { get; set; }
}