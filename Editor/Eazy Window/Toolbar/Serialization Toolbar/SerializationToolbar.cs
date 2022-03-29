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
        Add(SaveButton());
        Add(LoadButton());
        Add(ClearButton());
    }

    protected abstract ObjectField InputField();

    private Button SaveButton()
    {
        return new Button(clickEvent: () =>
        {
            var fileName = (obj == null) ? null : obj.name;
            serializer.Save(fileName, view);
        })
        { text = "Save Graph" };
    }

    private Button LoadButton()
    {
        return new Button(clickEvent: () =>
        {
            Graph graph = serializer.Load(obj.name);

            view.ShowGraph(graph);
        })
        { text = "Load Graph" };
    }

    private Button ClearButton()
    {
        return new Button(clickEvent: () =>
        {
            view.ShowGraph(null);
        })
        { text = "Clear" };
    }
}
