using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class SerializationToolbar<T> : Toolbar
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

    protected ObjectField InputField()
    {
        ObjectField objectField = new ObjectField(label: "Object field")
        {
            objectType = typeof(T)
        };

        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }

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
