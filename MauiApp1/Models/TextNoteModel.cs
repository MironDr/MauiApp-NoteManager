using MauiApp1.DTOs;
using MauiApp1.Utilities;
using MP01.Models;
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
    
    
    
    //Asocjacje  Kompozycja 
    
    private List<TextBlock> TextBlocks = new();
   

    public void AddTextBlock(string? title, string? content)
    {
        var list = TextBlocks;
        list.Add(new TextBlock
        {
            Title = title,
            Content = content
        });
        TextBlocks = list;
    }

    
    public void RemoveTextBlockAt(int index)
    {
        var list = TextBlocks;
        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);
            TextBlocks = list;
        }
    }
    
    public void ClearTextBlocks()
    {
        TextBlocks = new List<TextBlock>();
    }

    public int GetTextCount()
    {
        return TextBlocks.Count;
    }


    public IEnumerable<string?> GetBlocksTitles()
    {
        return TextBlocks.Select(b => b.Title);
    }
    
    public IEnumerable<string?> GetBlocksContents()
    {
        return TextBlocks.Select(b => b.Content);
    }


    private class TextBlock : BaseModel
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        
    }
    //
    
    
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