using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector3 velocity;
    CharacterController characterController;

    public Transform groundCheck;
    public LayerMask groundMask;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, 
            transform.TransformDirection(Vector3.down),
            out hit, 0.3f, groundMask))
        {
            switch(hit.collider.gameObject.tag)
            {
                case "FAST":
                    speed = 20f;
                    break;
                case "SLOW":
                    speed = 5f;
                    break;
                default:
                    speed = 10f;
                    break;
            }
        }

        characterController.Move(move * speed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "PICKUP")
        {
            hit.gameObject.GetComponent<PickUp>().Picked();
        }
    }
}
