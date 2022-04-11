using System;
using UnityEngine;

[System.Serializable]
public class NodeData : ScriptableObject
{
    [SerializeField]
    private string guid;

    [SerializeField]
    private Vector2 position;

    [SerializeField]
    private string nodeType;

    [SerializeField]
    private TestingOutData data;

    public string GUID { get => guid; set => guid = value; }

    public Vector2 Position { get => position; set => position = value; }

    public string NodeType { get => nodeType; set => nodeType = value; }

    public TestingOutData Data { get => data; set => data = value; }

    public override string ToString()
    {
        return string.Format("Position {0}, GUID {1}, Type {2}, Data {3}", position, guid, nodeType, data);
    }
}
