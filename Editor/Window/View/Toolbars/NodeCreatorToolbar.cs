using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace skybirdgames.eazygraph.Editor
{
    public class NodeCreatorToolbar : Toolbar
    {
        private readonly NodeCreator nodeCreator;

        public NodeCreatorToolbar(NodeCreator nodeCreator, string domainNamespace)
        {
            this.nodeCreator = nodeCreator;

            Add(new TextElement { text = "Node Creation: " });

            AddButtons(domainNamespace);
        }

        private void AddButtons(string domainNamespace)
        {
            foreach (var button in CreateButtonsForTypes(GetLocalTypes(domainNamespace)))
            {
                Add(button);
            }
        }

        private Button[] CreateButtonsForTypes(List<Type> types)
        {
            Button[] buttons = new Button[types.Count];

            for (int i = 0; i < types.Count; i++)
            {
                buttons[i] = CreateNodeButton(types[i]);
            }
            return buttons;
        }

        private Button CreateNodeButton(Type nodeType)
        {
            return new Button(clickEvent: () =>
            {
                nodeCreator.CreateNode(nodeType, new Vector2(0, 0), GetEmptyData());
            })
            { text = "Add " + nodeType.Name };
        }

        protected virtual TestingOutData GetEmptyData()
        {
            return new TestingOutData();
        }

        private List<Type> GetLocalTypes(string domainNamespace)
        {
            var localTypes = new List<Type>();

            foreach(var type in TypeCache.GetTypesDerivedFrom<NodeView>())
            {
                if (type.ToString().Contains(domainNamespace))
                {
                    localTypes.Add(type);
                }
            }

            return localTypes;
        }
    }
}

