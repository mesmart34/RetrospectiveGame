using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public float m_AnimationTime = 1.0f;
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void Open()
    {
        m_Animator.SetTrigger("Fade");
    }

    public void Close()
    {
        m_Animator.SetTrigger("Fade");
    }

    public IEnumerator DoFading(Action action)
    {
        m_Animator.SetTrigger("Fade");
        yield return new WaitForSeconds(m_AnimationTime);
        action.Invoke();
        m_Animator.SetTrigger("Fade");
    }
}
