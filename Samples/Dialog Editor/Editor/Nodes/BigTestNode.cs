using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.dialog.Editor
{
    [Serializable]
    public class BigTestNode : NodeView
    {
        public IntegerField ageField;

        public BigTestNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "Big Test Node";

            mainContainer.style.backgroundColor = Color.red;

            this.data = data;

            AddAgeField();
        }

        protected override PortInformation GetPortInformation()
        {
            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Multi,
                OutputPortCapacity = Port.Capacity.Multi
            };
            return portInfo;
        }

        private void AddAgeField()
        {
            ageField = new IntegerField("age field")
            {
                value = data.age
            };

            ageField.RegisterValueChangedCallback((evt) =>
            {
                data.age = evt.newValue;
                Debug.Log(evt.newValue);
            });

            Add(ageField);
        }
    }

}