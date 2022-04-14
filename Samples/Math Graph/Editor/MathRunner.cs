using System.Collections.Generic;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathRunner : IGraphRunner
    {
        public int Run(GraphData graph)
        {
            foreach (var node in graph.Nodes)
            {
                if (!HasChildren(node, graph))
                {
                    return ProcessNode(node, graph);
                }
            }
            return 0;
        }

        private int ProcessNode(NodeData node, GraphData graph)
        {
            if (IsOperator(node))
            {
                return ProcessOperaterNode(node, graph);
            }
            return 0;
        }

        private int ProcessOperaterNode(NodeData node, GraphData graph)
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

        private bool IsOperator(NodeData node)
        {
            return node.Data.name != null && node.Data.name.Length > 0;
        }

        private bool HasChildren(NodeData node, GraphData graph)
        {
            return graph.Outputs(node.GUID).Count > 0;
        }

        private string GetOperation(NodeData node)
        {
            return node.Data.name;
        }

        private List<NodeData> GetInputs(NodeData node, GraphData graph)
        {
            return 
                graph.Inputs(node.GUID).Count == 2 ? 
                graph.Inputs(node.GUID) : 
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