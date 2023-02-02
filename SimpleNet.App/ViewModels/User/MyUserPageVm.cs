using SimpleNet.Application.Users.Queries.GetUserPage;
using SimpleNet.DataModels.User;
using SimpleNet.ViewModels.Notes;

namespace SimpleNet.ViewModels.User;

public class MyUserPageVm : UserPageVm
{
    public CreateNoteVm CreateNoteVm { get; set; } = new();
    
    public static explicit operator MyUserPageVm(UserPageDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new MyUserPageVm
        {
            UserFullName = dto.UserName,
            Notes = dto.Notes.Select(n => (NoteVm)n).ToList()
        };
    }
}