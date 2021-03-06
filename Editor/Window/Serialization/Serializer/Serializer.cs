using System.Linq;

namespace skybirdgames.eazygraph.Editor
{
    public class Serializer
    {
        private readonly ISaveGraph save;
        private readonly ILoadGraph load;

        private readonly string parentFolder;

        public string FileExtension { get; }

        public Serializer(ISaveGraph saving, ILoadGraph loading, string parentFolder, string fileExtension)
        {
            save = saving;
            load = loading;

            this.parentFolder = parentFolder;
            FileExtension = fileExtension;
        }

        public void Save(string fileName, View graphView)
        {
            save.Save(
                path: FilePath(FixEmptyName(fileName)),
                nodes: graphView.nodes.ToList().Cast<NodeView>().ToList(),
                edges: graphView.edges.ToList());
        }

        public GraphData Load(string fileName)
        {
            return load.Load(
                path: (fileName == null) ? null : FilePath(fileName));
        }

        private string FilePath(string fileName)
        {
            return string.Format(parentFolder + "{0}{1}", fileName, FileExtension);
        }

        private string FixEmptyName(string fileName)
        {
            return fileName ?? "New";
        }
    }
}