using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class ScriptableObjectToolbar : SerializationToolbar
{
    public ScriptableObjectToolbar(EditorGraphView view, Serializer serializer) : base(view, serializer)
    {
    }

    protected override ObjectField InputField()
    {
        ObjectField objectField = new ObjectField(label: "Object field")
        {
            objectType = typeof(ScriptableObject)
        };

        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }
}
