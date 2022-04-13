using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class PortalNode : NodeView, IContainsValue, IUpdate
    {
        private readonly IGraphRunner graphRunner;
        private readonly ILoadGraph loading;

        protected Object obj;
        private ObjectField objectField;

        public PortalNode(Vector2 position, TestingOutData data, IGraphRunner runner) : base(position, data)
        {
            graphRunner = runner;
            loading = new ScriptableObjectLoading();

            mainContainer.style.backgroundColor = Color.red;

            outputContainer.Add(
                child: CreatePort(Direction.Output, Port.Capacity.Single, "Output"));

            Add(InputField());

            Refresh();
        }

        private ObjectField InputField()
        {
            objectField = new ObjectField(label: "hello")
            {
                objectType = typeof(GraphData)
            };

            objectField.SetValueWithoutNotify(obj);
            objectField.MarkDirtyRepaint();
            objectField.RegisterValueChangedCallback(evt => {
                obj = evt.newValue;

                Update();
            });

            return objectField;
        }

        public void Update()
        {
            var gd = (GraphData)obj;
            var graph = loading.Load(gd);
            var value = graphRunner.Run(graph);

            objectField.label = value.ToString();
        }

        public int Value()
        {
            var gd = (GraphData)obj;
            var graph = loading.Load(gd);

            return graphRunner.Run(graph);
        }
    }
}
