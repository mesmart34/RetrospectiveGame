using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private bool m_IsNear;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("TRIGGGER");
        if (collision.gameObject.tag == "Player")
        {
            var mat = GetComponent<Material>();
            m_IsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var mat = GetComponent<Material>();
            m_IsNear = false;
        }
    }

    private void OnMouseDown()
    {
        if (m_IsNear)
            Destroy(gameObject);
    }
}
