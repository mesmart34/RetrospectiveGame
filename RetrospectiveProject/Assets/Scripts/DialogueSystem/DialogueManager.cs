using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dialogue;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private DialogueGraph Graph;

    [SerializeField]
    private Transform AnswersParent;

    [SerializeField]
    private GameObject AnswerPrefab;

    [SerializeField]
    private TextMeshProUGUI Text;

    [SerializeField]
    private TextMeshProUGUI Name;

    [SerializeField]
    private Image AvatarSprite;

    private Chat RootChat;

    [SerializeField]
    private UnityEvent OnDialogueEnded;

    public string LastSentence;

    private void Start()
    {
        RootChat = (Chat)Graph.nodes[0];
        ShowDialogue();
    }

    public void SetDialogueGraph(DialogueGraph graph)
    {
        Graph = graph;
        RootChat = (Chat)Graph.nodes[0];
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        foreach (Transform child in AnswersParent)
        {
            Destroy(child.gameObject);
        }
        Text.text = RootChat.text;
        Name.text = RootChat.character.name;
        AvatarSprite.sprite = RootChat.character.Avatar;
        for (var i = 0; i < RootChat.answers.Count; i++)
        {
            var answerObject = Instantiate(AnswerPrefab, AnswersParent);
            answerObject.GetComponentInChildren<TextMeshProUGUI>().text = RootChat.answers[i].text;
            var option = i;
            answerObject.GetComponent<Button>().onClick.AddListener(()=>{
                OnButtonPressed(option);
            });
        }
    }

    private void EndDialogue(int option)
    {
        OnDialogueEnded.Invoke();
    }

    private void OnButtonPressed(int option)
    {
        LastSentence = RootChat.answers[option].text;
        var temp = RootChat;
        RootChat.AnswerQuestion(option);
        RootChat = Graph.current;
        if (temp == RootChat)
        {
            EndDialogue(option);
            return;
        }
        foreach (Transform child in AnswersParent)
        {
            Destroy(child.gameObject);
        }
        ShowDialogue();
    }
}
