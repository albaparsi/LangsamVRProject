using UnityEngine;
using UnityEngine.AI;

public class Wolf_Wandering : MonoBehaviour
{
    public float roamRadius = 3f;
    public float waitTime = 3f;

    private NavMeshAgent agent;
    private Animator anim;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        timer = waitTime;
    }

    void Update()
    {
        // Update animation speed
        anim.SetFloat("Speed", agent.velocity.magnitude);

        // Wander logic
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            Vector3 newPos = RandomNavSphere(transform.position, roamRadius);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, distance, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return origin;
    }
}