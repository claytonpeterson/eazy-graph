using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public abstract class EazyWindow : EditorWindow
    {
        protected View view;

        private void OnEnable()
        {
            SetupWindow();
        }

        private void OnDisable()
        {
            TeardownWindow();
        }

        private void SetupWindow()
        {
            rootVisualElement.Add(CreateView());

            foreach (var toolbar in GetToolbars())
            {
                rootVisualElement.Add(toolbar);
            }
        }

        private void TeardownWindow()
        {
            rootVisualElement.Clear();
        }

        private View CreateView()
        {
            view = new View(GetNodeSpawner());
            view.StretchToParentSize();
            return view;
        }

        protected abstract INodeSpawner GetNodeSpawner();

        protected virtual Toolbar[] GetToolbars()
        {
            return new Toolbar[]
            {
                new SerializationToolbar<GraphData>(
                    view: view,
                    serializer: GetSerializer()),

                new NodeCreatorToolbar(
                    nodeCreator: new NodeCreator(
                        graphView: view, 
                        nodeSpawner: GetNodeSpawner()),
                    domainNamespace: GetType().Namespace)
            };
        }

        protected Serializer GetSerializer()
        {
            return new Serializer(
                saving: new ScriptableObjectGraphSaving(),
                loading: new ScriptableObjectLoading(),
                parentFolder: "Assets/Resources/",
                fileExtension: ".asset");
        }
    }
}
