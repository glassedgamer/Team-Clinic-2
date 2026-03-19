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

    private void Update()
    {
        isGrounded = Physics.CheckSphere(jumpPoint.position, groundDistance, groundMask);

        SwitchPosition();
        Jumping();
    }

    private void FixedUpdate()
    {
        Jumping();
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

        // Takes spot number after button press and assigns a new position to the player
        if (currentSpot == 1) 
            targetPos = leftPos;
        else if (currentSpot == 2) 
            targetPos = centerPos;
        else if (currentSpot == 3) 
            targetPos = rightPos;

        //Have to use rb values and functions for movement so jump isn't jittery
        Vector3 newPos = Vector3.MoveTowards(rb.position, targetPos, sideSpeed * Time.deltaTime);
        rb.MovePosition(newPos);
    }

    void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
