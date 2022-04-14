using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

using skybirdgames.eazygraph.Editor;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathGraphWindow : EazyWindow
    {
        private RunableSerializationToolbar<GraphData> serializationToolbar;

        [MenuItem("Eazy Graph/Samples/Math Graph")]
        public static void ShowWindow()
        {
            StyleSheet styleSheet = (StyleSheet)Resources.Load("layout");

            var window = GetWindow(typeof(MathGraphWindow));
            window.titleContent.text = "Math Graph";
            window.rootVisualElement.styleSheets.Add(styleSheet);
            window.Show();
        }

        protected override Toolbar[] CreateToolbars()
        {
            // Create the serializer
            var scriptableObjectSerializer =
                new Serializer(
                    saving: new ScriptableObjectGraphSaving(),
                    loading: new ScriptableObjectLoading(),
                    parentFolder: "Assets/Resources/",
                    fileExtension: ".asset");

            serializationToolbar = new RunableSerializationToolbar<GraphData>(
                view, 
                scriptableObjectSerializer, 
                new MathRunner());

            return new Toolbar[]
            {
                serializationToolbar,
                new NodeCreatorToolbar(new NodeCreator(view, GetNodeSpawner()), GetType().Namespace)
            };
        }

        protected override INodeSpawner GetNodeSpawner()
        {
            return new MathGraphNodeSpawner();
        }
    }
}
