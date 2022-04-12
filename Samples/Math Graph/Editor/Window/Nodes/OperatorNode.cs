using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OperatorNode : NodeView
    {
        PopupField<string> popupField;

        List<string> popupFieldValues = new List<string> { "Add", "Subtract", "Multiply", "Divide" };

        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            mainContainer.style.backgroundColor = Color.blue;

            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Multi,
                OutputPortCapacity = Port.Capacity.Single
            };

            inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input A"));

            outputContainer.Add(
                child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));

            AddPopupField();

            Refresh();
        }

        private void AddPopupField()
        {
            popupField = new PopupField<string>(popupFieldValues, popupFieldValues[0])
            {
                value = data.name == null ? popupFieldValues[0] : data.name
            };

            popupField.RegisterValueChangedCallback((evt) =>
            {
                data.name = evt.newValue;
                Debug.Log(evt.newValue);
            });

            Add(popupField);
        }
    }
}
