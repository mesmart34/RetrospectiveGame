using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_OpenButtonText;
    [SerializeField]
    private float m_WaitTime = 0.677f;

    private Animator animator;
    private bool m_IsOpened;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AddObject()
    {

    }

    public void SetOpened()
    {
        print("Opened");
        StartCoroutine(ChangeOpenButtonText(m_WaitTime));
    }

    private IEnumerator ChangeOpenButtonText(float time)
    {
        animator.SetTrigger("Open");
        m_IsOpened = !m_IsOpened;
        yield return new WaitForSeconds(time);
        if (!m_IsOpened)
            m_OpenButtonText.text = "Открыть";
        else
            m_OpenButtonText.text = "Закрыть";
    }

}
