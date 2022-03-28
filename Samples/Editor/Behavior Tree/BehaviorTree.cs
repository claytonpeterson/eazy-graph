
using UnityEngine;

namespace SkybirdGames.AI.BehaviorTree
{
    public abstract class BehaviorTree
    {
        [SerializeField]
        private Node root;

        public Node.State Update()
        {
            return root.Update();
        }
    }
}
