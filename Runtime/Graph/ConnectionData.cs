using UnityEngine;

[System.Serializable]
public class ConnectionData : ScriptableObject
{
    public string nodeAGUID;
    public string nodeBGUID;

    public string nodeAPortName;
    public string nodeBPortName;
}