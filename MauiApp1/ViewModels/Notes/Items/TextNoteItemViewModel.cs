using MauiApp1.Interfaces;
using MauiApp1.Models;
using MauiApp1.View;
using MauiApp1.ViewModels.Notes.Items;
using MauiApp1.Views.ElementsViews;
using MauiApp1.Views.Notes;

namespace MauiApp1.ViewModels.Notes;

public sealed class TextNoteItemViewModel : NoteItemViewModel, ICompositeViewModel, IEventHandler
{
    private TextNoteModel? _model;

    private readonly TextBlocksViewModel _textBlocksViewModel;
    public CustomQuotesContainerViewModel CustomQuotesContainerViewModel { get; }
    public TextNoteItemViewModel(NoteModel data, TextBlocksViewModel textBlocksViewModel, CustomQuotesContainerViewModel customQuotesContainerViewModel) 
        : base(data)
    {
        if (data is not TextNoteModel textModel)
            throw new ArgumentException("Type is not TextNoteModel.", nameof(data));
        _textBlocksViewModel = textBlocksViewModel;
        CustomQuotesContainerViewModel = customQuotesContainerViewModel;
        _model = textModel;
        CustomQuotesContainerViewModel.CreateButtonViewModel.CreateNoteWithSourceViewModel.TextNote = _model;
        CustomQuotesContainerViewModel.CreateButtonViewModel.CreateNoteWithSourceViewModel.OnModelCreated += OnQuotesChanged;
        CustomQuotesContainerViewModel.DeleteButtonViewModel.DeleteNoteWithSourceViewModel.OnEventInvoke += OnQuotesChanged;
        CustomQuotesContainerViewModel.DeleteButtonViewModel.DeleteNoteWithSourceViewModel.SetModel(_model!);
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        _textBlocksViewModel.Blocks.Clear();

        for (int i = 0; i < _model!.GetBlocksTitles().Count(); i++)
        {
            _textBlocksViewModel.Blocks.Add(new CustomFieldViewModel(
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
                new CustomInfoStruct("Source Note => " , _ns[i].SourceNote?.Title),
                new CustomInfoStruct("Quote: " , _ns[i].Quote),
                new CustomInfoStruct("Comment: " , _ns[i].Comment)
            }));
        }
       
    }

    private void OnQuotesChanged()
    {
        CustomQuotesContainerViewModel.DeleteButtonViewModel.DeleteNoteWithSourceViewModel.SetModel(_model!);
        ReloadFields();
        OnEventInvoke?.Invoke();
    }

    public IEnumerable<BaseView> GetEmbeddedViews()
    {
        List<BaseView> views = [
            new TextBlocksView(_textBlocksViewModel),
            new CustomQuotesContainerView(CustomQuotesContainerViewModel)
        ];
        
        return views;
        
    }

    public event Action? OnEventInvoke;
}