namespace Behavior
{
    public abstract class Node
    {
        public enum Status
        {
            RUNNING,
            SUCCESS,
            FAILURE
        }

        // Evaluates the behavior tree and returns a status
        public abstract Status Evaluate(Entity entity, Plan tree);
    }
}
