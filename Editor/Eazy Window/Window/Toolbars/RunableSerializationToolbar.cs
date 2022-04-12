using UnityEngine.UIElements;

public class RunableSerializationToolbar<T> : SerializationToolbar<T>
{
    private IGraphRunner graphRunner;

    public RunableSerializationToolbar(EditorGraphView view, Serializer serializer, IGraphRunner runner) : base(view, serializer)
    {
        graphRunner = runner;

        Add(RunButton());
    }

    private Button RunButton()
    {
        return new Button(clickEvent: () =>
        {
            Graph graph = serializer.Load(obj.name);
            graphRunner.Run(graph);
        })
        { text = "Run Graph" };
    }
}
