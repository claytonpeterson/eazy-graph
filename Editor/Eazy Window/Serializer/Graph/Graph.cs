using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Graph
{
    [SerializeField]
    private List<SaveNode> nodes = new List<SaveNode>();

    [SerializeField]
    private List<SaveConnection> connections = new List<SaveConnection>();

    public List<SaveNode> Nodes 
    { 
        get => nodes; 
        set => nodes = value;  
    }

    public List<SaveConnection> Connections 
    { 
        get => connections; 
        set => connections = value; 
    }
}
