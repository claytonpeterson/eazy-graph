using System.Linq;

public class Serializer
{
    private readonly ISaveGraph save;
    private readonly ILoadGraph load;

    public Serializer(ISaveGraph saving, ILoadGraph loading, string fileExtension)
    {
        save = saving;
        load = loading;
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
        return load.Load(path);
    }

    private string FilePath(string fileName)
    {
        return string.Format("Assets/Resources/{0}", fileName);
    }

    private string FixEmptyName(string fileName)
    {
        return fileName == null ? "New" : fileName;
    }
}
