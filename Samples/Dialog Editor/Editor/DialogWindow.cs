using UnityEditor;
using UnityEditor.UIElements;

namespace skybirdgames.eazygraph.dialog.Editor
{
    public class DialogWindow : EazyWindow
    {
        [MenuItem("Eazy Graph/Samples/Dialog Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow(typeof(DialogWindow));
            window.titleContent.text = "Math Graph";
            window.Show();
        }

        protected override Toolbar[] CreateToolbars()
        {
            var scriptableObjectSerializer =
                new Serializer(
                    saving: new ScriptableObjectGraphSaving(),
                    loading: new ScriptableObjectLoading(),
                    parentFolder: "Assets/Resources/",
                    fileExtension: ".asset");
           
            return new Toolbar[]
            {
            new SerializationToolbar<GraphData>(view, scriptableObjectSerializer),
            new NodeCreatorToolbar(new NodeCreator(view, new Spawner()), this.GetType().Namespace)
            };
        }

        protected override INodeSpawner GetNodeSpawner()
        {
            return new Spawner();
        }
    }
}
