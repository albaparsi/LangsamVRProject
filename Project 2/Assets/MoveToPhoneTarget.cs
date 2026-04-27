using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPhoneTarget : MonoBehaviour
{
    public Transform handTransform;
    public Transform phone;
    public Transform phoneTarget;
    public AudioSource talkAudio;

    private NavMeshAgent agent;
    private Animator anim;
    private bool hasArrived = false;
    private bool isWandering = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        talkAudio = GetComponent<AudioSource>();

        agent.SetDestination(phoneTarget.position);
    }

    void Update()
    {
        // Update animation
        anim.SetFloat("Speed", agent.velocity.magnitude);

        // Check if arrived
        if (!hasArrived && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            hasArrived = true;
            agent.isStopped = true;

            StartCoroutine(PhoneSequence());
        }
    }
    
    IEnumerator PhoneSequence()
    {

        // Start pickup animation
        anim.SetTrigger("PickUp");

        // Wait a bit (adjust this timing)
        yield return new WaitForSeconds(1.2f);

        Rigidbody rb = phone.GetComponent<Rigidbody>();
        if (rb != null) //Disable physics before attaching to hand
        {
            rb.isKinematic = true;
            rb.angularVelocity = Vector3.zero;
        }
        // Grab phone (attach to hand)
        phone.SetParent(handTransform);
        phone.localPosition = Vector3.zero;
        phone.localRotation = Quaternion.identity;

        // Continue sequence
        yield return new WaitForSeconds(1f);

        anim.SetTrigger("Talk");
        if (talkAudio != null)
        {
            talkAudio.Play();
        }
        yield return new WaitForSeconds(6f);
        if (talkAudio != null && talkAudio.isPlaying)
        {
            talkAudio.Stop();
        }
        anim.SetTrigger("PutDown");

        yield return new WaitForSeconds(1.5f);

        phone.SetParent(null);

        phone.position = phoneTarget.position;
        phone.rotation = phoneTarget.rotation;

        // Put phone back
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        //Walk freely again
        anim.SetTrigger("WalkAgain");
        agent.isStopped = false;
        hasArrived = false;

        if (!isWandering)
        {
            isWandering = true;
            StartCoroutine(ResumeWandering());
        }
    }
    IEnumerator ResumeWandering()
    {
        yield return new WaitForSeconds(1f);

        agent.isStopped = false;

        while (true)
        {
            Vector3 newPos = RandomNavSphere(transform.position, 2f);
            agent.SetDestination(newPos);

            yield return new WaitForSeconds(3f);
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