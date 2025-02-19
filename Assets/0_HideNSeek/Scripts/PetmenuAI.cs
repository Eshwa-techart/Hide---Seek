using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetmenuAI : MonoBehaviour
{
    //NavMeshAgent myNavAgent;

    //Vector3 newPos;
    //bool moveAI;
    //public float timetoChangePos;
    //public float aiMoveSpeed;
    //float timer;




    //public BoxCollider _collider;

    //void Start()
    //{
    //    myNavAgent = GetComponent<NavMeshAgent>();
    //    moveAI = true;
    //}

    //void Update()
    //{
    //    if (moveAI)
    //    {
    //        timer += Time.deltaTime;

    //        if (timer >= timetoChangePos)
    //        {
    //            //newPos = NewRandomNavSphere(transform.position, aiMoveSpeed * 3, -1);
    //            newPos = RandomPointInBounds(_collider.bounds);
    //            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //            cube.transform.localScale = Vector3.one;
    //            cube.transform.position = newPos;
    //            myNavAgent.SetDestination(newPos);
    //            timer = 0;
    //        }

    //    }


    //}

    //Vector3 NewRandomNavSphere(Vector3 origin, float dist, int layermask)
    //{
    //    Vector3 randDirection = Random.insideUnitSphere * dist;
    //    randDirection += origin;
    //    NavMeshHit navHit;
    //    NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
    //    return navHit.position;
    //}
    //public static Vector3 RandomPointInBounds(Bounds bounds)
    //{
    //    return new Vector3(
    //        Random.Range(bounds.min.x + 1, bounds.max.x - 1),
    //        1.35f,
    //        Random.Range(bounds.min.z + 1, bounds.max.z - 1)
    //    );
    //}
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
}
