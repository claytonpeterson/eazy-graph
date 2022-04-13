using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

namespace skybirdgames.eazygraph.dialog.Editor
{
    [Serializable]
    public class LittleTestNode : NodeView
    {
        public TextField textField;

        public LittleTestNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "Little Test Node";

            mainContainer.style.backgroundColor = Color.green;

            AddNameField();
        }

        protected override void SetupPorts()
        {
            
        }

        private void AddNameField()
        {
            textField = new TextField("name field")
            {
                value = Data().name
            };

            textField.RegisterValueChangedCallback((evt) =>
            {
                Data().name = evt.newValue;
            });

            Add(textField);
        }
    }
}
