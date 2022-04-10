using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ScriptableObjectLoading : ILoadGraph
{
    public Graph Load(string path)
    {
        var graphData = AssetDatabase.LoadAssetAtPath<GraphData>(path);
        var graph = new Graph();

        LoadNodes(graphData.Nodes, graph);
        LoadEdges(graph);

        return graph;
    }

    private void LoadNodes(List<NodeData> nodes, Graph graph)
    {
        foreach (var node in nodes)
        {
            var saveNode = new SaveNode
            {
                Name = node.name,
                Guid = null,
                Position = node.Position,
                Data = node.Type
            };

            /*Debug.Log(string.Format(
                "{0}{1}{2}{3}", 
                saveNode.Name, 
                saveNode.Guid, 
                saveNode.Position, 
                saveNode.Data));
*/
            graph.Nodes.Add(saveNode);
        }
    }

    private void LoadEdges(Graph graph)
    {

    }
}
