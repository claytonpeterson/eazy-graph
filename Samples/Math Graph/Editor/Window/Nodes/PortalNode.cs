using UnityEngine;

using skybirdgames.eazygraph.Editor;
using UnityEditor.Experimental.GraphView;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class PortalNode : ObjectNode<GraphData>
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

            mainContainer.style.backgroundColor = Color.red;

            Refresh();
        }

        public override int Value()
        {
            if (!HasObject())
                return 0;

            return graphRunner.Run(loading.Load((GraphData)obj));
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
