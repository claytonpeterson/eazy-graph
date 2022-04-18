using System.Collections.Generic;

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

        public int RunNode(OperatorNode node)
        {
            int total = 0;
            for (int i = 0; i < node.Ports.GetInputConnections().Count; i++)
            {
                var input = (IContainsValue)node.Ports.GetInputConnections()[i].output.node;
                total = FigureOutMathAndStuff(input.Value(), total, node.Data().name);
            }
            return total;
        }

        private int ProcessNode(NodeData node, GraphData graph)
        {
            if (IsOperator(node))
            {
                return ProcessOperaterNode(node, graph);
            }
            return 0;
        }

        // this is the important one!!
        private int ProcessOperaterNode(NodeData node, GraphData graph)
        {
            int total = 0;
            foreach(NodeData inputNode in GetInputs(node, graph))
            {
                total = FigureOutMathAndStuff(
                    input: inputNode.Data.age,
                    total: total,
                    op: GetOperation(node));
            }

            return total;
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
            var inputs = graph.Inputs(node.GUID);
            return
                inputs.Count > 1 ?
                inputs : 
                null;
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
    }
}