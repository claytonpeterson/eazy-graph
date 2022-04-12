using System.Collections.Generic;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathRunner : IGraphRunner
    {
        public void Run(Graph graph)
        {
            foreach (var node in graph.Nodes)
            {
                ProcessNode(node, graph);
            }
        }

        private void ProcessNode(SaveNode node, Graph graph)
        {
            if (IsOperator(node))
            {
                var value = ProcessOperaterNode(node, graph);
                Debug.Log(value);
            }
        }

        private int ProcessOperaterNode(SaveNode node, Graph graph)
        {
            var inputs = GetInputs(node, graph);
            if (inputs != null)
            {
                int inputA = inputs[0].Data.age;
                int inputB = inputs[1].Data.age;
                return FigureOutMathAndStuff(
                    inputA, 
                    inputB, 
                    GetOperation(node));
            }
            return 0;
        }

        private bool IsOperator(SaveNode node)
        {
            Debug.Log(node.Data);
            return node.Data.name.Length > 0;
        }

        private string GetOperation(SaveNode node)
        {
            return node.Data.name;
        }

        private List<SaveNode> GetInputs(SaveNode node, Graph graph)
        {
            return 
                graph.Inputs(node.Guid).Count == 2 ? 
                graph.Inputs(node.Guid) : 
                null;
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