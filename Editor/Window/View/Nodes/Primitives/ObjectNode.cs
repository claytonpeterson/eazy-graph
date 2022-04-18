using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public abstract class ObjectNode: DynamicOutputNode, IContainsValue
    {
        protected Object obj;

        protected ObjectField objectField;

        public ObjectNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            obj = Resources.Load<GraphData>(Data().name);

            Add(InputField());

            Refresh();
        }

        /*public override void Update()
        {
            if (obj == null)
                return;

            var gd = (GraphData)obj;
            var graph = loading.Load(gd);

            objectField.label = graph.ToString();
            Data().age = Value();

            //output.UpdateOutputConnections();
        }*/

        public abstract int Value();

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

                if (obj != null)
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
    }
}
