using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Dialogue;

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

    private Chat RootChat;

    private void Start()
    {
        RootChat = (Chat)Graph.nodes[0];
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        Text.text = RootChat.text;
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
    private void OnButtonPressed(int option)
    {
        RootChat.AnswerQuestion(option);
        RootChat = Graph.current;
        foreach (Transform child in AnswersParent)
        {
            Destroy(child.gameObject);
        }
        ShowDialogue();
    }
}
