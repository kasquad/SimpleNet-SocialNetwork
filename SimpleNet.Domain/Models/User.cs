using System.ComponentModel.DataAnnotations;

namespace SimpleNet.Domain.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
    [Required]
    [StringLength(32)]
    public string HashedPassword { get; set; }
    
    public virtual ICollection<Note> Notes { get; set; }
    public virtual ICollection<User> IncomingFriends { get; set; }
    public virtual ICollection<User> OutgoingFriends { get; set; }

}