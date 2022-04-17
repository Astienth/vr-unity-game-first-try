using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyAI : MonoBehaviour
{
    public GameObject ennemy;
    private bool idleStarted = false;
    private bool idleAllowed = true;
    public NavMeshAgent navAgent;
    public float walkRadius;

    public void Update()
    {
        if (!idleStarted && idleAllowed)
        {
            StartCoroutine(WalkToCoroutine());
            idleStarted = true;
        }
    }

    private IEnumerator WalkToCoroutine()
    {
        Animator AnimEnnemy = ennemy.GetComponent<Animator>();

        while (idleAllowed)
        {
            bool arrived = false;
            //calculate target position
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1);
            Vector3 finalPosition = hit.position;
            //start path
            navAgent.SetDestination(finalPosition);
            AnimEnnemy.SetBool("isWalking", true);
            while (!arrived)
            {
                if (!navAgent.pathPending && navAgent.remainingDistance == 0)
                {
                    arrived = true;
                    AnimEnnemy.SetBool("isWalking", false);
                }
                yield return null;
            }
            int delay = Random.Range(4, 8);
            yield return new WaitForSeconds(delay);
        }
    }
}
