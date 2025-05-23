using MauiApp1.Structs;
using MauiApp1.Views.Notes;

namespace MauiApp1.TemplateSelectors;

public class NoteItemTemplateSelector : DataTemplateSelector
{
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is NoteItemStruct noteStruct)
        {
            return new DataTemplate(() =>
            {
                var view = new NoteItemView();
                view.SetData(noteStruct);
                return view;
            });
        }

      
        return new DataTemplate(() => new ContentView());
    }
}

    
