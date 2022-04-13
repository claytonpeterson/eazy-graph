using System;
using UnityEngine;

[System.Serializable]
public class SaveNode
{
    [SerializeField]
    private string name = "";

    [SerializeField]
    private Vector2 position;

    [SerializeField]
    private string guid;

    [SerializeField]
    private string type;

    [SerializeField]
    private TestingOutData data;

    public string Name { get => name; set => name = value; }

    public Vector2 Position { get => position; set => position = value; }

    public string Guid { get => guid; set => guid = value; }

    public string ObjType { get => type; set => type = value; }

    public TestingOutData Data { get => data; set => data = value; }

    public override string ToString()
    {
        return string.Format("Position {0}, GUID {1}, Type {2}, Data {3}", position, guid, type, data);
    }
}
