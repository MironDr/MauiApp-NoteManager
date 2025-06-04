using System.Text.Json;
using MauiApp1.DTOs;
using MauiApp1.Utilities;
using SQLite;

namespace MauiApp1.Models;

public class TextNoteModel : NoteModel
{
    [Ignore]
    public override NoteType Type => NoteType.Text;
    
    public override NoteModel EditNote(NoteDto dto)
    { 
        base.EditNote(dto);
       
        var textDto = dto as TextNoteDto;


        ClearTextBlocks();
        
        if (textDto != null)
            for (int i = 0; i < textDto.BlocksTitles.Count(); i++)
            {
                AddTextBlock(textDto.BlocksTitles.ElementAt(i), textDto.BlocksContent.ElementAt(i));
            }

        return this;
    }

    public static TextNoteModel CreateNote(NoteDto dto)
    {
        var textDto = dto as TextNoteDto;
        
        if(textDto is null)
            throw new ArgumentException("Invalid note type");
        
        var textNoteModel = GetNoteBase<TextNoteModel>(dto);
        
     
        for (int i = 0; i < textDto.BlocksTitles.Count(); i++)
        {
                textNoteModel.AddTextBlock(textDto.BlocksTitles.ElementAt(i), textDto.BlocksContent.ElementAt(i));
        }
        
        return textNoteModel;
    }
    
    
    
    public string TextBlocksJson { get; set; } = "[]";

    
    [Ignore]
    private List<TextBlock> TextBlocks { get; set; } = new();

  
    private class TextBlock
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }

  
    public void AddTextBlock(string? title, string? content)
    {
        TextBlocks.Add(new TextBlock
        {
            Title = title,
            Content = content
        });
        SyncToJson();
    }

  
    public void RemoveTextBlockAt(int index)
    {
        if (index >= 0 && index < TextBlocks.Count)
        {
            TextBlocks.RemoveAt(index);
            SyncToJson();
        }
    }


    public void ClearTextBlocks()
    {
        TextBlocks = new List<TextBlock>();
        SyncToJson();
    }

  
    public int GetTextCount() => TextBlocks.Count;

    public IEnumerable<string?> GetBlocksTitles() => TextBlocks.Select(b => b.Title);
    
    public IEnumerable<string?> GetBlocksContents() => TextBlocks.Select(b => b.Content);
    
    public void SyncToJson()
    {
        TextBlocksJson = JsonSerializer.Serialize(TextBlocks);
    }
    
    public void LoadFromJson()
    {
        if (!string.IsNullOrWhiteSpace(TextBlocksJson))
        {
            try
            {
                TextBlocks = JsonSerializer.Deserialize<List<TextBlock>>(TextBlocksJson) ?? new();
            }
            catch
            {
                TextBlocks = new();
            }
        }
    }
    
    
    //Asocjacje z atrybutem
    private readonly List<NoteWithSourceModel> _sources = new();

    public void AddSourceLink(NoteWithSourceModel ns)
    {
        if (ns.SourceNote == null)
        {
            Console.WriteLine("Source is null");
            return;
        }
        
        if(_sources.Contains(ns))
            return;
        
        if (ns.Note == this)
        {
            _sources.Add(ns);
        }
    }

    public void RemoveSourceLink(NoteWithSourceModel ns)
    {
        if(!_sources.Contains(ns))   
            return;
        
        _sources.Remove(ns);
        
        if (ns.Note == this)
        {
            ns.Remove();
        }

    }

    private void RemoveAllSources()
    {
        foreach (var ns in _sources.ToList())
        {
            ns.Remove();
        }
    }

    public List<SourceNoteModel> GetSourceLinks()
    {
        return _sources.Select(t => t.SourceNote).ToList();
    }
    
    
    public List<NoteWithSourceModel> GetNotesLinks()
    {
        return _sources.ToList();
    }
    //

    public override void UnlinkAssociations()
    {
        base.UnlinkAssociations();
        ClearTextBlocks();
        RemoveAllSources();
    }
}