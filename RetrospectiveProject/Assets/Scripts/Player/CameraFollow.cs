using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private Vector3 m_Offset;
    [SerializeField]
    private float m_Speed = 1.0f;
    [SerializeField]
    private bool Horizontal = true;
    [SerializeField]
    private bool Vertical = true;

    private void LateUpdate()
    {
        var newTarget = m_Target.position;
        if (!Horizontal)
            newTarget.x = 0;
        if (!Vertical)
            newTarget.y = 0;
        transform.position = Vector3.Lerp(transform.position, newTarget + m_Offset, Time.deltaTime * m_Speed);
    }
}
