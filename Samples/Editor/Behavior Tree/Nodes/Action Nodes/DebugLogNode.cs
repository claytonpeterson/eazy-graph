using UnityEngine;

namespace SkybirdGames.AI.BehaviorTree
{
    public class DebugLogNode : ActionNode
    {
        public string message;

        protected override void OnStart()
        {
            Debug.Log("Start: " + message);
        }

        protected override void OnStop()
        {
            Debug.Log("Stop: " + message);
        }

        protected override State OnUpdate()
        {
            Debug.Log("Update: " + message);
            return State.SUCCESS;
        }
    }
}