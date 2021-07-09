using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 10.0f;
    [SerializeField]
    private bool MobileInput = false;
    [SerializeField]
    private Inventory m_Inventory;
    [SerializeField]
    private float m_PickUpDistance = 1.0f;
    public bool m_IsAbleToMove = true;

    private bool m_Jump = false;
    private CharacterController2D m_Controller;
    private float m_Move = 0.0f;

    private void Awake()
    {
        m_Controller = GetComponent<CharacterController2D>();
        Physics2D.IgnoreLayerCollision(7, 6);
    }

    public void Jump()
    {
        m_Jump = true;
    }

    private void Update()
    {
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

       
        if(Input.GetMouseButton(0))
        {
            RaycastHit2D hit;
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (hit = Physics2D.Raycast(point, Vector2.zero, m_PickUpDistance))
            {
                if(m_PickUpDistance >= Vector2.Distance(point, transform.position))
                    TakeItem(hit.collider.gameObject);
            }
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
            print("collided");
            m_IsAbleToMove = false;
            collider.GetComponent<DialogueTrigger>().TriggerDialog();
        }
    }


    public void TakeItem(GameObject obj)
    {
        if (obj.layer == 6)
        {
            Destroy(obj.gameObject);
        }
    }

    public void UseItem()
    {

    }


}