using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Options")]
    [SerializeField]
    private float m_Speed = 10.0f;
    [SerializeField]
    private bool MobileInput = false;
    [SerializeField]
    private Inventory m_Inventory;
    [SerializeField]
    private float m_PickUpDistance = 1.0f;
    public bool m_IsAbleToMove = true;
    
    [Header("Camera Shaking")]
    private Camera m_Camera;
    [SerializeField]
    private float m_MagnitudeToZoomCamera = 1.0f;
    [SerializeField]
    private float m_CameraZoomSpeed = 1.0f;
    [SerializeField]
    private float m_CameraZoom = 2.0f;
    private float m_CameraZoomOriginal;


    private bool m_Jump = false;
    private CharacterController2D m_Controller;
    private float m_Move = 0.0f;
    private bool m_MobileMoveButton;
    private float m_MobileMoveDir;
    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_Controller = GetComponent<CharacterController2D>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Camera = Camera.main;
        Physics2D.IgnoreLayerCollision(7, 6);
        m_CameraZoomOriginal = m_Camera.orthographicSize;
    }

    public bool GetPlayerDirection()
    {
        return m_Controller.IsFacedRight();
    }

    public void Jump()
    {
        m_Jump = true;
    }

    public void SetMobileMoveDirection(float value)
    {
        m_MobileMoveDir = value;
    }

    public void MoveButtonPressed()
    {
        print("pressed");
        m_MobileMoveButton = true;
    }

    public void MoveButtonUnpressed()
    {
        print("unpressed");
        m_MobileMoveButton = false;
    }

    private void TouchMove()
    {
        if(MobileInput)
            m_Move = m_Speed;
    }

    private void Update()
    {
        if (m_MobileMoveButton)
            TouchMove();
        if (!MobileInput && m_IsAbleToMove)
        {
            m_Move = Input.GetAxisRaw("Horizontal") * m_Speed;
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }
        if (!m_IsAbleToMove)
            m_Move = 0.0f;

       
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (hit = Physics2D.Raycast(point, Vector2.zero, m_PickUpDistance))
            {
                if (m_PickUpDistance >= Vector2.Distance(point, transform.position))
                {
                    //TakeItem(hit.transform.gameObject);
                }
            }
        }

        if(m_Rigidbody.velocity.magnitude > m_MagnitudeToZoomCamera)
        {
            m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, m_CameraZoomOriginal + m_CameraZoom, Time.deltaTime * m_CameraZoomSpeed);
        }
        else
        {
            m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, m_CameraZoomOriginal, Time.deltaTime * m_CameraZoomSpeed);
        }
        
    }

    private void FixedUpdate()
    {
        m_Controller.Move(m_Move * Time.fixedDeltaTime, false, m_Jump);
        m_Jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.gameObject.gameObject;
        if (collider.tag == "DialogueTrigger")
        {
            m_IsAbleToMove = false;
            //collider.GetComponent<DialogueTrigger>().TriggerDialog();
        }
    }



}