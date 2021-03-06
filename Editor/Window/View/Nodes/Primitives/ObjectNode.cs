using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public abstract class ObjectNode<T> : DynamicOutputNode, IContainsValue where T : ScriptableObject
    {
        protected Object obj;

        protected ObjectField objectField;

        public ObjectNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            obj = Resources.Load<T>(Data().name);

            Add(InputField());

            Refresh();
        }

        public abstract int Value();

        protected bool HasObject() => obj != null;

        private ObjectField InputField()
        {
            objectField = new ObjectField()
            {
                objectType = typeof(T)
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
