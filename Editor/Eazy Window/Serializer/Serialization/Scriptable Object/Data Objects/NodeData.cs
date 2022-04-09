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
    private Type nodeType;

    public string GUID { get => guid; set => guid = value; }

    public Vector2 Position { get => position; set => position = value; }

    public Type Type { get => nodeType; set => nodeType = value; }
}
