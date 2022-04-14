namespace Behavior
{
    public class Inverter : Decorator
    {
        public Inverter(Node child) : base(child) { }

        public override Status Evaluate(Entity entity, Plan tree)
        {
            switch (child.Evaluate(entity, tree))
            {
                case Status.FAILURE:
                    return Status.SUCCESS;
                case Status.SUCCESS:
                    return Status.FAILURE;
                case Status.RUNNING:
                    return Status.RUNNING;
            }
            return Status.FAILURE;
        }
    }
}
