namespace Behavior
{
    public class Selector : Composite
    {
        public Selector(Node[] children) : base(children) { }

        public override Status Evaluate(Entity entity, Plan tree)
        {
            for (int i = 0; i < children.Length; i++)
            {
                Node node = children[i];
                switch (node.Evaluate(entity, tree))
                {
                    case Status.FAILURE:
                        continue;
                    case Status.SUCCESS:
                        return Status.SUCCESS;
                    case Status.RUNNING:
                        return Status.RUNNING;
                    default:
                        continue;
                }
            }
            return Status.FAILURE;
        }
    }
}
