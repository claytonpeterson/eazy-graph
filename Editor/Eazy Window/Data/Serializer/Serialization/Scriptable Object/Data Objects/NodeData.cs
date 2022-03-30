using System;
using UnityEngine;

[System.Serializable]
public class NodeData : ScriptableObject
{
    [SerializeField]
    private Vector2 position;

    [SerializeField]
    private Type nodeType;

    public Vector2 Position { get => position; set => position = value; }

    public Type Type { get => nodeType; set => nodeType = value; }
}
