using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(NPCsBehaviourManager))]

public class NPC_AI_Manager : MonoBehaviour
{

    

    private NavMeshAgent agent;
    private Animator anim;
    private AudioSource audioSource;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidBody;
    private NPCsBehaviourManager npcsBehaviourManager;


    private float navMeshAgentRadius = 0.2279f;
    private float colliderSize = 0.2279f;
    private float colliderHeight = 2f;
    private Vector3 colliderCenter = new Vector3(0f, 1f, 0f);

    public NPCsBehaviourManager.FireState fireState;
    private bool fireIsOn = false;

    //parameters for sit_on
    public GameObject sitLocation;
    float sitDistance = 1.35f;
    bool isWalkingTowards = false;
    bool stopTurning = false;
    bool sittingOn = false;

    private void Start()
    {
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();

        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.height = colliderHeight;
        capsuleCollider.radius = colliderSize;
        capsuleCollider.center = colliderCenter;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        npcsBehaviourManager = GetComponent<NPCsBehaviourManager>();
        fireState = npcsBehaviourManager.fireState;

        Sit_On();

    }

    float calculatePathLength(Vector3 targetPosition)//Calculates the correct path length
                                                     //by using the corners of a navMeshPath
    {
        NavMeshPath path = new NavMeshPath();

        float pathLength = 0f;

        if (agent.enabled)
        {
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
        }

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = this.gameObject.transform.position - this.gameObject.transform.up;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        //Debug.Log(path.corners.Length);
        //Debug.Log(allWayPoints.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }



        return pathLength;
    }
    public void Sit_On()
    {
        float dist = calculatePathLength(sitLocation.transform.position);
        //Debug.Log(dist);
        if (dist > sitDistance)
        {
            agent.SetDestination(sitLocation.transform.position);
            anim.SetBool("IsWalking", true);
            agent.isStopped = false;
        }
        else if (dist <= sitDistance && !stopTurning)
        {
            anim.SetBool("IsWalking", false);
            agent.isStopped = true;

            Vector3 targetDir;
            targetDir = new Vector3(sitLocation.transform.position.x - transform.position.x,
             0f, sitLocation.transform.position.z - transform.position.z);
            Quaternion rot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation,
             rot, 0.05f);

            //Debug.Log(Mathf.Abs(transform.eulerAngles.y - character.transform.eulerAngles.y));
            if ((Mathf.Abs(transform.eulerAngles.y - sitLocation.transform.eulerAngles.y) < 0.5f)
                || (Mathf.Abs(transform.eulerAngles.y - sitLocation.transform.eulerAngles.y) > 349f))
            {

                stopTurning = true;
            }
        }

    }

}
