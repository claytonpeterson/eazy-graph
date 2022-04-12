using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OperatorNode : NodeView, IContainsValue, IUpdate
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

        private readonly OutputUpdater output;


        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            output = new OutputUpdater(this);

            mainContainer.style.backgroundColor = Color.blue;

            SetupPorts();
            AddPopupField();
            AddCalculationField();
            Refresh();
        }

        public int Value()
        {
            return Calculate();
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

                UpdateCalculationField();
            });

            Add(popupField);
        }

        private void AddCalculationField()
        {
            calculationField = new Label(Calculate().ToString());
            Add(calculationField);
        }

        List<Edge> InputConnections()
        {
            return inputContainer.Q<Port>().connections.ToList();
        }

        private bool CanCalculate()
        {
            return InputConnections().Count == 2;
        }

        private int Calculate()
        {
            if (!CanCalculate())
                return 0;

            var conn = InputConnections();
            var a = (IContainsValue)conn[0].output.node;
            var b = (IContainsValue)conn[1].output.node;

            var value = FigureOutMathAndStuff(
                inputA: a.Value(),
                inputB: b.Value(), 
                op: data.name);

            return value;
        }

        public void UpdateCalculationField()
        {
            calculationField.text = Calculate().ToString();

            // send the message forth
            output.UpdateOutputConnections();
        }

        private int FigureOutMathAndStuff(int inputA, int inputB, string op)
        {
            if (op == "Add")
            {
                return inputA + inputB;
            }
            else if (op == "Subtract")
            {
                return inputA - inputB;
            }
            else if (op == "Multiply")
            {
                return inputA * inputB;
            }
            else if (op == "Divide")
            {
                return inputA / inputB;
            }
            return 0;
        }

        public void Update()
        {
            UpdateCalculationField();
        }
    }
}
