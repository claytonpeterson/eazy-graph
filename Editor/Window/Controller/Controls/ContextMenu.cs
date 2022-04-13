using UnityEngine;
using UnityEngine.UIElements;

public class ContextMenu
{
    private readonly EditorGraphView graphView;
    private readonly NodeCreator nodeCreator;

    public ContextMenu(EditorGraphView graphView, NodeCreator nodeCreator)
    {
        this.graphView = graphView;
        this.nodeCreator = nodeCreator;
    }

    public ContextualMenuManipulator CreateContextualMenu(string contextualMenuText)
    {
        return null;
        /*return new ContextualMenuManipulator(menuEvent =>
            menuEvent.menu.AppendAction(contextualMenuText, actionEvent =>
                graphView.AddElement(
                    nodeCreator.CreateNode(
                        null,
                        actionEvent.eventInfo.mousePosition)),
                        DropdownMenuAction.AlwaysEnabled));*/
    }
}
