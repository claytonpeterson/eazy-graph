using System;
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

    [SerializeField]
    private Type data;

    public string Name { get => name; set => name = value; }

    public Vector2 Position { get => position; set => position = value; }

    public string Guid { get => guid; set => guid = value; }

    public Type Data { get => data; set => data = value; }
}
