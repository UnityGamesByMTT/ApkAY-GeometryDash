using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator SquareAnim;
    

    [Space]
    public float jumpForce = 10f;
    public float moveSpeed = 5f;

    private float jumpStartY;
    private float jumpPeakY;

    [SerializeField] private float rotationForce;
    [SerializeField] private bool isGrounded;
    private Rigidbody2D rb;
    private bool isMoving = true;
    private bool isUpdideDown = false;
    private bool GravityInverse = false;
    [SerializeField]private bool isRocket = false;

    [SerializeField] private BoxCollider2D MainCollider;
    [SerializeField] private GameObject DestroyedPlayer;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private GameObject[] DestroyedParticles;
    [SerializeField] private GameObject DustParticals;

    private float randomForceStrength = 3f;

    [Header("Rocket")]
    [SerializeField] private GameObject Rocket;

    [Header("AudioS")]
    [SerializeField] AudioManager audioController;

    [Header("NoobMode")]
    public bool noobMode = false;
    [SerializeField] private GameObject LastSafetransform;
    private int jumpcount = 0;
    private Vector3 lastSafepos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpPeakY = jumpForce / (rb.gravityScale * Physics2D.gravity.magnitude / rb.mass);
    }

    void Update()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            bool isScreenHeld = Input.GetKey(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended);

            if (isScreenHeld && isGrounded && !isRocket)
            {
                JumpAction();
            }
            else if(isScreenHeld && isRocket)
            {
                FlyAction();
            }
        }
    }

    void JumpAction()
    {
        isGrounded = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if(audioController)audioController.PlaySound("Jump");
        if (!GravityInverse)
        {
            if (isUpdideDown)
            {
                isUpdideDown = false;
                SquareAnim.Play("Clip2");
            }
            else
            {
                isUpdideDown = true;
                SquareAnim.Play("Clip1");

            }
        }
        else
        {
            if (isUpdideDown)
            {
                isUpdideDown = false;
                SquareAnim.Play("Clop3");
            }
            else
            {
                isUpdideDown = true;
                SquareAnim.Play("Clip4");

            }
        }
        jumpStartY = transform.position.y;
    }
    void FlyAction()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.2f);
    }
    private void NoobMode(int count)
    {
        if (noobMode)
        {
            if (count % 4 == 0)
            {
                lastSafepos = gameObject.transform.position;

            }
            if (count % 5 == 0)
            {
                LastSafetransform.SetActive(true);
                LastSafetransform.transform.position = lastSafepos;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            jumpcount++;
            NoobMode(jumpcount);
            isGrounded = true;
            DustParticals.SetActive(true);
            // StartCoroutine(SmoothAlignToNearestEdge());
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if(audioController)audioController.PlaySound("Noob");
            isMoving = false;
            MainCollider.enabled = false;
            DestroyedPlayer.SetActive(true);
            PlayerSprite.SetActive(false);
            cameraController.StopCamera(true);
            cameraController.StartCameraShake();
            if (Rocket) Rocket.SetActive(false);

            foreach (var ob in DestroyedParticles)
            {
                Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
                Vector2 randomForce = new Vector2(Random.Range(-randomForceStrength, randomForceStrength), Random.Range(-randomForceStrength, randomForceStrength));
                rb.AddForce(randomForce, ForceMode2D.Impulse);
                StartCoroutine(Destroyplayer(ob.gameObject, Random.Range(0.5f, 2f)));
            }
            if (!noobMode)
            {
                StartCoroutine(gameManager.GameOver());
            }
            else
            {
                gameObject.transform.position = LastSafetransform.transform.position;
                StartCoroutine(RestartGame());
            }
        }
        
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.5f);
        isMoving = true;
        MainCollider.enabled = true;
        DestroyedPlayer.SetActive(false);
        PlayerSprite.SetActive(true);
        cameraController.StopCamera(false);
       
       // if (Rocket) Rocket.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isGrounded = true;
            DustParticals.SetActive(false);
            // StartCoroutine(SmoothAlignToNearestEdge());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gravity"))
        {
            if (!GravityInverse)
            {
                GravityInverse = true;
                rb.gravityScale *= -1;
                jumpForce *= -1;
            }
            else
            {
                GravityInverse = false;
                rb.gravityScale *= -1;
                jumpForce *= -1;
            }
            if(isRocket)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,transform.localScale.y*-1,transform.localScale.z);
            }
        }
        if(collision.gameObject.CompareTag("End"))
        {
            gameManager.LevelComplete();
            if(audioController)audioController.PlaySound("LecelComplete");
        }
        if(collision.gameObject.CompareTag("FakeJump"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Back"))
        {
            moveSpeed *= -1;
            cameraController.offsetX *= -1;
            DustParticals.transform.localScale = new Vector3(DustParticals.transform.localScale.x * -1, DustParticals.transform.localScale.y, DustParticals.transform.localScale.z);
        }
        if (collision.gameObject.CompareTag("JumpGain"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Jump"))
        {
            
                isGrounded = false;
                jumpForce *= 1.5f;
                JumpAction();
                jumpForce /= 1.5f;
            
        }
        if (collision.gameObject.CompareTag("NegativeJump"))
        {
            
            
                isGrounded = false;
                jumpForce *= -1.5f;
                JumpAction();
                jumpForce /= -1.5f;
            
        }
        if (collision.gameObject.CompareTag("Rocket"))
        {
            if(isRocket)
            {
                isRocket = false;
                Rocket.SetActive(false);
                DustParticals.SetActive(true);
                PlayerSprite.SetActive(true);
                rb.gravityScale /= 0.3f;
            }
            else
            {
                isRocket = true;
                Rocket.SetActive(true);
                DustParticals.SetActive(false);
                PlayerSprite.SetActive(false);
                rb.gravityScale *= 0.3f;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("JumpGain"))
        {
            isGrounded = false;
        }
    }
    IEnumerator Destroyplayer(GameObject obj, float timex = 0.1f)
    {
        yield return new WaitForSeconds(timex);
        obj.SetActive(false);
    }



}


