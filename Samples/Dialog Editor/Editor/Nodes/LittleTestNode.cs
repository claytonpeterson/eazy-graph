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

            this.data = data;

            AddNameField();
        }

        protected override PortInformation GetPortInformation()
        {
            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Single
            };
            return portInfo;
        }

        private void AddNameField()
        {
            textField = new TextField("name field")
            {
                value = data.name
            };

            textField.RegisterValueChangedCallback((evt) =>
            {
                data.name = evt.newValue;
            });

            Add(textField);
        }
    }
}
