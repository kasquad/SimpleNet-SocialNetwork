using SimpleNet.Application.Users.Queries.GetMyUserPage;
using SimpleNet.Domain.Models;
using SimpleNet.ViewModels.Notes;

namespace SimpleNet.ViewModels.User;

public class MyUserPageVm 
{
    public string FullName { get; set; }
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public CreateNoteVm CreateNoteVm { get; set; } = new();
    
    public static explicit operator MyUserPageVm(MyUserPageDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new MyUserPageVm
        {
            FullName = dto.UserName,
            // Notes = dto.Notes.Select(n => (NoteVm)n).ToList()
            Notes = dto.Notes
        };
    }
}