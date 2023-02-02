using SimpleNet.Domain.Models;

namespace SimpleNet.ViewModels.Notes;

public class NoteVm
{
    public string Body { get; set; }
    public bool IsPersonal { get; set; }
    public DateTime DateTime { get; set; }

    public static explicit operator NoteVm(Note model)
    {
        return new NoteVm
        {
            Body = model.Body,
            IsPersonal = model.IsPersonal,
            DateTime = model.DateTime
        };
    }
}