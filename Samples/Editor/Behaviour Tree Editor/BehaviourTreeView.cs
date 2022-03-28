/*using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeView : GraphEditorView
{
    private Button NewNodeButton()
    {
        return new Button(clickEvent: () =>
        {
            Debug.Log("hi");
        })
        { text = "Add Node" };
    }

    Object obj;

    private ObjectField BehaviourObjectField()
    {
        ObjectField objectField = new ObjectField(label: "Behaviour")
        {
            objectType = typeof(IBehaviour)
        };

        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }


    public override void AddNodes(GraphData graph)
    {
        if (graph == null)
            return;

        foreach (var nodeData in graph.Nodes)
        {
            Debug.Log(nodeData);

            var visualElements = new List<VisualElement>();

            if (nodeData.GetType() == typeof(NumberNodeData))
            {
                visualElements.Add(NewNodeButton());
            }

            else if (nodeData.GetType() == typeof(SpriteTestNode))
            {
                visualElements.Add(BehaviourObjectField());
            }

            var node = new GraphNode(
                nodeData.GetType().ToString(),
                nodeData.Position,
                visualElements.ToArray());

            AddElement(node);
        }
    }
}
*/