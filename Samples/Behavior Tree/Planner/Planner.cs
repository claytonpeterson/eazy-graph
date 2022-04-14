using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Behavior;

public class Planner : MonoBehaviour
{
    [SerializeField] private Vector3[] patrolRoute;

    [SerializeField]
    private Entity entity;

    [SerializeField]
    private float tickRate = 1f;

    private WaitForSeconds tick;
    private Plan plan;
    private List<IUtilityScore> plans;

    private void Start()
    {
        tick = new WaitForSeconds(tickRate);
        plans = new List<IUtilityScore>()
        {
            //new Plan(entity: entity, root: FarmingBehaviors.FollowPath(entity, new Vector3[] { Vector3.zero, new Vector3(-2, 0, 2) }), utility: 0.6f),
            //new Plan(entity: entity, root: FarmingBehaviors.BuildFarm(entity, new string[] { "shovel", "bucket" }), utility: 0.5f)
        };

        plan = SelectPlan(plans) as Plan;

        StartCoroutine(Run());
    }

    /// <summary>
    /// Evaluates the behavior tree at a given tick rate
    /// </summary>
    private IEnumerator Run()
    {
        while (true)
        {
            plan = SelectPlan(plans) as Plan;

            if (plan != null)
            {
                if (plan.Evaluate() == Node.Status.SUCCESS)
                {
                    print("complete!");
                    plans.Remove(plan);
                    plan = null;
                }
            }
            yield return tick;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Objective CurrentObjective()
    {
        if (plan != null)
        {
            return plan.CurrentObjective();
        }
        return null;
    }

    public bool HasObjective()
    {
        return CurrentObjective() != null;
    }

    private IUtilityScore SelectPlan(List<IUtilityScore> utilities)
    {
        float utility;
        float highestUtility = 0;
        IUtilityScore highestUtilityScore = null;

        for (int i = 0; i < utilities.Count; i++)
        {
            utility = utilities[i].UtilityScore();
            if (utility > highestUtility)
            {
                highestUtility = utility;
                highestUtilityScore = utilities[i];
            }
        }
        return highestUtilityScore;
    }
}
