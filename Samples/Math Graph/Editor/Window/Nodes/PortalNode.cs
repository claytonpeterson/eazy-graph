using UnityEditor.Experimental.GraphView;
using UnityEngine;

using skybirdgames.eazygraph.Editor;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class PortalNode : ObjectNode
    {
        // For running the graph
        private readonly IGraphRunner graphRunner;
        private readonly ILoadGraph loading;

        private readonly OutputUpdater output;

        public PortalNode(Vector2 position, TestingOutData data, IGraphRunner runner) : base(position, data)
        {
            graphRunner = runner;
            loading = new ScriptableObjectLoading();

            output = new OutputUpdater(this);

            obj = Resources.Load<GraphData>(Data().name);

            mainContainer.style.backgroundColor = Color.red;

            Refresh();
        }

        public override int Value()
        {
            if (obj == null)
                return 0;

            var gd = (GraphData)obj;
            var graph = loading.Load(gd);

            return graphRunner.Run(graph);
        }

        protected override void SetupPorts()
        {
            Ports.AddOutputPort("port 1", Port.Capacity.Single);
        }

        public override void Update()
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
    }
}
