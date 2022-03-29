
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class NodeSpawner : INodeSpawner
{
    //Object obj;

    string t;
/*
    private ObjectField ObjectFieldContainer(ObjectField objectField)
    {
        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }
*/
    public TextField TextFieldContainer(TextField textField)
    {
        textField.SetValueWithoutNotify(t);
        textField.MarkDirtyRepaint();
        textField.RegisterValueChangedCallback(evt => {
            t = evt.newValue;
        });
        return textField;
    }
    
    public NodeView CreateNode(string name, Vector2 position)
    {
        if (name == "NPC Dialog Node")
        {
            return NPCDialogNode(position);
        }
        else
            return ReplyNode(position);
    }

    public NodeView NPCDialogNode(Vector2 position)
    {
        var t = new TextField();

        var a = new NodeView(
            title: "NPC Dialog Node",
            position: position,
            visualElements: new VisualElement[] { TextFieldContainer(t) },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Multi,
            null);
        
        a.data = t.text;
        Debug.Log(t.text);

        return a;
    }

    public NodeView ReplyNode(Vector2 position)
    {
        return new NodeView(
            title: "Reply Node",
            position: position,
            visualElements: new VisualElement[] { },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Multi,
            null);
    }
}
