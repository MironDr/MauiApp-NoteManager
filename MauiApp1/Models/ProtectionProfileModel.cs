using MauiApp1.DTOs;
using MauiApp1.Utilities;

namespace MauiApp1.Models;

public class ProtectionProfileModel : BaseModel
{
    public string ProfileName { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; }

    public byte[] Salt { get; set; }
    
    private readonly Dictionary<int, NoteModel> _notes = new();
    
    public void RemoveNote(int id)
    {
        _notes.TryGetValue(id, out var model);
       
        _notes.Remove(id);
        
        if(model != null)
            if(model.ProtectionProfile == this)
                model.ProtectionProfile = null;
        
    }

    public void AddNoteToGroup(NoteModel note)
    {
        _notes.TryAdd(note.Id, note);

        if(note.ProtectionProfile != this)
            note.ProtectionProfile = this;
    }

    public Dictionary<int, NoteModel> GetNotes()
    {
        return _notes.ToDictionary(note => note.Key, note => note.Value);
    }
    
    private ProtectionProfileModel(string profileName, byte[] passwordHash, byte[] salt)
    {
        ProfileName = profileName;
        PasswordHash = passwordHash;
        Salt = salt;
    }
    
    
    public static ProtectionProfileModel CreateProfile(ProtectionProfileDto profileDto)
    {
        ObjectUtils.CreatePasswordHash(profileDto.Password, out var hash, out var salt);
        
        ProtectionProfileModel protectionProfileModel = new ProtectionProfileModel
            (profileDto.ProfileName,
                hash,
                salt)
        {
            Id = _idCounter++
        };

        return protectionProfileModel;
        
    }

}