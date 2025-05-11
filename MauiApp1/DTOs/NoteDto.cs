using MauiApp1.Base;
using MauiApp1.Models;

namespace MauiApp1.DTOs;

public class NoteDto : BaseCommon
{
    public string Title {get; init; } = string.Empty;

    public string? Description { get; init; }
    
    public CategoryModel? Category { get; set; }
}