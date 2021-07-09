using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private Dialogue m_Dialogue;

    private void Start()
    {
       
    }

    public void TriggerDialog()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(m_Dialogue);
    }
}
