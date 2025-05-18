using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public sealed class TextNoteItemViewModel : NoteItemViewModel, ICompositeViewModel
{
    private TextNoteModel? _model;
    
    public TextBlocksViewModel TextBlocksViewModel { get; }
    public TextNoteItemViewModel(NoteModel data, string? categoryName, TextBlocksViewModel textBlocksViewModel) 
        : base(data, categoryName)
    {
        if (data is not TextNoteModel textModel)
            throw new ArgumentException("Type is not TextNoteModel.", nameof(data));
        TextBlocksViewModel = textBlocksViewModel;
        _model = textModel;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        Fields.Add(new CustomFieldViewModel(
            "Text",
            _model!.TextContent,
            null,
            IsReadOnly
        ));
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new TextBlocksView(TextBlocksViewModel)
        ];
        
        return views;
        
    }
}