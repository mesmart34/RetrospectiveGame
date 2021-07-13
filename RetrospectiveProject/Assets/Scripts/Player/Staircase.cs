using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Staircase : MonoBehaviour
{
    [SerializeField] 
    private Staircase Other;
    private bool Spawned;
    private GameObject FadeScreen;
    private bool IsPlayerIn = false;
    private bool Pressed = false;
    private Collider2D Player;

    private void Awake()
    {
        FadeScreen = GameObject.FindGameObjectWithTag("FadeScreen");    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Spawned = false;
            collision.gameObject.GetComponent<PlayerController>().enabled = true;
            IsPlayerIn = false;
        }
    }

    private void Update()
    {
        Pressed = false;
        if(Input.GetButtonDown("Jump"))
        {
            Pressed = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Pressed && collision.tag == "Player")
        {
            Other.Spawned = true;
            StartCoroutine(FadeScreen.GetComponent<FadeScreen>().DoFading(() =>
            {
                collision.transform.position = new Vector3(Other.gameObject.transform.position.x, Other.gameObject.transform.position.y, 0);
                collision.gameObject.GetComponent<PlayerController>().enabled = false;
                Camera.main.GetComponent<CameraFollow>().SetOnTargetImmediate();
            }));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !Spawned)
        {
            IsPlayerIn = true;
        }
    }
}
