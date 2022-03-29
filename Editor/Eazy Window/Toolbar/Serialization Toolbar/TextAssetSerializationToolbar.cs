using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class TextAssetToolbar : SerializationToolbar
{
    public TextAssetToolbar(EditorGraphView view, Serializer serializer) : base(view, serializer)
    {
    }

    protected override ObjectField InputField()
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
}
