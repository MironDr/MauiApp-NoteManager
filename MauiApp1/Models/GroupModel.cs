using MauiApp1.DTOs;

namespace MauiApp1.Models;

public class GroupModel : BaseModel
{


    private NoteModel? _mainNote;
    public string GroupName { get; set; }
    
    private GroupModel(string groupName)
    {
        Id = _idCounter++;
        GroupName = groupName;
    }

    //Asocjacje Kwalifikowana
    private readonly List<NoteModel> Notes = new();
    
    public void RemoveNote(NoteModel note)
    {
        if (Notes.Contains(note))
        {
            if(note.Id == _mainNote?.Id)
                RemoveMainNote();
            Notes.Remove(note);
        }
        
        if (note.Group == this)
        {
            note.Group = null;
        }
    }

    public void AddNoteToGroup(NoteModel note)
    {
        if (!Notes.Contains(note) && note.Category == null && Notes.Count < 4)
        {
            Notes.Add(note);
        }
        
        if(note.Group != this)
            note.Group = this;
    }
    
    public List<NoteModel> GetNotes()
    {
        return Notes.ToList();
    }
    //


    public void AddMainNote(NoteModel note)
    {
        if(!Notes.Contains(note))
            return;
        
        _mainNote = note;
        
        if(!note.IsMainInGroup)
         note.IsMainInGroup = true;
    }

    public void RemoveMainNote()
    {
        if(_mainNote == null)
            return;

        if(_mainNote.IsMainInGroup)
            _mainNote.IsMainInGroup = false;
        
        _mainNote = null;
        
    }
    

    public NoteModel? GetMainNote()
    {
        return _mainNote;
    }
    
    public static GroupModel CreateGroup(GroupDto groupDto)
    {
        return new GroupModel(groupDto.GroupName);
    }

}