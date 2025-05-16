using MauiApp1.Models;

namespace MauiApp1.ViewModels.Notes;

public sealed class TextNoteItemViewModel : NoteItemViewModel
{
    private TextNoteModel? _model;
    public TextNoteItemViewModel(NoteModel data, string? categoryName) 
        : base(data, categoryName)
    {
        if (data is not TextNoteModel textModel)
            throw new ArgumentException("Type is not TextNoteModel.", nameof(data));

        _model = textModel;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        Fields.Add(new CustomFieldViewModel(
            "Text",
            _model.TextContent,
            s => _model.TextContent = s!,
            IsReadOnly
        ));
    }
}