﻿using System.Collections.Generic;
using System.Linq;
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

        protected override void OnPortRemoved(Port port)
        {
            Debug.Log(port + " down");
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

                calculationField.text = Calculate();
            });

            Add(popupField);
        }

        private void AddCalculationField()
        {
            calculationField = new Label(Calculate());
            Add(calculationField);
        }

        List<Edge> Connections()
        {
            return inputContainer.Q<Port>().connections.ToList();
        }

        private bool CanCalculate()
        {
            return Connections().Count == 2;
        }

        private string Calculate()
        {
            if (!CanCalculate())
                return "";

            var conn = Connections();
            var a = (NumberNode)conn[0].output.node;
            var b = (NumberNode)conn[1].output.node;

            var value = FigureOutMathAndStuff(
                inputA: a.Value(),
                inputB: b.Value(), 
                op: data.name);

            return value.ToString();
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
    }
}
