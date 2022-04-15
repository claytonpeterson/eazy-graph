using UnityEditor;
using skybirdgames.eazygraph.Editor;

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

        protected override INodeSpawner GetNodeSpawner()
        {
            return new Spawner();
        }
    }
}
