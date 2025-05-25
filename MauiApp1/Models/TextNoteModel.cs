using MauiApp1.DTOs;
using MauiApp1.Utilities;
using MP01.Models;

namespace MauiApp1.Models;

public class TextNoteModel : NoteModel
{
    
    public override NoteType Type => NoteType.Text;

    protected TextNoteModel() : base()
    {
    }
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
        
        var textNoteModel = (TextNoteModel)GetNoteBase(dto, new TextNoteModel());
        
     
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
    private List<NoteWithSourceModel> Sources = new();

    public void AddSourceLink(NoteWithSourceModel ns)
    {
        if (ns.SourceNote == null)
        {
            Console.WriteLine("Source is null");
            return;
        }
        
        if(Sources.Contains(ns))
            return;
        
        if (ns.Note == this)
        {
            Sources.Add(ns);
        }
    }

    public void RemoveSourceLink(NoteWithSourceModel ns)
    {
        if(!Sources.Contains(ns))   
            return;
        
        Sources.Remove(ns);
        
        if (ns.Note == this)
        {
            ns.Remove();
        }

    }

    public List<SourceNoteModel> GetSourceLinks()
    {
        return Sources.Select(t => t.SourceNote).ToList();
    }
    
    
    public List<NoteWithSourceModel> GetNotesLinks()
    {
        return Sources.ToList();
    }
    //

  
    
}