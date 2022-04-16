using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

using skybirdgames.eazygraph.Editor;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class NumberNode : NodeView, IContainsValue
    {
        private FloatField numberField;

        private readonly OutputUpdater output;

        public NumberNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            output = new OutputUpdater(this);

            title = "Number";

            Data().name = "Number";

            mainContainer.style.backgroundColor = Color.green;

            AddNumberField();
            Refresh();
        }

        public int Value()
        {
            return (int)numberField.value;
        }

        private void AddNumberField()
        {
            numberField = new FloatField()
            {
                value = Data().age
            };

            numberField.RegisterValueChangedCallback((evt) =>
            {
                Data().age = (int)evt.newValue;
                output.UpdateOutputConnections();
            });

            Add(numberField);
        }

        protected override void SetupPorts()
        {
            var port = CreatePort(Direction.Output, Port.Capacity.Single, "Output");
            port.AddManipulator(new EdgeConnector<Edge>(new NodeUpdateManipulator(this)));
            outputContainer.Add(port);
        }

        public override void Update()
        {
            output.UpdateOutputConnections();
        }
    }
}