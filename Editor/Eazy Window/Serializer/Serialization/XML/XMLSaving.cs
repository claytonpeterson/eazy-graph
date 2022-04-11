using System.Collections.Generic;
using System.Xml;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class XMLSaving : ISaveGraph
{
    public void Save(string path, List<NodeView> graphNodes, List<Edge> graphEdges)
    {
        // Create document
        var document = new XmlDocument();

        // Create root node
        XmlNode rootNode = document.CreateElement("root");
        document.AppendChild(rootNode);

        // Create the nodes
        var nodes = SaveNodes(document, graphNodes);
        rootNode.AppendChild(nodes);

        // Create the edges
        var edges = SaveEdges(document, graphEdges);
        rootNode.AppendChild(edges);

        document.Save(path);

        AssetDatabase.Refresh();
    }

    private XmlNode SaveNodes(XmlDocument document, List<NodeView> nodes)
    {
        var nodeParent = document.CreateElement("nodes");

        for (int i = 0; i < nodes.Count; i++)
        {
            // Create the root node
            XmlNode node = document.CreateElement("node");
            nodeParent.AppendChild(node);

            // Create the guid node
            XmlNode guid = document.CreateElement("guid");
            guid.InnerText = nodes[i].guid;
            node.AppendChild(guid);

            var pos = nodes[i].GetPosition();

            // Create position node
            XmlNode position = document.CreateElement("position");
            node.AppendChild(position);

            // Create X node
            XmlNode xPosition = document.CreateElement("x");
            xPosition.InnerText = pos.x.ToString();
            position.AppendChild(xPosition);

            // Create Y node
            XmlNode yPosition = document.CreateElement("y");
            yPosition.InnerText = pos.y.ToString();
            position.AppendChild(yPosition);

            // Save data
            XmlNode dataNode = document.CreateElement("data");
            dataNode.InnerText = nodes[i].GetType().ToString();
            node.AppendChild(dataNode);
        }

        return nodeParent;
    }

    private XmlNode SaveEdges(XmlDocument document, List<Edge> graphEdges)
    {
        var edges = document.CreateElement("edges");

        for (int i = 0; i < graphEdges.Count; i++)
        {
            // Create edge root
            XmlNode edge = document.CreateElement("edge");
            edges.AppendChild(edge);

            // Create the input node
            var inputNode = graphEdges[i].input.node as NodeView;
            XmlNode nodeAGUID = document.CreateElement("end");
            nodeAGUID.InnerText = inputNode.guid;
            edge.AppendChild(nodeAGUID);

            // Create the output node
            var outputNode = graphEdges[i].output.node as NodeView;
            XmlNode nodeBGUID = document.CreateElement("start");
            nodeBGUID.InnerText = outputNode.guid;
            edge.AppendChild(nodeBGUID);
        }

        return edges;
    }
}
