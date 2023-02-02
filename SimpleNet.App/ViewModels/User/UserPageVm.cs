using SimpleNet.Application.Users.Queries.GetUserPage;
using SimpleNet.Domain.Models;
using SimpleNet.ViewModels.Notes;

namespace SimpleNet.DataModels.User;

public class UserPageVm
{
    public string UserFullName { get; set; }
    public ICollection<NoteVm> Notes { get; set; } = new List<NoteVm>();
    
    public static explicit operator UserPageVm(Application.Users.Queries.GetUserPage.UserPageDto dto)
    {
        return new UserPageVm
        {
            UserFullName = dto.UserName,
            Notes = dto.Notes.Select(n => (NoteVm)n).ToList()
        };
    }
}