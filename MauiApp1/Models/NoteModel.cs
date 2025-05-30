using MauiApp1.DTOs;
using MauiApp1.Utilities;

namespace MauiApp1.Models;
public enum NoteType
{
    Text,
    Account,
    Source,
    CheckList,
  
}
public abstract class NoteModel : BaseModel
{
    public virtual NoteType Type { get; }

    private string _title = null!;
    protected NoteModel() : base() { }

    public string Title
    {
        get => _title;
        protected set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Cannot be empty.", nameof(Title));
            _title = value;
        }
    }
    
    private string? _encryptedDescription;
    public string? Description
    {
        get
        {
            if (_protectionProfile == null || string.IsNullOrEmpty(_encryptedDescription) 
                                           || !_protectionProfile.IsUnlocked 
                                           || !ObjectUtils.IsBase64String(_encryptedDescription))
                return _encryptedDescription;
            
        
            return ObjectUtils.DecryptAes(_encryptedDescription, _protectionProfile.DerivedKey!);
        }
        private set
        {
            if (_protectionProfile == null)
            {
                _encryptedDescription = value;
                return;
            }

            if (!_protectionProfile.IsUnlocked)
                throw new InvalidOperationException("Profile is locked.");

            if (value != null) _encryptedDescription = ObjectUtils.EncryptAes(value, _protectionProfile.DerivedKey!);
        }
    }
    
    public DateTime CreatedAt { get; private set; }

    
    // ---- Protection Profile ----
    
    private ProtectionProfileModel? _protectionProfile;
    
    public ProtectionProfileModel? ProtectionProfile
    {
        get => _protectionProfile;
        set
        {
            if(value?.Id == _protectionProfile?.Id)
                return;
            
            if (_protectionProfile is { IsUnlocked: false })
                throw new InvalidOperationException("Current protection profile is not unlocked.");
            
            if(value is { IsUnlocked: false })
                throw new InvalidOperationException("New protection profile is not unlocked.");

            if (_protectionProfile != null)
            {
                DecryptWithProfile();
                _protectionProfile?.RemoveNote(Id);
            }

            _protectionProfile = value;

            if (_protectionProfile != null)
            {
                _protectionProfile.AddNoteToProfile(this);
                
                EncryptWithProfile();
            }
        }
    }
    
    // ---- Group ----

    private GroupModel? _group;
    public GroupModel? Group
    {
        get => _group;
        set
        {
            if (Category != null)
                return;

            if (value != null && value.GetNotes().Count > 3)
                return;

            if (_group != null && _group.GetNotes().Contains(this))
            {
                _group.RemoveNote(this);
                IsMainInGroup = false;
            }

            _group = value;

            _group?.AddNoteToGroup(this);
        }
    }

    // ---- Category ----

    private CategoryModel? _category;
    public CategoryModel? Category
    {
        get => _category;
        set
        {
            if (Group != null)
                return;

            if (_category != null && _category.GetNotes().Contains(Id))
            {
                _category.RemoveNote(this);
            }

            _category = value;

            if (_category != null && !_category.GetNotes().Contains(Id))
            {
                _category.AddNote(this);
            }
        }
    }

    // ---- Main note flag ----

    private bool _isMainInGroup;
    public bool IsMainInGroup
    {
        get => _isMainInGroup;
        set
        {
            if (_group == null || value == _isMainInGroup)
                return;
            
            _isMainInGroup = value;
            
            if (value)
            {
                if (_group.GetMainNote() == null)
                    _group.AddMainNote(this);
            }
            else
            {
                if (_group.GetMainNote() != null)
                    _group.RemoveMainNote();
            }

           
        }
    }

    // ---- Helpers ----
    protected static NoteModel GetNoteBase(NoteDto dto, NoteModel noteModel)
    {
        if(dto.Category == null && dto.Group == null)
            throw new ArgumentNullException(nameof(dto) + " Group and Category cannot be null");
        
        noteModel.Id = _idCounter++;
        noteModel.Title = dto.Title;
        noteModel.Description = dto.Description;
        noteModel.CreatedAt = DateTime.Now;
        noteModel.ProtectionProfile = dto.ProtectionProfile;
        
        if (dto.Category != null)
        {
            noteModel.Group = null;
            noteModel.Category = dto.Category;
            noteModel.IsMainInGroup = false;
        }
        else if (dto.Group != null)
        {
            noteModel.Category = null;
            noteModel.Group = dto.Group;
            noteModel.IsMainInGroup = dto.IsMainInGroup;
        }
        

        return noteModel;
    }
    
    public virtual NoteModel EditNote(NoteDto noteDto)
    {
        if(noteDto.Category == null && noteDto.Group == null)
            throw new ArgumentNullException(nameof(noteDto) + " Group and Category cannot be null");
        
        Title = noteDto.Title;
        Description = noteDto.Description;
        
        if (noteDto.Category != null)
        {
            Group = null;
            Category = noteDto.Category;
            IsMainInGroup = false;
        }
        else if (noteDto.Group != null)
        {
            Category = null;
            Group = noteDto.Group;
            IsMainInGroup = noteDto.IsMainInGroup;
        }
        
        ProtectionProfile = noteDto.ProtectionProfile;
        
        return this;
    }
    
    protected void EncryptWithProfile()
    {
        if(ProtectionProfile == null)
            return;
        
        if (!ProtectionProfile.IsUnlocked)
            throw new InvalidOperationException("Profile is locked.");

        if (_encryptedDescription != null)
            Description = _encryptedDescription;
    }

    protected void DecryptWithProfile()
    {
        if(ProtectionProfile == null)
            return;
        
        if (!ProtectionProfile.IsUnlocked)
            throw new InvalidOperationException("Profile is locked.");

        if (Description != null)
            _encryptedDescription = Description;
    }
   
}