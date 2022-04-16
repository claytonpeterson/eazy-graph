using UnityEditor.Experimental.GraphView;

namespace skybirdgames.eazygraph.Editor
{
    public class DynamicPorts : Ports
    {
        private int inputIndex;
        private int outputIndex;

        public DynamicPorts(NodeView view) : base(view)
        {
        }

        public void AddInputPort(Port.Capacity capacity)
        {
            AddInputPort("port: " + inputIndex, capacity);
            inputIndex++;
        }

        public void AddOutputPort(Port.Capacity capacity)
        {
            AddOutputPort("port: " + outputIndex, capacity);
            outputIndex++;
        }
    }
}