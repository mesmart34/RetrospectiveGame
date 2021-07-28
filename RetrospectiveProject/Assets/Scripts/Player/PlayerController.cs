using UnityEngine;
using TMPro;

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

    [SerializeField]
    public Animator m_Animator;

    [Header("Camera Shaking")]
    private bool CameraShake = false;
    private Camera m_Camera;
    [SerializeField]
    private float m_MagnitudeToZoomCamera = 1.0f;
    [SerializeField]
    private float m_CameraZoomSpeed = 1.0f;
    [SerializeField]
    private float m_CameraZoom = 2.0f;
    private float m_CameraZoomOriginal;

    public LevelOneList objectList;

    [SerializeField]
    private GameObject StaircaseUseObject;
    public GameObject Logic;

    private bool m_Jump = false;
    private CharacterController2D m_Controller;
    private float m_Move = 0.0f;
    private bool m_MobileMoveButton;
    private float m_MobileMoveDir;
    private Rigidbody2D m_Rigidbody;
    private bool m_AbleToUseStaircase;
    private GameObject StaircaseObject;
    private bool m_Running = false;

    [SerializeField]
    private GameEvent DiaryOpen;
    [SerializeField]
    private GameObject ThougthText;

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

    public void SetAbleToMove(bool arg)
    {
        m_IsAbleToMove = arg;
    }

    public void ShowThought(string sentence)
    {
        ThougthText.GetComponent<TextTyper>().Sentence = sentence;
        ThougthText.SetActive(true);
        ThougthText.GetComponent<TextTyper>().enabled = true;
        m_IsAbleToMove = false;
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


        if(Input.GetButtonDown("Jump") && m_AbleToUseStaircase)
        {
            StaircaseObject.GetComponent<Staircase>().Use(gameObject);
        }

        if(Input.GetMouseButtonDown(0) && !m_Running && m_IsAbleToMove)
        {
            m_Animator.SetTrigger("Use");
            RaycastHit2D hit;
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (hit = Physics2D.Raycast(point, Vector2.zero, m_PickUpDistance))
            {
                if (m_PickUpDistance >= Vector2.Distance(point, transform.position))
                {
                    if (hit.collider.gameObject.tag == "Item")
                    {
                        var item = hit.collider.gameObject.GetComponent<ItemHandler>().item;
                        objectList.SetObject(item);
                        m_Inventory.AddItem(item);

                        Destroy(hit.collider.gameObject);
                    }else if(hit.collider.gameObject.name == "Дневник")
                    {
                        DiaryOpen.Raise();
                    }else if(hit.collider.gameObject.tag == "Discoverable")
                    {
                        ThougthText.GetComponent<TextTyper>().Sentence = hit.collider.gameObject.GetComponent<ThougthHandler>().Sentence;
                        ThougthText.SetActive(true);
                        ThougthText.GetComponent<TextTyper>().enabled = true;
                        m_IsAbleToMove = false;
                        ThougthText.GetComponent<TextTyper>().CanInvokeEnd = false;
                    }
                    else if (hit.collider.gameObject.tag == "Watch")
                    {
                        ThougthText.GetComponent<TextTyper>().Sentence = hit.collider.gameObject.GetComponent<ThougthHandler>().Sentence;
                        ThougthText.SetActive(true);
                        ThougthText.GetComponent<TextTyper>().enabled = true;
                        m_IsAbleToMove = false;
                        ThougthText.GetComponent<TextTyper>().CanInvokeEnd = true;
                    }
                }
            }
        }

        if (m_Rigidbody.velocity.magnitude > m_MagnitudeToZoomCamera)
        {
            if (CameraShake)
                m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, m_CameraZoomOriginal + m_CameraZoom, Time.deltaTime * m_CameraZoomSpeed);
            m_Animator.SetBool("Run", true);
            m_Running = true;
        }
        else
        {
            if (CameraShake)
                m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, m_CameraZoomOriginal, Time.deltaTime * m_CameraZoomSpeed);
            m_Animator.SetBool("Run", false);
            m_Running = false;
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
        if(collider.tag == "StaircaseTrigger")
        {
            m_AbleToUseStaircase = true;
            StaircaseObject = collider.gameObject;
            StaircaseUseObject.SetActive(true);
            StaircaseUseObject.GetComponent<TextMeshProUGUI>().text = collider.GetComponent<Staircase>().Label;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "StaircaseTrigger")
        {
            m_AbleToUseStaircase = false;
            StaircaseObject = null;
            StaircaseUseObject.SetActive(false);
        }
    }
}