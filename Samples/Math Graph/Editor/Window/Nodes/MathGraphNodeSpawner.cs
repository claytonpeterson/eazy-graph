﻿using System;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathGraphNodeSpawner : INodeSpawner
    {
        public NodeView CreateNodeView(Type type, Vector2 position, TestingOutData data)
        {
            if (type == typeof(NumberNode))
            {
                return new NumberNode(position, data);
            }
            else if (type == typeof(OperatorNode))
            {
                return new OperatorNode(position, data);
            }

            return null;
        }
    }
}