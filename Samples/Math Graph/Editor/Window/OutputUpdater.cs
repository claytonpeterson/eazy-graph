using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OutputUpdater
    {
        private readonly Node node;

        public OutputUpdater(Node node)
        {
            this.node = node;
        }

        public void UpdateOutputConnections()
        {
            foreach (var output in OutputConnections())
            {
                var operatorNode = (OperatorNode)output.input.node;
                operatorNode.UpdateCalculationField();
            }
        }

        private List<Edge> OutputConnections()
        {
            return node.outputContainer.Q<Port>().connections.ToList();
        }
    }
}
