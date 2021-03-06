using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public class Ports
    {
        private readonly NodeView view;

        public Ports(NodeView view)
        {
            this.view = view;
        }

        public int InputPortCount() { return view.inputContainer.childCount; }

        public int OutputPortCount() { return view.outputContainer.childCount; }

        public List<Edge> GetInputConnections()
        {
            return GetConnections(view.inputContainer);
        }

        public List<Edge> GetOutputConnections()
        {
            return GetConnections(view.outputContainer);
        }

        public Port GetInputPort(string portName)
        {
            return SearchContainerForPort(portName, view.inputContainer); 
        }

        public Port GetOutputPort(string portName)
        {
            return SearchContainerForPort(portName, view.outputContainer);
        }

        public Port AddInputPort(string portName, Port.Capacity capacity)
        {
            return AddPort(portName, capacity, Direction.Input, view.inputContainer);
        }

        public Port AddOutputPort(string portName, Port.Capacity capacity)
        {
            return AddPort(portName, capacity, Direction.Output, view.outputContainer);
        }

        private Port AddPort(string portName, Port.Capacity capacity, Direction direction, VisualElement container)
        {
            var port = CreatePort(direction, capacity, portName);
            container.Add(port);
            return port;
        }

        private Port CreatePort(Direction portDirection, Port.Capacity capacity, string portName = "")
        {
            // Create
            var port = view.InstantiatePort(
                Orientation.Horizontal,
                portDirection,
                capacity,
                typeof(float));

            // Add manipulator
            port.AddManipulator(new EdgeConnector<Edge>(new NodeUpdateManipulator(view)));

            // Name 
            port.portName = portName;
            return port;
        }

        private Port SearchContainerForPort(string portName, VisualElement container)
        {
            foreach (VisualElement child in container.Children())
            {
                var port = (Port)child;
                if (port != null && port.portName == portName)
                {
                    return port;
                }
            }
            return null;
        }

        private List<Edge> GetConnections(VisualElement container)
        {
            var connections = new List<Edge>();
            foreach (var child in container.Children())
            {
                connections.AddRange(((Port)child).connections);
            }
            return connections;
        }
    }
}