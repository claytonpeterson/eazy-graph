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

        public virtual void Refresh()
        {
            RefreshExpandedState();
            RefreshPorts();
        }

        public Port GetPort(string portName)
        {
            return Ports.GetPort(portName);
        }

        public abstract void Update();
    }
}
