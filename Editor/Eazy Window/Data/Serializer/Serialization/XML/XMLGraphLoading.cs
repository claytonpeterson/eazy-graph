using System;
using System.Xml;
using UnityEngine;

public class XMLGraphLoading : ILoadGraph
{
    public Graph Load(string path)
    {
        var graph = new Graph();

        XmlDocument document = new XmlDocument();

        document.Load(path);

        LoadNodes(document, graph);
        LoadEdges(document, graph);

        return graph;
    }

    private void LoadNodes(XmlDocument document, Graph graph)
    {
        XmlNodeList nodes = document.SelectNodes("/root/nodes/node");

        foreach (XmlNode node in nodes)
        {
            var n = new SaveNode
            {
                Name = GetName(node),
                Guid = GetGUID(node),
                Position = GetPosition(node),
                Data = GetData(node)
            };

            graph.Nodes.Add(n);
        }
    }

    private string GetName(XmlNode node)
    {
        return node.SelectSingleNode("name").InnerText;
    }

    private string GetGUID(XmlNode node)
    {
        return node.SelectSingleNode("guid").InnerText;
    }

    private Vector2 GetPosition(XmlNode node)
    {
        var x = node.SelectSingleNode("position/x").InnerText;
        var y = node.SelectSingleNode("position/y").InnerText;
        return new Vector2(float.Parse(x), float.Parse(y));
    }

    private Type GetData(XmlNode node)
    {
        var text = node.SelectSingleNode("data").InnerText;
        return Type.GetType(text);
    }

    private void LoadEdges(XmlDocument document, Graph graph)
    {
        XmlNodeList edges = document.SelectNodes("/root/edges/edge");

        foreach (XmlNode edge in edges)
        {
            var connection = new SaveConnection
            {
                GuidA = edge.SelectSingleNode("start").InnerText,
                GuidB = edge.SelectSingleNode("end").InnerText
            };

            graph.Connections.Add(connection);
        }
    }
}
