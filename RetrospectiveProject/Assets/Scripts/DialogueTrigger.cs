using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{

    [SerializeField]
    public Dialogue.DialogueGraph Dialogue;

    [SerializeField]
    private GameObject DialogueManagerObject;

    [SerializeField]
    private UnityEvent OnDialogueTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnDialogueTriggered.Invoke();
            DialogueManagerObject.GetComponent<DialogueManager>().SetDialogueGraph(Dialogue);
            //DialogueManagerObject.transform.GetChild(0).gameObject.SetActive(true);
            //collision.GetComponent<PlayerController>().enabled = false;
        }
    }

}
