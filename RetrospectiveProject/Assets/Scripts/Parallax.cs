using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform Camera;
    private float StartPos, Length;
    [SerializeField]
    private float Strenth;

    private void Start()
    {
        StartPos = transform.position.x;
    }

    private void Update()
    {
        var temp = Camera.transform.position.x * (1 - Strenth);
        var dist = Camera.transform.position.x * Strenth;
        transform.position = new Vector3(StartPos + dist, transform.position.y, transform.position.z);
    }
}
