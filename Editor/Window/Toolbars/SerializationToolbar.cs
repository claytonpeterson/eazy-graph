using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class SerializationToolbar<T> : Toolbar
{
    protected Object obj;

    protected readonly Serializer serializer;

    private readonly EditorGraphView view;

    public SerializationToolbar(EditorGraphView view, Serializer serializer)
    {
        this.view = view;
        this.serializer = serializer;

        Add(InputField());
        Add(SaveButton());
        Add(LoadButton());
        Add(ClearButton());
    }

    public void Save()
    {
        var fileName = (obj == null) ? null : obj.name;
        serializer.Save(fileName, view);
    }

    public GraphData LoadGraph()
    {
        if (obj == null)
            return null;

        return serializer.Load(obj.name);
    }

    protected ObjectField InputField()
    {
        var label = "Object Field (" + serializer.FileExtension + ")";

        ObjectField objectField = new ObjectField(label: label)
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
            Save();
        })
        { text = "Save Graph" };
    }

    private Button LoadButton()
    {
        return new Button(clickEvent: () =>
        {
            view.ShowGraph(LoadGraph());
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
