using UnityEditor;
using UnityEditor.UIElements;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class ProgrammingGraphWindow : EazyWindow
    {
        [MenuItem("Eazy Graph/Samples/Programming Graph")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(ProgrammingGraphWindow));
            window.titleContent.text = "Programming Graph Editor";
            window.Show();
        }

        protected override Toolbar[] CreateToolbars()
        {
            return new Toolbar[]
            {
                SerializerToolbar(GetSerializer()),
                NodeControllerToolbar()
            };
        }

        protected override INodeSpawner GetNodeSpawner()
        {
            return null;
        }

        private Serializer GetSerializer()
        {
            return new Serializer(
                saving: new ScriptableObjectGraphSaving(),
                loading: new ScriptableObjectLoading(),
                parentFolder: "Assets/Resources/",
                fileExtension: ".asset");
        }

        private RunableSerializationToolbar<GraphData> SerializerToolbar(Serializer serializer)
        {
            return new RunableSerializationToolbar<GraphData>(
                view: view,
                serializer: serializer,
                runner: null);
        }

        private NodeCreatorToolbar NodeControllerToolbar()
        {
            return new NodeCreatorToolbar(
                new NodeCreator(
                    graphView: view,
                    nodeSpawner: GetNodeSpawner()),
                nameSpace: GetType().Namespace);
        }
    }
}
