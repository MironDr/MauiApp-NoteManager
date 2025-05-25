using MauiApp1.DTOs;
using MauiApp1.Utilities;

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

    public static AccountNoteModel CreateNote(NoteDto dto)
    {
        var accountNoteDto = dto as AccountNoteDto;
        if(accountNoteDto is null)
            throw new ArgumentException("Invalid note type");
        
        var accountNoteModel = (AccountNoteModel)GetNoteBase(dto, new AccountNoteModel());
        
        accountNoteModel.Login = accountNoteDto.Login;
        accountNoteModel.Password = accountNoteDto.Password;
        
        return accountNoteModel;
    }

}