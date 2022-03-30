using UnityEngine;

public class ScriptableObjectLoading
{
    public GraphData Load(string fileName)
    {
        if (IsValidFileName(fileName) == false)
        {
            return null;
        }
        return Resources.Load<GraphData>(fileName);
    }

    private bool IsValidFileName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return false;
        }
        return true;
    }
}
