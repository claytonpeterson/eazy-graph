using UnityEngine;
using UnityEditor.UIElements;
using System;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.dialog.Editor
{
    [Serializable]
    public class BigTestNode : MultiPortNode
    {
        public IntegerField ageField;

        public BigTestNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "Big Test Node";

            mainContainer.style.backgroundColor = Color.red;

            this.data = data;

            AddAgeField();
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