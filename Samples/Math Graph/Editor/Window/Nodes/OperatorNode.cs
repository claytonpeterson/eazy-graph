using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

using skybirdgames.eazygraph.Editor;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OperatorNode : NodeView, IContainsValue
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

        private Port inputA;
        private Port inputB;

        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            output = new OutputUpdater(this);

            mainContainer.style.backgroundColor = Color.blue;

            AddPopupField();
            AddCalculationField();

            Update();
            Refresh();
        }

        public int Value()
        {
            return Calculate();
        }

        protected override void SetupPorts()
        {
            inputA = Ports.AddInputPort("input a", Port.Capacity.Single);
            inputB = Ports.AddInputPort("input b", Port.Capacity.Single);
            Ports.AddOutputPort("output", Port.Capacity.Multi);
        }

        private void AddPopupField()
        {
            popupField = new PopupField<string>(popupFieldValues, popupFieldValues[0])
            {
                value = Data().name == null ? popupFieldValues[0] : Data().name
            };

            popupField.RegisterValueChangedCallback((evt) =>
            {
                Data().name = evt.newValue;
                Data().age = Calculate();
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
            var output = new List<Edge>();
            output.AddRange(inputA.connections);
            output.AddRange(inputB.connections);

            return output;
            /*return inputContainer.Q<Port>().connections.ToList();*/
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
                op: Data().name);

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

        public override void Update()
        {
            UpdateCalculationField();

            Data().name = Data().name ?? popupFieldValues[0];
            Data().age = Calculate();
        }
    }
}
