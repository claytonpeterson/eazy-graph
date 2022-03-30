using System.Linq;
using UnityEngine;

public class Serializer
{
    private readonly ISaveGraph save;
    private readonly ILoadGraph load;

    private readonly string fileExtension;

    public Serializer(ISaveGraph saving, ILoadGraph loading, string fileExtension)
    {
        save = saving;
        load = loading;

        this.fileExtension = fileExtension;
    }

    public void Save(string fileName, EditorGraphView graphView)
    {
        save.Save(
            path: FilePath(FixEmptyName(fileName)), 
            nodes: graphView.nodes.ToList().Cast<NodeView>().ToList(), 
            edges: graphView.edges.ToList());
    }

    public Graph Load(string fileName)
    {
        string path = (fileName == null) ? null : FilePath(fileName);
        Debug.Log("Loading: " + path);
        return load.Load(path);
    }

    private string FilePath(string fileName)
    {
        return string.Format("Assets/Resources/{0}{1}", fileName, fileExtension);
    }

    private string FixEmptyName(string fileName)
    {
        return fileName == null ? "New" : fileName;
    }
}
