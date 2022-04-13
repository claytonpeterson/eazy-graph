using UnityEngine;
using UnityEditor.UIElements;
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

            AddAgeField();
        }

        protected override void SetupPorts()
        {
        }

        private void AddAgeField()
        {
            ageField = new IntegerField("age field")
            {
                value = Data().age
            };

            ageField.RegisterValueChangedCallback((evt) =>
            {
                Data().age = evt.newValue;
                Debug.Log(evt.newValue);
            });

            Add(ageField);
        }
    }
}