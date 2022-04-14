using UnityEngine;

namespace Behavior
{
    public abstract class Composite : Node
    {
        protected Node[] children;
        protected int index = 0;

        public Composite(Node[] children)
        {
            this.children = children;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns the active node in the branch</returns>
        public Node CurrentNode()
        {
            return children[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns the following node in the branch if it exists, otherwise returns null</returns>
        public Node Next()
        {
            if (index < children.Length-1)
            {
                index++;
                return children[index];
            }
            return null;
        }
    }
}
