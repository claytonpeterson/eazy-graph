﻿using System.Linq;

public class Serializer
{
    private readonly EditorGraphView graphView;

    private readonly ISaveGraph save;
    private readonly ILoadGraph load;

    public Serializer(EditorGraphView graphView, ISaveGraph saving, ILoadGraph loading)
    {
        this.graphView = graphView;

        save = saving;
        load = loading;
    }

    public void Save(string fileName)
    {
        save.Save(
            path: GetFilePath(FixEmptyName(fileName)), 
            nodes: graphView.nodes.ToList().Cast<GraphNode>().ToList(), 
            edges: graphView.edges.ToList());
    }

    public Graph Load(string fileName)
    {
        string path = (fileName == null) ? null : GetFilePath(fileName);

        return load.Load(path);
    }

    // TODO, it's safe to say we could move this up a level
    private string FixEmptyName(string fileName)
    {
        return (fileName == null) ? "New" : fileName;
    }

    private string GetFilePath(string fileName)
    {
        return string.Format("Assets/Resources/{0}.xml", fileName);
    }
}
