using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCollider : MonoBehaviour
{
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_PlayerPosition;
    private EnnemyAI m_EnemyAI;
    private bool m_coroutineStarted = false;

    private void Start()
    {
        m_EnemyAI = GetComponentInParent<EnnemyAI>();
        m_NavMeshAgent = GetComponentInParent<NavMeshAgent>();
        m_Animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider otherCol)
    {
        if(otherCol.name == "XR Origin" && !m_coroutineStarted)
        {
            StartCoroutine(runToPlayer(otherCol));
        }
    }

    private void OnTriggerStay(Collider otherCol)
    {
        if (otherCol.name == "XR Origin")
        {
            if (!m_coroutineStarted)
            {
                StartCoroutine(runToPlayer(otherCol));
            }
        }
    }

    private IEnumerator runToPlayer(Collider PlayerCol)
    {
        //init needed params
        m_coroutineStarted = true;
        m_EnemyAI.IdleAllowed = false;
        m_Animator.SetInteger("actionState", 2);
        m_NavMeshAgent.speed = 2f;
        m_PlayerPosition = PlayerCol.transform.position;
        m_NavMeshAgent.SetDestination(m_PlayerPosition);
        //loop
        while ( m_coroutineStarted &&
            m_NavMeshAgent.remainingDistance > 0.5f &&
            m_NavMeshAgent.remainingDistance < 20f)
        {
            m_PlayerPosition = PlayerCol.transform.position;
            yield return null;
        }
        //exit loop, reset params
        m_Animator.SetInteger("actionState", 0);
        m_coroutineStarted = false;
        m_EnemyAI.IdleAllowed = true;
    }
}
