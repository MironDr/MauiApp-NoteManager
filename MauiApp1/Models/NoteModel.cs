
using MauiApp1.DTOs;
using MauiApp1.Utilities;
using SQLite;

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
    
    //Data Base
        private string _title = null!;
        
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Cannot be empty.", nameof(Title));
                _title = value;
            }
        }
        
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        [Ignore]
        public int DaysCount => DateTime.Now.Day - CreatedAt.Day;
        
        public bool IsMain { get; set; }

    //Ignore
        [Ignore]
        public virtual NoteType Type { get; }
        
        [Ignore]
        public string? EncryptedDescription
        {
            get
            {
                if (_protectionProfile == null || string.IsNullOrEmpty(Description) 
                                               || !_protectionProfile.IsUnlocked 
                                               || !ObjectUtils.IsBase64String(Description))
                    return Description;

                try
                {
                    return ObjectUtils.DecryptAes(Description, _protectionProfile.DerivedKey!);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to decrypt", ex);
                }
            }
            private set
            {
                if (_protectionProfile == null)
                {
                    Description = value;
                    return;
                }
                

                if (!_protectionProfile.IsUnlocked)
                    throw new InvalidOperationException("Profile is locked.");

                if (value != null) Description = ObjectUtils.EncryptAes(value, _protectionProfile.DerivedKey!);
            }
        }
        
        // ---- Protection Profile ----
        private ProtectionProfileModel? _protectionProfile;
        [Ignore]
        public ProtectionProfileModel? ProtectionProfile
        {
            get => _protectionProfile;
            set
            {
                if(value?.Id == _protectionProfile?.Id)
                    return;
                
                if (_protectionProfile is { IsUnlocked: false })
                    throw new InvalidOperationException("Current protection profile is not unlocked.");
                

                if (_protectionProfile != null && _protectionProfile.GetNotes().ContainsKey(Id))
                {
                    DecryptWithProfile();
                    _protectionProfile?.RemoveNote(Id);
                }

                
                _protectionProfile = value;
                ProfileId = _protectionProfile?.Id;
                
                if (_protectionProfile != null)
                {
                    _protectionProfile.AddNoteToProfile(this);
                    EncryptWithProfile();
                }
            }
        }
        
        // ---- Group ----

        private GroupModel? _group;
        [Ignore]
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

                GroupId = _group?.Id;
                _group?.AddNoteToGroup(this);
            }
        }
        
        // ---- Main note flag ----
        private bool _isMainInGroup;
        
        [Ignore]
        public bool IsMainInGroup
        {
            get => _isMainInGroup;
            set
            {
                if (_group == null || value == _isMainInGroup)
                    return;
                
                _isMainInGroup = value;
                IsMain = value;
                
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
        
        // ---- Category ----

        private CategoryModel? _category;
        [Ignore]
        public CategoryModel? Category
        {
            get => _category;
            set
            {
                if (Group != null)
                    return;

                if (_category != null && _category.GetNotes().Contains(this))
                {
                    _category.RemoveNote(this);
                }
                
                _category = value;
                CategoryId = _category?.Id;

                if (_category != null && !_category.GetNotes().Contains(this))
                {
                    _category.AddNote(this);
                }
            }
        }
    
    //Associations id
        // ---- Protection Profile ----
    
        public int? ProfileId { get; set; }
        
        // ---- Group ----
        
        public int? GroupId { get; set; }
        
        // ---- Category ----
        public int? CategoryId { get; set; }

    // ---- Helpers ----
    protected static T GetNoteBase<T>(NoteDto dto) where T : NoteModel, new()
    {
        if (dto.Category == null && dto.Group == null)
            throw new ArgumentNullException(nameof(dto), "Both Category and Group cannot be null");

        var note = new T
        {
            Title = dto.Title,
            EncryptedDescription = dto.Description,
            CreatedAt = DateTime.Now,
            ProtectionProfile = dto.ProtectionProfile
        };

        if (dto.Category != null)
        {
            note.Category = dto.Category;
            note.Group = null;
            note.IsMainInGroup = false;
        }
        else
        {
            note.Group = dto.Group;
            note.Category = null;
            note.IsMainInGroup = dto.IsMainInGroup;
        }

        return note;
    }
    
    public virtual NoteModel EditNote(NoteDto noteDto)
    {
        if(noteDto.Category == null && noteDto.Group == null)
            throw new ArgumentNullException(nameof(noteDto) + " Group and Category cannot be null");
        
        Title = noteDto.Title;
        EncryptedDescription = noteDto.Description;
        
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
    
    private void EncryptWithProfile()
    {
        if(ProtectionProfile == null)
            return;
        
        if (Description != null && ProtectionProfile.DerivedKey != null)
            EncryptedDescription = Description;
        
    }

    private void DecryptWithProfile()
    {
        if(ProtectionProfile == null)
            return;

        if (Description != null && ProtectionProfile.DerivedKey != null)
            Description = EncryptedDescription;
        

    }
    
    public virtual void UnlinkAssociations()
    {
        ProtectionProfile = null;
        Group = null;
        Category = null;
    }

    
   
}