using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace skybirdgames.eazygraph.Editor
{
    [Serializable]
    public abstract class NodeView : Node, IUpdate
    {
        public string guid;

        public Vector2 Position;

        private readonly Ports ports;

        public Ports Ports => ports;

        public NodeView(Vector2 position, TestingOutData data)
        {
            Position = new Vector2(position.x, position.y);

            userData = data;

            guid = Guid.NewGuid().ToString();

            SetPosition(new Rect(position.x, position.y, 100, 100));

            // Create and set up ports using the new structure 
            ports = new Ports(this);
            SetupPorts();

            Refresh();
        }

        protected abstract void SetupPorts();

        public TestingOutData Data()
        {
            return (TestingOutData)userData;
        }
/*
        public Port CreatePort(Direction portDirection, Port.Capacity capacity = Port.Capacity.Single, string portName = "")
        {
            var newPort = InstantiatePort(
                Orientation.Horizontal,
                portDirection,
                capacity,
                typeof(float));

            newPort.portName = portName;
            return newPort;
        }
*/
        public virtual void Refresh()
        {
            RefreshExpandedState();
            RefreshPorts();
        }

        public Port GetPort(string portName)
        {
            foreach (var child in inputContainer.Children())
            {
                var port = (Port)child;
                if (port == null)
                    continue;

                if (port.portName == portName)
                    return port;
            }
            foreach (var child in outputContainer.Children())
            {
                var port = (Port)child;
                if (port == null)
                    continue;

                if (port.portName == portName)
                    return port;
            }
            return null;
        }

        public abstract void Update();
    }
}
