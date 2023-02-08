using System.ComponentModel.DataAnnotations;

namespace SimpleNet.Domain.Models;

public class Message
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid ChatId { get; set; }
    public virtual User Sender { get; set; }
    [Required]
    public Guid SenderId { get; set; }
}