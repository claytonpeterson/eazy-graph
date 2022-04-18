using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine;

namespace skybirdgames.eazygraph.Editor
{
    public class NodeConnector
    {
        private View graphView;

        public NodeConnector(View graphView)
        {
            this.graphView = graphView;
        }

        public void ConnectNodes(GraphData graph)
        {
            foreach (NodeData node in graph.Nodes)
            {
                CreateEdges(GetConnections(node, graph.Connections));
            }
        }

        private void CreateEdges(List<ConnectionData> connections)
        {
            foreach (var connection in connections)
            {

                if (GetGraphNodeByGUID(connection.nodeAGUID) != null &&
                    GetGraphNodeByGUID(connection.nodeAGUID).GetOutputPort(connection.nodeAPortName) == null)
                    GetGraphNodeByGUID(connection.nodeAGUID).Ports.AddOutputPort(connection.nodeAPortName, Port.Capacity.Single);

                if (GetGraphNodeByGUID(connection.nodeBGUID) != null &&
                    GetGraphNodeByGUID(connection.nodeBGUID).GetInputPort(connection.nodeBPortName) == null)
                    GetGraphNodeByGUID(connection.nodeBGUID).Ports.AddInputPort(connection.nodeBPortName, Port.Capacity.Single);


                /*
                                if (!DoesPortExist(connection.nodeAGUID, connection.nodeAPortName))
                                {
                                    GetGraphNodeByGUID(connection.nodeAGUID).Ports.AddOutputPort(connection.nodeAPortName, Port.Capacity.Single);
                                }

                                if (!DoesPortExist(connection.nodeBGUID, connection.nodeBPortName))
                                {
                                    GetGraphNodeByGUID(connection.nodeBGUID).Ports.AddInputPort(connection.nodeBPortName, Port.Capacity.Single);
                                }
                */
                CreateEdge(
                    GetOutputPort(connection.nodeAGUID, connection.nodeAPortName),
                    GetInputPort(connection.nodeBGUID, connection.nodeBPortName));
            }
        }

        private bool DoesPortExist(string nodeGUID, string portName)
        {
            return
                GetGraphNodeByGUID(nodeGUID) != null && 
                GetGraphNodeByGUID(nodeGUID).GetInputPort(portName) != null ||
                GetGraphNodeByGUID(nodeGUID).GetOutputPort(portName) != null;
        }

        private bool IsConnected(NodeData node, ConnectionData connection)
        {
            return node.GUID == connection.nodeAGUID;
        }

        private List<ConnectionData> GetConnections(NodeData node, List<ConnectionData> allConnections)
        {
            return allConnections.Where(x => IsConnected(node, x)).ToList();
        }

        private void CreateEdge(Port output, Port input)
        {
            var tEdge = new Edge
            {
                input = input,
                output = output
            };

            tEdge?.input.Connect(tEdge);
            tEdge?.output.Connect(tEdge);
            graphView.Add(tEdge);
        }

        private Port GetInputPort(string nodeGUID, string portName)
        {
            return GetGraphNodeByGUID(nodeGUID).GetInputPort(portName);
        }

        private Port GetOutputPort(string nodeGUID, string portName)
        {
            return GetGraphNodeByGUID(nodeGUID).GetOutputPort(portName);
        }

        private NodeView GetGraphNodeByGUID(string guid)
        {
            foreach (NodeView node in graphView.Nodes)
            {
                if (node.guid == guid)
                    return node;
            }
            return null;
        }
    }
}
