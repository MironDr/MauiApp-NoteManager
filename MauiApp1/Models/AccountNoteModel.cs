using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class AccountNoteModel : NoteModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public override NoteType Type => NoteType.Account;
    
    protected AccountNoteModel() : base()
    {
    }

    public override NoteModel EditNote(NoteDto dto)
    { 
        base.EditNote(dto);
        Login = ((dto as AccountNoteDto)!).Login;
        Password = ((dto as AccountNoteDto)!).Password;
        return this;
    }

    public new static AccountNoteModel CreateNote(NoteDto dto)
    {
        var textDto = dto as AccountNoteDto;
        return new AccountNoteModel
        {
            Id = _idCounter++,
            Title = textDto.Title,
            Description = textDto.Description,
            Category = textDto.Category,
            Login = textDto.Login,
            Password = textDto.Password
        };
    }
}