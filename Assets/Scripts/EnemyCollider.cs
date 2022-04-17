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

    private void Start()
    {
        m_EnemyAI = GetComponentInParent<EnnemyAI>();
        m_NavMeshAgent = GetComponentInParent<NavMeshAgent>();
        m_Animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider otherCol)
    {
        if(otherCol.name == "XR Origin")
        {
            m_EnemyAI.IdleAllowed = false;
            m_PlayerPosition = otherCol.transform.position;
            string move = (m_Animator.HasState(0, Animator.StringToHash("Run"))) ? "Runn" : "Walk";
            StartCoroutine(runToPlayer(move));
        }
    }

    private void OnTriggerStay(Collider otherCol)
    {
        if (otherCol.name == "XR Origin")
        {
            m_PlayerPosition = otherCol.transform.position;
            Debug.Log(m_PlayerPosition);
        }
    }

    private void OnTriggerExit(Collider otherCol)
    {
        if (otherCol.name == "XR Origin")
        {
            m_EnemyAI.IdleAllowed = true;
        }
    }

    private IEnumerator runToPlayer(string move)
    {
        m_Animator.SetBool("is" + move + "ing", true);
        while (m_NavMeshAgent.remainingDistance > 1f)
        {
            m_NavMeshAgent.SetDestination(m_PlayerPosition);
            yield return null;
        }
        m_Animator.SetBool("is" + move + "ing", false);
    }
}
