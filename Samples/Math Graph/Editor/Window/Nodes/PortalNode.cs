using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class PortalNode : NodeView, IContainsValue, IUpdate
    {
        // For running the graph
        private readonly IGraphRunner graphRunner;
        private readonly ILoadGraph loading;

        private readonly OutputUpdater output;

        protected Object obj;
        private ObjectField objectField;

        public PortalNode(Vector2 position, TestingOutData data, IGraphRunner runner) : base(position, data)
        {
            graphRunner = runner;
            loading = new ScriptableObjectLoading();

            output = new OutputUpdater(this);

            obj = Resources.Load<GraphData>(Data().name);

            Debug.Log(string.Format("Creating portal node {0}", obj != null));

            mainContainer.style.backgroundColor = Color.red;

            outputContainer.Add(
                child: CreatePort(Direction.Output, Port.Capacity.Single, "Output"));

            Add(InputField());

            Refresh();
        }

        private ObjectField InputField()
        {
            objectField = new ObjectField()
            {
                objectType = typeof(GraphData)
            };

            objectField.SetValueWithoutNotify(obj);
            objectField.MarkDirtyRepaint();
            objectField.RegisterValueChangedCallback(evt => 
            {
                obj = evt.newValue;

                if(obj != null)
                {
                    Data().name = evt.newValue.name;
                }
                else
                {
                    Data().name = "";
                    Data().age = 0;
                }

                Update();
            });

            return objectField;
        }

        public void Update()
        {
            if (obj == null)
                return;

            var gd = (GraphData)obj;
            var graph = loading.Load(gd);
            var value = graphRunner.Run(graph);

            objectField.label = value.ToString();

            Data().age = Value();

            output.UpdateOutputConnections();
        }

        public int Value()
        {
            if (obj == null)
                return 0;

            var gd = (GraphData)obj;
            var graph = loading.Load(gd);

            return graphRunner.Run(graph);
        }
    }
}
