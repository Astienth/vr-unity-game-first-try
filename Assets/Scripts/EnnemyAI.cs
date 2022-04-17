using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{
    private bool idleStarted = false;
    private bool idleAllowed = true;
    public bool IdleAllowed
    {
        get { return idleAllowed; }
        set { idleAllowed = value; }
    }
    private NavMeshAgent navAgent;
    public float walkRadius;
    private Animator AnimEnnemy;

    private void Start()
    {
        AnimEnnemy = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if (!idleStarted && idleAllowed)
        {
            StartCoroutine(MoveToCoroutine("Walk"));
        }
    }

    private IEnumerator MoveToCoroutine(string name)
    {
        idleStarted = true;
        while (idleAllowed)
        {
            bool arrived = false;
            //calculate target position
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;
            //start path
            navAgent.speed = (name == "Run") ? 2f : 1f;
            int state = (name == "Run") ? 2 : 1;
            navAgent.SetDestination(finalPosition);
            AnimEnnemy.SetInteger("actionState", state);
            while (!arrived && idleAllowed)
            {
                if (!navAgent.pathPending && navAgent.remainingDistance == 0)
                {
                    arrived = true;
                    AnimEnnemy.SetInteger("actionState", 0);
                }
                yield return null;
            }
            int delay = Random.Range(4, 8);
            yield return new WaitForSeconds(delay);
        }
        idleStarted = false;
    }
}
