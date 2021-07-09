using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_Text;
    private Queue<string> m_Sentences = new Queue<string>();
    private Animator m_Animator;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        m_Animator.SetTrigger("Open");
        print("Started dialogue with" + dialogue.m_Name);
        m_Sentences.Clear();
        foreach(var sentence in dialogue.m_Sentences)
        {
            m_Sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    private IEnumerator TypeSentence(string sentence)
    {
        m_Text.SetText("");
        yield return new WaitForSeconds(0.5f);
        foreach (var c in sentence)
        {
            m_Text.text += c;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void DisplayNextSentence()
    {
        if (m_Sentences.Count == 0)
        {
            EndDialogue();
            m_Animator.SetTrigger("Open");
            FindObjectOfType<PlayerController>().m_IsAbleToMove = true;
            return;
        }
        var sentence = m_Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        print(sentence);
    }

    private void EndDialogue()
    {
        print("The dialoue is ended");
    }

}
