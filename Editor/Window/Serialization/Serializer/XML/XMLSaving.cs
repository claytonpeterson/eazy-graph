using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace skybirdgames.eazygraph.Editor
{
    public class XMLSaving : ISaveGraph
    {
        public GraphData Save(string path, List<NodeView> graphNodes, List<Edge> graphEdges)
        {
            // Create document
            var document = new XmlDocument();

            // Create root node
            XmlNode rootNode = document.CreateElement("root");
            document.AppendChild(rootNode);

            rootNode.AppendChild(SaveNodes(document, graphNodes));
            rootNode.AppendChild(SaveEdges(document, graphEdges));
            //rootNode.AppendChild(SaveData(document, graphNodes));

            document.Save(path);

            AssetDatabase.Refresh();

            return null;
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

                // Save type
                XmlNode typeNode = document.CreateElement("type");
                typeNode.InnerText = nodes[i].GetType().ToString();
                node.AppendChild(typeNode);

                //var data = MySerializer<Data>.Serialize(nodes[i].data);

                /*// Save data object
                XmlNode dataNode = document.CreateElement("data");
                using (var stringwriter = new StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(Data));
                    serializer.Serialize(stringwriter, nodes[i].data);
                    dataNode.InnerText = stringwriter.ToString();
                }
                node.AppendChild(dataNode);*/
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

        private XmlNode SaveData(XmlDocument document, List<NodeView> nodes)
        {
            var nodeParent = document.CreateElement("data");

            for (int i = 0; i < nodes.Count; i++)
            {
                // Create the root node
                XmlNode node = document.CreateElement("node");
                nodeParent.AppendChild(node);

                // Create the guid node
                XmlNode guid = document.CreateElement("guid");
                guid.InnerText = nodes[i].guid;
                node.AppendChild(guid);

                //DataToXML(document, nodes[i].data);

                /*// Create the guid node
                XmlNode data = document.CreateElement("data");
                guid.InnerText = nodes[i].data;
                node.AppendChild(guid);*/
            }

            return nodeParent;
        }

        private XmlNode DataToXML(XmlDocument document, TestingOutData data)
        {
            var d = MySerializer<TestingOutData>.Serialize(data);

            return null;
        }

        public static void Serialize(object item, string path)
        {
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, item);
            writer.Close();
        }

        public class MySerializer<T> where T : class
        {
            public static string Serialize(T obj)
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                using (var sww = new StringWriter())
                {
                    using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                    {
                        xsSubmit.Serialize(writer, obj);
                        return sww.ToString();
                    }
                }
            }
        }
    }
}