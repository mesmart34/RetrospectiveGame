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

    [SerializeField]
    public UnityEvent OnClose;

    [SerializeField]
    private GameEvent OnThougthEnd;

    private bool Skipped = false;

    public bool CanInvokeEnd = false; 


    private void OnEnable()
    {
        StartCoroutine(Type(Time));
    }

    private void OnDisable()
    {
        Sentence = "";
        Text.text = "";
        OnClose.Invoke();
    }

    private IEnumerator Type(float time)
    {
        Skipped = false;
        foreach(var c in Sentence)
        {
            Text.text += c;
            yield return new WaitForSeconds(time);
        }
        OnEndTyping.Invoke();
    }

    public void Tap()
    {
        if (!Skipped)
            SkipPrinting();
        else
        {
            if (CanInvokeEnd)
                OnEndTyping.Invoke();
            gameObject.SetActive(false);
            OnThougthEnd.Raise();
            
            enabled = false;
        }
    }

    public void SkipPrinting()
    {
        StopAllCoroutines();
        Text.text = Sentence;
        Skipped = true;
    }
}
