using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class XMLSerializationToolbar : Toolbar
{
    private readonly EditorGraphView view;
    private readonly Serializer serializer;

    private Object obj;

    public XMLSerializationToolbar(EditorGraphView view)
    {
        this.view = view;

        // TODO, pass the serializer in! this way we only need 1 serialization toolbar class!
        serializer = new Serializer(
            graphView: view, 
            saving: new XMLGraphSaving(), 
            loading: new XMLGraphLoading());

        Add(TextAssetField());
        Add(NewGraphButton());
        Add(SaveButton());
        Add(LoadButton());
        Add(ClearButton());
    }

    private ObjectField TextAssetField()
    {
        ObjectField objectField = new ObjectField(label: "Object field")
        {
            objectType = typeof(TextAsset)
        };

        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }

    private Button NewGraphButton()
    {
        return new Button(clickEvent: () =>
        {

        })
        { text = "New Graph" };
    }

    private Button SaveButton()
    {
        return new Button(clickEvent: () =>
        {
            var fileName = (obj == null) ? null : obj.name;
            serializer.Save(fileName);
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
            //serializer.Load(null);
        }) { text = "Clear" };
    }
}
