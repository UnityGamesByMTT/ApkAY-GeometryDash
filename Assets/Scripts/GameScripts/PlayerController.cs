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

    [SerializeField] private BoxCollider2D MainCollider;
    [SerializeField] private GameObject DestroyedPlayer;
    [SerializeField] private GameObject PlayerSprite;
    [SerializeField] private GameObject[] DestroyedParticles;
    private float randomForceStrength = 3f;

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

            if (isScreenHeld && isGrounded)
            {
                isGrounded = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // StartCoroutine(SmoothAlignToNearestEdge());
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isMoving = false;
            MainCollider.enabled = false;
            DestroyedPlayer.SetActive(true);
            PlayerSprite.SetActive(false);
            cameraController.StopCamera();
            cameraController.StartCameraShake();

            foreach (var ob in DestroyedParticles)
            {
                Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
                Vector2 randomForce = new Vector2(Random.Range(-randomForceStrength, randomForceStrength), Random.Range(-randomForceStrength, randomForceStrength));
                rb.AddForce(randomForce, ForceMode2D.Impulse);
                StartCoroutine(Destroyplayer(ob.gameObject, Random.Range(0.5f, 2f)));
            }
            StartCoroutine(gameManager.GameOver());
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
        }
    }

    IEnumerator Destroyplayer(GameObject obj, float timex = 0.1f)
    {
        yield return new WaitForSeconds(timex);
        obj.SetActive(false);
    }



}


