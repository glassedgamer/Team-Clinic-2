using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //References to player stuff
    [Header("Player References")]
    public Animator playerAnims;
    public Rigidbody rb;
    public Transform jumpPoint;

    // The three positios that the player can switch between
    [Header("Player Positions")]
    public float positionValue = 4.0f;
    public Vector3 leftPos;
    public Vector3 centerPos;
    public Vector3 rightPos;
    Vector3 targetPos;


    /*
     * currentSpot = 1 --> leftPos
     * currentSpot = 2 --> centerPos
     * currentSpot = 3 --> rightPos
     */
    public int currentSpot = 2;

    //Various Speeds && Forces
    [Header("Speeds & Forces")]
    public float forwardSpeed;
    public float sideSpeed;
    public float jumpForce;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    float groundDistance = 0.4f;
    bool isGrounded = true;

    private void Start()
    {
        currentSpot = 2;
        targetPos = centerPos;
    }

    [System.Obsolete]
    private void Update()
    {
        //Checks if the player is grounded with a sphere cast
        isGrounded = Physics.CheckSphere(jumpPoint.position, groundDistance, groundMask);

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        //If W pressed, move player forward and play running animation. Else, stop everything
        if(Input.GetKey(KeyCode.W))
        {
            playerAnims.SetBool("Running", true);
            rb.velocity = new Vector3(0, rb.velocity.y, forwardSpeed);
        } else
        {
            playerAnims.SetBool("Running", false);
        }

        //Guarding is turned on and off. Also stops any movement if you're moving
        if (Input.GetKey(KeyCode.E))
        {
            playerAnims.SetBool("Guarding", true);
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            playerAnims.SetBool("Guarding", false);
        }

        SwitchPosition();
        Jumping();

    }

    private void FixedUpdate()
    {
        //Jumping();
        rb.linearDamping = 0;
    }

    void SwitchPosition()
    {
        // Determines which spot number you're on depending on which button you press
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSpot--;
            playerAnims.SetTrigger("Left"); //Triggers the Left animation
        } else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSpot++;
            playerAnims.SetTrigger("Right"); //Triggers the Right animation
        }

        // Limits current spot so it can't go past 1 or 3
        if (currentSpot < 1)
            currentSpot = 1;
        else if (currentSpot > 3)
            currentSpot = 3;

        if(currentSpot == 2)
        {
            rb.MovePosition(new Vector3(0, transform.position.y, transform.position.z));
        } else if (currentSpot == 1)
        {
            rb.MovePosition(new Vector3(-positionValue, transform.position.y, transform.position.z));
        } else if (currentSpot == 3)
        {
            rb.MovePosition(new Vector3(positionValue, transform.position.y, transform.position.z));
        }
    }

    void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnims.SetTrigger("Jumping");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(jumpPoint.position, 0.5f);
    }

}
