using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OperatorNode : NodeView
    {
        private PopupField<string> popupField;

        private readonly List<string> popupFieldValues = new List<string> 
        { 
            "Add", 
            "Subtract", 
            "Multiply", 
            "Divide" 
        };

        private Label calculationField;

        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            mainContainer.style.backgroundColor = Color.blue;

            SetupPorts();
            AddPopupField();
            AddCalculationField();
            Refresh();
        }

        private void SetupPorts()
        {
            inputContainer.Add(
                child: CreatePort(Direction.Input, Port.Capacity.Multi, "Input A"));

            outputContainer.Add(
                child: CreatePort(Direction.Output, Port.Capacity.Single, "Output"));
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

                Calculate();
            });

            Add(popupField);
        }

        private void AddCalculationField()
        {
            calculationField = new Label();
            Add(calculationField);
        }

        private void Calculate()
        {
            Debug.Log("calculating");

            foreach(var connection in Connections())
            {
                var numberNode = (NumberNode)connection.output.node;
                Debug.Log(numberNode.Value());
            }
        }

        IEnumerable<Edge> Connections()
        {
            return inputContainer.Q<Port>().connections;
        }
    }
}
