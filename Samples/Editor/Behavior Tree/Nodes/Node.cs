
namespace SkybirdGames.AI.BehaviorTree
{
    public abstract class Node
    {
        public enum State
        {
            RUNNING,
            FAILURE,
            SUCCESS
        }

        private State state;

        private bool started;

        public State Update()
        {
            if(!started)
            {
                OnStart();
                started = true;
            }

            state = OnUpdate();

            if (state == State.FAILURE || state == State.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        protected abstract void OnStart();

        protected abstract void OnStop();

        protected abstract State OnUpdate();
    }
}
