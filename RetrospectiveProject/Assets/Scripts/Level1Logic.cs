using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using TMPro;

public class Level1Logic : MonoBehaviour
{

    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private Inventory inventory;
    private GameObject player;
    [SerializeField]
    private GameObject Thoughts;

    [SerializeField]
    private TextMeshProUGUI QuestDescription;

    [Header("Quest 1")]
    [SerializeField]
    private GameObject Quest1Trigger;

    [Space]

    [Header("Quest 2")]
    [SerializeField]
    private int Quest2TriggerEntries = 0;
    [SerializeField]
    private GameObject StaircaseTrigger;
    [SerializeField]
    private GameObject Quest2Trigger1;
    [SerializeField]
    private GameObject Quest2Trigger2;

    [Space]

    [Header("Quest 3")]
    [SerializeField]
    private GameObject[] ObjectsToFind;
    [SerializeField]
    private GameObject Quest3Trigger1;





    private void Awake()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        inventory.ClearInventory();
        foreach (var obj in ObjectsToFind)
            obj.tag = "Untagged";
        Quest1();
    }

    public void ShowThought(string sentence)
    {
        Thoughts.GetComponent<TextTyper>().Sentence = sentence;
        Thoughts.SetActive(true);
    }

    public void OpenDialog(DialogueGraph dialogue)
    {
        player.GetComponent<PlayerController>().m_IsAbleToMove = false;
        player.GetComponent<PlayerController>().m_Animator.SetBool("Run", false);
        dialogueManager.SetDialogueGraph(dialogue);
        dialogueManager.gameObject.SetActive(true);
    }

    public void CloseDialog()
    {
        dialogueManager.gameObject.SetActive(false);
        player.GetComponent<PlayerController>().m_IsAbleToMove = true;
    }

    public void Quest1()
    {
        QuestDescription.text = "Поздоровайтесь с бабушкой";
        Quest1Trigger.SetActive(true);

    }

    public void Quest1Complete()
    {
        CloseDialog();
        Destroy(Quest1Trigger);
        Quest2();
    }

    public void Quest2()
    {
        QuestDescription.text = "Исследуйте дом";
        StaircaseTrigger.SetActive(true);
        Quest2Trigger2.SetActive(true);
    }

    public void Quest2GotUpToSecondFloor()
    {
        Quest2Trigger1.SetActive(true);
    }

    public void Quest2Complete()
    {
        CloseDialog();
        Quest2Trigger1.SetActive(false);
        Quest2Trigger2.SetActive(false);
        Quest3();
    }

    public void Quest3()
    {
        QuestDescription.text = "Соберите все предметы";
        foreach (var obj in ObjectsToFind)
            obj.tag = "Item";
        //Quest3Trigger1.SetActive(true);
    }

    public void Quest3Complete()
    {
        Quest3Trigger1.SetActive(false);
        QuestDescription.text = "Посмотрите вещи в чулане";
        CloseDialog();

    }
}
