using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TextTyper : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI Text;

    [SerializeField]
    public float Time = 0.5f;

    [TextArea]
    public string Sentence;

    [SerializeField]
    public UnityEvent OnEndTyping;

    

    private void Start()
    {
        StartCoroutine(Type(Time));
    }

    private IEnumerator Type(float time)
    {
        foreach(var c in Sentence)
        {
            Text.text += c;
            yield return new WaitForSeconds(time);
        }
        OnEndTyping.Invoke();
    }
}
