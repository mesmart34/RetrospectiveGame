using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent Event;

    [SerializeField]
    private string ObjectTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ObjectTag)
            Event.Invoke();
    }
}
