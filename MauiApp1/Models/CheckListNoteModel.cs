using MauiApp1.DTOs;
using MauiApp1.Utilities;
using MP01.Models;
using SQLite;

namespace MauiApp1.Models;

public class CheckListNoteModel : NoteModel
{
    [Ignore]
    public override NoteType Type => NoteType.CheckList;
    
    public override NoteModel EditNote(NoteDto dto)
    { 
        base.EditNote(dto);
       
        var checkListNoteDto = dto as CheckListNoteDto;


        ClearCheckBoxes();

        if (checkListNoteDto != null)
            for(int i = 0; i < checkListNoteDto.BoxTitles.Count(); i++)
            {
                AddCheckBox(checkListNoteDto.BoxTitles.ElementAt(i), checkListNoteDto.BoxStatuses.ElementAt(i));
            }
            
        return this;
    }

    public static CheckListNoteModel CreateNote(NoteDto dto)
    {
        var checkListNoteDto = dto as CheckListNoteDto;
        
        if(checkListNoteDto is null)
            throw new ArgumentException("Invalid note type");
        
        var checkListNoteModel = GetNoteBase<CheckListNoteModel>(dto);
        
        for(int i = 0; i < checkListNoteDto.BoxTitles.Count(); i++)
        {
            checkListNoteModel.AddCheckBox(checkListNoteDto.BoxTitles.ElementAt(i), checkListNoteDto.BoxStatuses.ElementAt(i));
        }
        
        return checkListNoteModel;
    }
    
    
    
    //Asocjacje  Kompozycja 
    
    private readonly List<CheckBox> _checkBoxes = new();

    public IReadOnlyList<CheckBox> GetCheckBoxes() => _checkBoxes.AsReadOnly();


    private void AddCheckBox(string? title, bool status)
    {
        _checkBoxes.Add(new CheckBox
        {
            Title = title,
            Status = status
        });
    }

    
    public void RemoveCheckBoxAt(int index)
    {
        if (index >= 0 && index < _checkBoxes.Count)
        {
            _checkBoxes.RemoveAt(index);
        }
    }
    
    private void ClearCheckBoxes()
    {
        _checkBoxes.Clear();
    }

    public int GetCheckListCount()
    {
        return _checkBoxes.Count;
    }


    public IEnumerable<string> GetBoxTitles()
    {
        return _checkBoxes.Select(b => b.Title);
    }
    
    public List<bool> GetBoxStatuses()
    {
        return _checkBoxes.Select(b => b.Status).ToList();
    }
    //
    
    
    public override void UnlinkAssociations()
    {
        base.UnlinkAssociations();
        ClearCheckBoxes();
    }
   


    
}

public class CheckBox 
{
    public required string? Title { get; set; }
    public bool Status { get; set; } = false;
}