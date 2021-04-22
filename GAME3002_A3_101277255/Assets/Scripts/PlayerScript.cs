using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpStrength;
    public float moveSpeedRate;
    public int LevelofSecurity;
    public float maxVelocityMag;
    public int PlayerInitialLiveCount;
    public float deathDuration;

    Animator player_Anim;
    Rigidbody player_RB;

    //Regardless of player's orientation
    Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 Left = new Vector3(-1.0f, 0.0f, 0.0f);
    Vector3 Right = new Vector3(1.0f, 0.0f, 0.0f);

    bool isMoveRight = true;
    bool doneMoving = true;
    bool isGrounded = true;
    bool isDead = false;

    float tempDisableTimer;

    Vector3 player_reSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        player_Anim = GetComponent<Animator>();
        player_RB = GetComponent<Rigidbody>();
        setRespawnPos(player_RB.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            tempDisableTimer = deathDuration;
            isDead = false;
        }
        else
        {
            if (tempDisableTimer > 0.0f)
            {
                tempDisableTimer -= Time.deltaTime;
            }
            else
            {
                CheckPlayerInput();
                checkOrientation();
                checkIfGroundedAnim();
            }
        }
    }

    public void setRespawnPos(Vector3 vec)
    {
        player_reSpawnPos = vec;
    }

    public void die()
    {
        if (PlayerInitialLiveCount > 0)
        {
            PlayerInitialLiveCount -= 1;
            player_RB.position = player_reSpawnPos;
            isDead = true;

            print(PlayerInitialLiveCount);
        }
        else
        {
            //you lose
        }
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded) //can't jump when in air
            {
                player_RB.AddForce(Up * jumpStrength, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (player_RB.velocity.magnitude < maxVelocityMag)
            {
                player_RB.AddForce(Left * moveSpeedRate, ForceMode.Impulse);
            }

            if (isMoveRight)
            {
                isMoveRight = false;
                doneMoving = false;
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (player_RB.velocity.magnitude < maxVelocityMag)
            {
                player_RB.AddForce(Right * moveSpeedRate, ForceMode.Impulse);
            }

            if (!isMoveRight)
            {
                isMoveRight = true;
                doneMoving = false;
            }
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
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        //when player isn't in contact with anything, aka in air
        isGrounded = false;
    }

    private void checkIfGroundedAnim()
    {
        if (isGrounded)
        {
            player_Anim.SetBool("isJumping", false);
            player_Anim.SetBool("isMoving", Mathf.Abs(player_RB.velocity.x) > 0.01f);
        }
        else
        {
            player_Anim.SetBool("isJumping", true);
            player_Anim.SetBool("isMoving", false);
        }
    }
}
