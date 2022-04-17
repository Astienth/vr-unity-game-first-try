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
            StartCoroutine(idleCoroutine());
            idleStarted = true;
        }
    }

    private IEnumerator MoveToCoroutine(string animName)
    {
        bool arrived = false;
        Animator AnimEnnemy = ennemy.GetComponent<Animator>();
        AnimEnnemy.Play(animName);
        //calculate target position
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
        //start path
        navAgent.SetDestination(finalPosition);
        while (!arrived)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance + 0.1f)
            {
                arrived = true;
            }
            yield return null;
        }
        AnimEnnemy.Play("Idle");
        yield return new WaitForSeconds(Random.Range(4,8));
    }

    private IEnumerator idleCoroutine()
    {
        while (idleAllowed)
        {
            yield return MoveToCoroutine("Walk");
        }
    }
}
