using UnityEngine;

public interface INodeSpawner
{
    NodeView CreateNode(string name, Vector2 position);
}
