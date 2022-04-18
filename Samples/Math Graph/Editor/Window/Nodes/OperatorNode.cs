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
        private readonly MathRunner mathRunner;

        public OperatorNode(Vector2 position, TestingOutData data, MathRunner mathRunner) : base(position, data)
        {
            output = new OutputUpdater(this);
            this.mathRunner = mathRunner;

            mainContainer.style.backgroundColor = Color.blue;

            AddPopupField();
            AddCalculationField();

            Add(InputPortButton());
            Add(OutputPortButton());

            Update();
            Refresh();
        }

        private Button InputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddInputPort("port " + (Ports.InputPortCount() + 1), Port.Capacity.Single);
            })
            { text = "Add Input Port" };
        }

        private Button OutputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddOutputPort("port " + (Ports.OutputPortCount() + 1), Port.Capacity.Single);
            })
            { text = "Add Output Port" };
        }

        public int Value()
        {
            if (!CanCalculate())
                return 0;

            return mathRunner.RunNode(this);
        }

        protected override void SetupPorts()
        {
            Ports.AddInputPort("port 1", Port.Capacity.Single);
            Ports.AddInputPort("port 2", Port.Capacity.Single);

            Ports.AddOutputPort("port 1", Port.Capacity.Single);
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
                Data().age = Value();
                UpdateCalculationField();
            });

            Add(popupField);
        }

        private void AddCalculationField()
        {
            calculationField = new Label(Value().ToString());
            Add(calculationField);
        }

        private bool CanCalculate()
        {
            return Ports.GetInputConnections().Count > 1;
        }

        public void UpdateCalculationField()
        {
            calculationField.text = Value().ToString();

            // send the message forth
            output.UpdateOutputConnections();
        }

        public override void Update()
        {
            UpdateCalculationField();

            Data().name = Data().name ?? popupFieldValues[0];
            Data().age = Value();
        }
    }
}
