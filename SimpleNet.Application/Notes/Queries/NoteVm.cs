using SimpleNet.Application.Users.Queries.GetUsersByName;
using SimpleNet.Domain.Models;

namespace SimpleNet.Application.Notes.Queries;

public class NoteVm
{
    public string Body { get; set; }
    public bool IsPersonal { get; set; }
    public DateTime DateTime { get; set; }
    public UserMinVm User { get; set; }

    public static explicit operator NoteVm(Note note)
    {
        return new NoteVm
        {
            Body = note.Body,
            IsPersonal = note.IsPersonal,
            DateTime = note.DateTime,
            User = (UserMinVm)note.User
        };
    }
}