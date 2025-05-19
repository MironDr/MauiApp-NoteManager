using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.Views.Notes;
using MP01.Models;

namespace MauiApp1.ViewModels.Notes;

public sealed class TextNoteItemViewModel : NoteItemViewModel, ICompositeViewModel
{
    private TextNoteModel? _model;
    
    public TextBlocksViewModel TextBlocksViewModel { get; }
    public CustomQuotesContainerViewModel CustomQuotesContainerViewModel { get; }
    public TextNoteItemViewModel(NoteModel data, string? categoryName, TextBlocksViewModel textBlocksViewModel, CustomQuotesContainerViewModel customQuotesContainerViewModel) 
        : base(data, categoryName)
    {
        if (data is not TextNoteModel textModel)
            throw new ArgumentException("Type is not TextNoteModel.", nameof(data));
        TextBlocksViewModel = textBlocksViewModel;
        CustomQuotesContainerViewModel = customQuotesContainerViewModel;
        _model = textModel;
        CustomQuotesContainerViewModel.CreateButtonViewModel.CreateNoteWithSourceViewModel.TextNote = _model;
        CustomQuotesContainerViewModel.CreateButtonViewModel.CreateNoteWithSourceViewModel.OnModelCreated += ReloadFields;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        TextBlocksViewModel.Blocks.Clear();

        for (int i = 0; i < _model!.GetBlocksTitles().Count(); i++)
        {
            TextBlocksViewModel.Blocks.Add(new CustomFieldViewModel(
                _model!.GetBlocksTitles().ElementAt(i),
                _model!.GetBlocksContents().ElementAt(i),
                null,
                true
            ));
        }

        List<NoteWithSourceModel> _ns = _model!.GetNotesLinks();
        
        CustomQuotesContainerViewModel.Fields.Clear();
        
        for (int i = 0; i < _ns.Count(); i++)
        {
            CustomQuotesContainerViewModel.Fields.Add(new CustomInfoViewModel(new[]
            {
                new CustomInfoStruct("Source Note => " , _ns[i].Note?.Title),
                new CustomInfoStruct("Quote: " , _ns[i].Quote),
                new CustomInfoStruct("Comment: " , _ns[i].Comment)
            }));
        }
        
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new TextBlocksView(TextBlocksViewModel),
            new CustomQuotesContainerView(CustomQuotesContainerViewModel)
        ];
        
        return views;
        
    }
}