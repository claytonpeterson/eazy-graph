using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveNode
{
    [SerializeField]
    private string name;

    [SerializeField]
    private Vector2 position;

    [SerializeField]
    private string guid;

    public string Name { get => name; set => name = value; }
    
    public Vector2 Position { get => position; set => position = value; }

    public string Guid { get => guid; set => guid = value; }
}

public class SaveConnection
{
    [SerializeField]
    private string guidA;

    [SerializeField]
    private string guidB;

    public string GuidA { get => guidA; set => guidA = value; }

    public string GuidB { get => guidB; set => guidB = value; }
}

[System.Serializable]
public class Graph
{
    [SerializeField]
    private List<SaveNode> nodes = new List<SaveNode>();

    [SerializeField]
    private List<SaveConnection> connections = new List<SaveConnection>();

    public List<SaveNode> Nodes { get => nodes; set => nodes = value;  }

    public List<SaveConnection> Connections { get => connections; set => connections = value; }
}
