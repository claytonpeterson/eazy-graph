using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JSONSaving : ISaveGraph
{
    public void Save(string path, List<NodeView> nodes, List<Edge> edges)
    {
        foreach(var node in nodes)
        {
            string json = JsonUtility.ToJson(node);
            Debug.Log(json);

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.Write(json);
            }

            AssetDatabase.Refresh();

            /*XmlSerializer serializer = new XmlSerializer(typeof(NodeView));
            StreamWriter writer = new StreamWriter(path);
            serializer.Serialize(writer.BaseStream, node);
            writer.Close();*/
        }
    }
}
