using MauiApp1.Models;

namespace MauiApp1.ViewModels.Notes;

public sealed class AccountNoteItemViewModel : NoteItemViewModel
{
    private AccountNoteModel? _model;
    public AccountNoteItemViewModel(NoteModel data) 
        : base(data)
    {
        if (data is not AccountNoteModel accountModel)
            throw new ArgumentException("Type is not TextNoteModel.", nameof(data));

        _model = accountModel;
        ReloadFields();
    }

    protected override void ReloadFields()
    {
        base.ReloadFields();

        Fields.Add(new CustomFieldViewModel(
            "Login",
            _model!.Login,
            null,
            true
        ));
        Fields.Add(new CustomFieldViewModel(
            "Password",
            _model!.Password,
            null,
            true
        ));
    }
}