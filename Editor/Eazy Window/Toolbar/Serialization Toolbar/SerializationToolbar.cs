using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public abstract class SerializationToolbar : Toolbar
{
    protected Object obj;

    private readonly EditorGraphView view;
    private readonly Serializer serializer;

    public SerializationToolbar(EditorGraphView view, Serializer serializer)
    {
        this.view = view;
        this.serializer = serializer;

        Add(InputField());
        Add(NewGraphButton());
        Add(SaveButton());
        Add(LoadButton());
        Add(ClearButton());
    }

    protected abstract ObjectField InputField();

    protected Button NewGraphButton()
    {
        return new Button(clickEvent: () =>
        {

        })
        { text = "New Graph" };
    }

    protected Button SaveButton()
    {
        return new Button(clickEvent: () =>
        {
            var fileName = (obj == null) ? null : obj.name;
            serializer.Save(fileName, view);
        })
        { text = "Save Graph" };
    }

    protected Button LoadButton()
    {
        return new Button(clickEvent: () =>
        {
            Graph graph = serializer.Load(obj.name);

            view.ShowGraph(graph);
        })
        { text = "Load Graph" };
    }

    protected Button ClearButton()
    {
        return new Button(clickEvent: () =>
        {
            view.ShowGraph(null);
        })
        { text = "Clear" };
    }
}
