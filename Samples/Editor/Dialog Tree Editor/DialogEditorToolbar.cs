using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DialogEditorToolbar : Toolbar
{
    private readonly NodeSpawner nodeSpawner;
    private readonly EditorGraphView view;

    public DialogEditorToolbar(EditorGraphView view)
    {
        this.view = view;
        nodeSpawner = new NodeSpawner();

        Add(new TextElement
        {
            text = "Dialog Editor Tools: "
        });

        Add(NewNPCDialogNode());
        Add(NewReplyNode());
    }

    private Button NewNPCDialogNode()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode(nodeSpawner.CreateNode("NPC Dialog Node", Vector2.zero));
        })
        { text = "Add NPC Dialog Node" };
    }

    private Button NewReplyNode()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode(nodeSpawner.CreateNode("Reply Node", Vector2.zero));
        })
        { text = "Add Reply Node" };
    }
}
