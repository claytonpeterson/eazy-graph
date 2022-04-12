using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class NumberNode : NodeView
    {
        FloatField numberField;

        public NumberNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            mainContainer.style.backgroundColor = Color.green;

            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Single
            };

            inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));

            outputContainer.Add(
                child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));

            AddNumberField();
            Refresh();
        }

        private void AddNumberField()
        {
            numberField = new FloatField("number field")
            {
                value = data.age
            };

            numberField.RegisterValueChangedCallback((evt) =>
            {
                data.age = (int)evt.newValue;
                Debug.Log(evt.newValue);
            });

            Add(numberField);
        }
    }
}