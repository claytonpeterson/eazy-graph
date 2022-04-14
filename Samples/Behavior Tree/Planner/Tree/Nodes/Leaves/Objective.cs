using Behavior;
public class Objective : Node
{
    private readonly Action action;
    private readonly string planName;

    public Objective(Action action, string planName="")
    {
        this.action = action;
        this.planName = planName;
    }

    public string Name()
    {
        return planName;
    }

    public override Status Evaluate(Entity entity, Plan tree)
    {
        return Status.FAILURE;
    }

    public Action GetAction()
    {
        return action;
    }
}
