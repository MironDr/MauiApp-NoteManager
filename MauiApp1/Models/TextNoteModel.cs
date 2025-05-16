using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class TextNoteModel : NoteModel
{
    public string? TextContent { get; set; }
    
    public override NoteType Type => NoteType.Text;

    public override NoteModel EditNote(NoteDto dto)
    { 
        base.EditNote(dto);
        TextContent = (dto as TextNoteDto)?.TextContent;
        return this;
    }

    public new static TextNoteModel CreateNote(NoteDto dto)
    {
        var textDto = dto as TextNoteDto;
        return new TextNoteModel
        {
            Id = _idCounter++,
            Title = textDto.Title,
            Description = textDto.Description,
            Category = textDto.Category,
            TextContent = textDto.TextContent
        };
    }
}