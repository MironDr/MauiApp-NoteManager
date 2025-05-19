using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.Views.Notes;
using MP01.Models;

namespace MauiApp1.ViewModels.Notes;


public sealed class SourceNoteItemViewModel : NoteItemViewModel, ICompositeViewModel
{
    private SourceNoteModel? _model;
    
    public CustomQuotesContainerViewModel CustomQuotesContainerViewModel { get; }
 
    public SourceNoteItemViewModel(NoteModel data, string? categoryName, CustomQuotesContainerViewModel customQuotesContainerViewModel) 
        : base(data, categoryName)
    {
        if (data is not SourceNoteModel sourceModel)
            throw new ArgumentException("Type is not SourceNoteModel.", nameof(data));
        CustomQuotesContainerViewModel = customQuotesContainerViewModel;
        _model = sourceModel;
        CustomQuotesContainerViewModel.CreateButtonViewModel.CreateNoteWithSourceViewModel.SourceNote = _model;
        CustomQuotesContainerViewModel.CreateButtonViewModel.CreateNoteWithSourceViewModel.OnModelCreated += ReloadFields;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        Fields.Add(new CustomFieldViewModel("Source", _model!.Source, null,true));
        Fields.Add(new CustomFieldViewModel("Author", _model!.Author, null,true));
        Fields.Add(new CustomFieldViewModel("Published Date", _model!.PublishedDate.ToShortDateString(), null,true));
        Fields.Add(new CustomFieldViewModel("Source Type", _model!.SourceType.ToString(), null,true));
        
        List<NoteWithSourceModel> _ns = _model!.GetNotesLinks();
        
        CustomQuotesContainerViewModel.Fields.Clear();
        
        for (int i = 0; i < _ns.Count(); i++)
        {
            CustomQuotesContainerViewModel.Fields.Add(new CustomInfoViewModel(new[]
            {
                new CustomInfoStruct("Linked Note => " , _ns[i].Note?.Title),
                new CustomInfoStruct("Quote: " , _ns[i].Quote),
                new CustomInfoStruct("Comment: " , _ns[i].Comment)
            }));
        }

        
        
    }


    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new CustomQuotesContainerView(CustomQuotesContainerViewModel)
        ];
        
        return views;
    }
}