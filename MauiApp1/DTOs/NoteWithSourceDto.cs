using MauiApp1.Base;
using MauiApp1.Models;

namespace MauiApp1.DTOs;

public class NoteWithSourceDto : BaseCommon
{
    public SourceNoteModel? SourceNote { get; set; }
    public TextNoteModel? Note { get; set; }
    public string? Quote { get; set; }
    public string? Comment { get; set; }
  
}