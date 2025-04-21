using UnityEngine;
using UnityEngine.AI;

public class NewMonoBehaviourScript : MonoBehaviour
{   
    public NavMeshAgent agent;
    public Transform followTarget;
    public float stopDistance = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update(){
        if (followTarget != null){
            // Set the destination to the followTarget's position
            agent.SetDestination(followTarget.position);

            // Check the distance between the minion and the followTarget (player)
            float distanceToTarget = Vector3.Distance(transform.position, followTarget.position);

            // If the distance is less than the stopDistance, stop the agent
            if (distanceToTarget < stopDistance)
                agent.isStopped = true; // Stop the agent's movement
            else
                agent.isStopped = false; // Keep the agent moving towards the target
        }
    }
}
