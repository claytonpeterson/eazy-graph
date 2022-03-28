using UnityEngine;

public interface INodeSpawner
{
    GraphNode CreateNode(string name, Vector2 position);
}
