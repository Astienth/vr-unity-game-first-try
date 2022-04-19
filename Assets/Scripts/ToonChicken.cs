using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class ToonChicken : MonoBehaviour
{
    private Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ToonChickenLoop());
    }

    private IEnumerator ToonChickenLoop()
    {
        //desync all instances
        int number;
        bool isParsable = Int32.TryParse(Regex.Match(gameObject.name, @"\d+").Value, out number);
        if (isParsable)
        {
            yield return new WaitForSeconds(number);
        }
        else
        {
            yield return new WaitForSeconds(4);
        }
        //loop
        while (true)
        {
            m_Animator.SetBool("Turn Head", false);
            m_Animator.SetBool("Eat", true);
            yield return new WaitForSeconds(4);

            m_Animator.SetBool("Eat", false);
            m_Animator.SetBool("Turn Head", true);
            yield return new WaitForSeconds(2);
        }
    }
}
