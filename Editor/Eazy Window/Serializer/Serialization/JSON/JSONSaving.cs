using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JSONSaving : ISaveGraph
{
    public void Save(string path, List<NodeView> nodes, List<Edge> edges)
    {
        Debug.Log(JsonUtility.ToJson(nodes));

        foreach(var node in nodes)
        {
            string json = JsonUtility.ToJson(node);
            Debug.Log(json);

            //AssetDatabase.Refresh();
        }
    }
}
