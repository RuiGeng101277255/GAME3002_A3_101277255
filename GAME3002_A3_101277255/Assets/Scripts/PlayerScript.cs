using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpStrength = 10.0f;
    public float moveSpeedRate = 0.5f;
    public int LevelofSecurity = 0;

    Animator player_Anim;
    Rigidbody player_RB;

    //Regardless of player's orientation
    Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 Left = new Vector3(-1.0f, 0.0f, 0.0f);
    Vector3 Right = new Vector3(1.0f, 0.0f, 0.0f);

    bool isMoveRight = true;
    bool doneMoving = true;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        player_Anim = GetComponent<Animator>();
        player_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        checkOrientation();
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            player_RB.AddForce(Up * jumpStrength, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            player_RB.AddForce(Left * moveSpeedRate, ForceMode.Impulse);
            if (isMoveRight)
            {
                isMoveRight = false;
                doneMoving = false;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player_RB.AddForce(Right * moveSpeedRate, ForceMode.Impulse);
            if (!isMoveRight)
            {
                isMoveRight = true;
                doneMoving = false;
            }
        }

        if (!isGrounded)
        {
            player_Anim.SetBool("isJumping", true);
            player_Anim.SetBool("isMoving", false);
        }
        else
        {
            player_Anim.SetBool("isJumping", false);
            player_Anim.SetBool("isMoving", player_RB.velocity.magnitude > 0.01f);
        }
    }

    private void checkOrientation()
    {
        //Transform used only for the character to look left/right, not for physics
        if (!doneMoving)
        {
            if (isMoveRight)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Abs(transform.localScale.z));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -Mathf.Abs(transform.localScale.z));
            }
            doneMoving = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Abs(player_RB.velocity.y) < 0.001f)
        {
            isGrounded = true;
            player_Anim.SetBool("isJumping", false);
            player_Anim.SetBool("isMoving", player_RB.velocity.magnitude > 0.01f);
        }
    }
}
