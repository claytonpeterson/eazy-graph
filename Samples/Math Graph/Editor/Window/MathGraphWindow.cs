using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

using skybirdgames.eazygraph.Editor;
using UnityEditor.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathGraphWindow : EazyWindow
    {
        [MenuItem("Eazy Graph/Samples/Math Graph")]
        public static void ShowWindow()
        {
            StyleSheet styleSheet = (StyleSheet)Resources.Load("layout");

            var window = GetWindow(typeof(MathGraphWindow));
            window.titleContent.text = "Math Graph";
            window.rootVisualElement.styleSheets.Add(styleSheet);
            window.Show();
        }

        protected override Toolbar[] GetToolbars()
        {
            return new Toolbar[]
            {
                new RunableSerializationToolbar<GraphData>(
                    view: view,
                    serializer: GetSerializer(),
                    runner: new MathRunner()),

                new NodeCreatorToolbar(
                    nodeCreator: new NodeCreator(
                        graphView: view,
                        nodeSpawner: GetNodeSpawner()),
                    domainNamespace: GetType().Namespace)
            };
        }

        protected override INodeSpawner GetNodeSpawner()
        {
            return new MathGraphNodeSpawner();
        }
    }
}
