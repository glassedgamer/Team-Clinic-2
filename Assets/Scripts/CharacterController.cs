using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public Animator playerAnims;

    // The three positios that the player can switch between
    public Vector3 leftPos;
    public Vector3 centerPos;
    public Vector3 rightPos;

    /*
     * currentSpot = 1 --> leftPos
     * currentSpot = 2 --> centerPos
     * currentSpot = 3 --> rightPos
     */
    public int currentSpot = 2;

    public float forwardSpeed;
    public float sideSpeed;

    private void Start()
    {
        currentSpot = 2;
    }

    private void Update()
    {
        SwitchPosition();
    }

    void SwitchPosition()
    {
        // Determines which spot number you're on depending on which button you press
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSpot--;
            playerAnims.SetTrigger("Left");
        } else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSpot++;
            playerAnims.SetTrigger("Right");
        }

        // Limits current spot so it can't go past 1 or 3
        if (currentSpot < 1)
            currentSpot = 1;
        else if (currentSpot > 3)
            currentSpot = 3;

        // Takes spot number after button press and assigns a new position to the player
        if (currentSpot == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPos, sideSpeed * Time.deltaTime);
        } else if (currentSpot == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, centerPos, sideSpeed * Time.deltaTime);
        } else if (currentSpot == 3)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPos, sideSpeed * Time.deltaTime);
        }
    }
}
