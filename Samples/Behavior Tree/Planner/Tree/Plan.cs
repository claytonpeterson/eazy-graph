using UnityEngine;

namespace Behavior
{
    public class Plan : IUtilityScore
    {
        private readonly Entity entity;
        private readonly Node root;
        private readonly float utility;
        private Objective objective;

        public Plan(Entity entity, Node root, float utility)
        {
            this.entity = entity;
            this.root = root;
            this.utility = utility;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Node.Status Evaluate()
        {
            return root.Evaluate(entity, this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Node Root()
        {
            return root;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns the objective that is being evaluated, null if an objective does not exist</returns>
        public Objective CurrentObjective()
        {
            return objective;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objective"></param>
        public void SetObjective(Objective objective)
        {
            this.objective = objective;
        }

        public float UtilityScore()
        {
            return utility;
        }
    }
}
