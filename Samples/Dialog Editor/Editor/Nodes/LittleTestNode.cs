using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

namespace skybirdgames.eazygraph.dialog.Editor
{
    [Serializable]
    public class LittleTestNode : SinglePortNode
    {
        public TextField textField;

        public LittleTestNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "Little Test Node";

            mainContainer.style.backgroundColor = Color.green;

            this.data = data;

            AddNameField();
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
