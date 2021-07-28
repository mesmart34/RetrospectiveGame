using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTesting : MonoBehaviour
{
    [SerializeField]
    public SerializableEvent Event;

    public void PrintSomething()
    {
        print("printed something");
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Event.Invoke();
        }
    }
}
