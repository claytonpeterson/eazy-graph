using UnityEngine;

namespace Behavior
{
    public class Sequence : Composite
    {
        public Sequence(Node[] children) : base(children) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tree"></param>
        /// <returns>Returns the current status of the sequence</returns>
        public override Status Evaluate(Entity entity, Plan tree)
        {
            // if the current node is an objective, save it
            if (CurrentNode() is Objective objective)
            {
                tree.SetObjective(objective);
            }

            switch (CurrentNode().Evaluate(entity, tree))
            {
                case Status.FAILURE:
                    return Status.FAILURE;
                case Status.SUCCESS:
                    if (Next() == null)
                        return Status.SUCCESS;
                    break;
            }
            return Status.RUNNING;
        }
    }
}
