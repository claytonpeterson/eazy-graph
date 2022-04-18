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

        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            output = new OutputUpdater(this);

            mainContainer.style.backgroundColor = Color.blue;

            AddPopupField();
            AddCalculationField();

            Add(InputPortButton());
            
            Update();
            Refresh();
        }

        private Button InputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddInputPort("port: " + (Ports.InputPortCount()+1), Port.Capacity.Single);
            })
            { text = "Add Input Branch" };
        }

        public int Value()
        {
            return Calculate();
        }

        protected override void SetupPorts()
        {
            Ports.AddInputPort("input a", Port.Capacity.Single);
            Ports.AddInputPort("input b", Port.Capacity.Single);

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
            foreach (var port in Ports.GetInputConnections())
            {
                output.Add(port);
            }
            return output;
        }

        private bool CanCalculate()
        {
            return InputConnections().Count > 1;
        }

        private int Calculate()
        {
            if (!CanCalculate())
                return 0;

            int total = 0;

            for(int i = 0; i < InputConnections().Count; i++)
            {
                var node = (IContainsValue)InputConnections()[i].output.node;

                total = FigureOutMathAndStuff(node.Value(), total, Data().name);
            }

            return total;
        }

        public void UpdateCalculationField()
        {
            calculationField.text = Calculate().ToString();

            // send the message forth
            output.UpdateOutputConnections();
        }

        private int FigureOutMathAndStuff(int input, int total, string op)
        {
            if (op == "Add")
            {
                return total + input;
            }
            else if (op == "Subtract")
            {
                return total - input;
            }
            else if (op == "Multiply")
            {
                if (total == 0)
                    total = 1;
                return total * input;
            }
            else if (op == "Divide")
            {
                return total / input;
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
