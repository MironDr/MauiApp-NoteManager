using System.Text.Json;
using MauiApp1.DTOs;
using MauiApp1.Utilities;
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
    
    public string CheckBoxesJson { get; set; } = "[]";


    private readonly List<CheckBox> _checkBoxes = new();
    
    public IReadOnlyList<CheckBox> GetCheckBoxes() => _checkBoxes.AsReadOnly();

    public void SyncToJson()
    {
        CheckBoxesJson = JsonSerializer.Serialize(_checkBoxes);
    }

    public void LoadFromJson()
    {
        _checkBoxes.Clear();
        var deserialized = JsonSerializer.Deserialize<List<CheckBox>>(CheckBoxesJson);
        if (deserialized != null)
            _checkBoxes.AddRange(deserialized);
    }
    
    public void AddCheckBox(string? title, bool status)
    {
        _checkBoxes.Add(new CheckBox { Title = title, Status = status });
        SyncToJson(); 
    }

    public void RemoveCheckBoxAt(int index)
    {
        if (index >= 0 && index < _checkBoxes.Count)
        {
            _checkBoxes.RemoveAt(index);
            SyncToJson();
        }
    }

    public void ClearCheckBoxes()
    {
        _checkBoxes.Clear();
        SyncToJson();
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
    
    public class CheckBox 
    {
        [Ignore]
        public required string? Title { get; set; }

        [Ignore]
        public bool Status { get; set; } 
    }
    //
    
    
    public override void UnlinkAssociations()
    {
        base.UnlinkAssociations();
        ClearCheckBoxes();
    }
   


    
}

