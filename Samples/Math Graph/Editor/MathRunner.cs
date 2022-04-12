using System.Collections.Generic;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathRunner : IGraphRunner
    {
        public void Run(Graph graph)
        {
            GetOperators(graph);
        }

        private List<OperatorNode> GetOperators(Graph graph)
        {
            var operators = new List<OperatorNode>();

            foreach(var node in graph.Nodes)
            {
                if(node.Data.name.Length > 0)
                {
                    Debug.Log(node.Data.name);

                    var i = graph.Inputs(node.Guid);
                    foreach(var b in i)
                    {
                        Debug.Log(b.Data.age);
                    }
                }
            }

            return operators;
        }
    }
}