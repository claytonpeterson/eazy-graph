using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public class RunableSerializationToolbar<T> : SerializationToolbar<T>
    {
        private readonly IGraphRunner graphRunner;

        public RunableSerializationToolbar(View view, Serializer serializer, IGraphRunner runner) : base(view, serializer)
        {
            graphRunner = runner;

            Add(RunButton());
        }

        private Button RunButton()
        {
            return new Button(clickEvent: () =>
            {
                Save();

                GraphData graph = serializer.Load(obj.name);

                Debug.Log(graphRunner.Run(graph));
            })
            { text = "Run Graph" };
        }
    }
}