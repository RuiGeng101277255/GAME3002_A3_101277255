using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float jumpStrength;
    public float moveSpeedRate;
    public int LevelofSecurity;
    public float maxVelocityMag;
    public int PlayerInitialLiveCount;
    public float deathDuration;
    public ScreenMessageScript screenM;
    public TimerScript timer;

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
    public bool gameOver = false;

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
        //Check game state (if it's over or still playing)
        if (!gameOver)
        {
            //A brief delay after player dies, before giving back the controls
            if (isDead)
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
        else
        {
            //If game is over, the player can restart the game by reloading the scene
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
        }
    }

    //Setting respawn point after collecting kunai
    public void setRespawnPos(Vector3 vec)
    {
        player_reSpawnPos = vec;
    }

    //When player dies, it takes away a live. If the player's live remaining becomes 0, he/she loses, otherwise the player respawns.
    public void die()
    {
        if (PlayerInitialLiveCount > 1)
        {
            //Sends player to respawn point if they are still alive.
            PlayerInitialLiveCount -= 1;
            player_RB.position = player_reSpawnPos;
            player_RB.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            isDead = true;
        }
        else
        {
            //Death
            screenM.setTextDisplayed("YOU LOST!\nPress R to Restart", true, false);
            PlayerInitialLiveCount = 0;
            timer.isPaused = true;
            gameOver = true;
        }
    }

    //Called by the goal gameobject at the very end.
    public void PlayerWins()
    {
        screenM.setTextDisplayed("YOU WIN!\nPress R to Restart", true, true);
        timer.isPaused = true;
        gameOver = true;
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded)
            {
                player_RB.AddForce(Up * jumpStrength, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        //Limits the velocity magnitude at a certain point

        if (Input.GetKey(KeyCode.A))
        {
            if (Mathf.Abs(player_RB.velocity.x) < maxVelocityMag)
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
            if (Mathf.Abs(player_RB.velocity.x) < maxVelocityMag)
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

    //For animation purposes
    private void checkIfGroundedAnim()
    {
        if (player_RB.velocity.magnitude < 0.1f)
        {
            isGrounded = true;
        }

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
