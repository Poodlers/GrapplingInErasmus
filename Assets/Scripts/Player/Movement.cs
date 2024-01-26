using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerInput playerInput;

    private Rigidbody2D rb;

    private Animator animator;

    private float distanceToGround;

    private float jumpBufferCounter = 0f;

    private bool canQuickStep = true;
    private float coyoteTime = 0.1f;

    private float coyoteTimeCounter = 0f;

    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float maxSpeed = 5;

    [SerializeField]
    private float dashingPower = 10f;

    [SerializeField]
    private float dashTime = 0.1f;

    public float dashCooldown = 0.5f;

    [SerializeField]
    private float acceleration = 0.2f;

    [SerializeField]
    private float deceleration = 0.1f;

    [SerializeField]
    private float airAcceleration = 0.1f;

    [SerializeField]
    private float maxAirSpeed = 5f;

    [SerializeField]
    private float jumpBufferTime = 0.2f;
    [SerializeField]
    private float baseMovementSpeed = 5f;


    [SerializeField]
    private float anchorRange = 3f;

    public LayerMask groundLayer;

    private SpriteRenderer spriteRenderer;

    private GameObject[] anchors;

    private GameObject currentAnchor;

    private GameObject possibleAnchor;

    public GameObject optionsMenu;
    public Image canGrappleUI;

    public Sprite canGrappleSprite;
    public Sprite cannotGrappleSprite;

    public GameObject optionsMenuInstructor;

    public GameObject HPBar;

    private bool isAnimLocked = false;

    private Vector2 moveInput;

    public Slider dashSlider;
    private PlayerHealth playerHealth;
    void Start()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        playerInput.Enable();
        anchors = GameObject.FindGameObjectsWithTag("Anchor");
    }


    // Update is called once per frame
    void Update()
    {
        if (playerHealth.isDead) return;

        if (Input.GetKeyDown(KeyCode.Q) && !optionsMenu.activeSelf)
        {
            optionsMenuInstructor.SetActive(false);
            canGrappleUI.gameObject.SetActive(false);
            HPBar.SetActive(false);
            optionsMenu.SetActive(true);
            //pause game
            Time.timeScale = 0f;
        }

        possibleAnchor = null;

        foreach (GameObject anchor in anchors)
        {
            if (Vector2.Distance(transform.position, anchor.transform.position) < anchorRange
             && anchor.transform.position.y > transform.position.y)
            {
                if (possibleAnchor != null && anchor.transform.position.y < possibleAnchor.transform.position.y) continue;
                possibleAnchor = anchor;

            }

        }
        if (possibleAnchor != null && !isGrounded())
        {
            canGrappleUI.sprite = canGrappleSprite;
        }
        else
        {
            canGrappleUI.sprite = cannotGrappleSprite;
        }


        moveInput = playerInput.Player.Move.ReadValue<Vector2>();

        if (Input.GetKeyDown(KeyCode.Space))
        {

            jumpBufferCounter = jumpBufferTime;
        }
        else if (jumpBufferCounter > 0f)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        //if (Input.GetKeyDown(KeyCode.F) && !isGrounded())

        if (Input.GetMouseButtonDown(0) && !isGrounded())
        {

            if (possibleAnchor != null)
            {
                // if in range, connect to anchor
                currentAnchor = possibleAnchor;
                currentAnchor.GetComponent<Grapple>().OnThrowHook();
            }

        }
        //if (Input.GetKeyUp(KeyCode.F))
        if (Input.GetMouseButtonUp(0))
        {
            // disconnect from anchor
            if (currentAnchor != null)
            {
                currentAnchor.GetComponent<Grapple>().onUnThrowHook();
                currentAnchor = null;
            }
        }

        //if player is at jump peak, set y velocity to 0
        if (!isGrounded() && rb.velocity.y < 0.1f && rb.velocity.y > -0.1f)
        {

            animator.SetBool("jump", false);
            animator.SetBool("maxHeightReached", true);
        }
        else if (rb.velocity.y < -0.3f)
        {
            animator.SetBool("maxHeightReached", false);
            animator.SetBool("fallingFromJump", true);
        }

    }

    private IEnumerator QuickStep()
    {

        dashSlider.value = 0f;
        isAnimLocked = true;
        canQuickStep = false;
        Vector2 wishSpeed = (moveInput.x * transform.right).normalized;
        rb.velocity = wishSpeed * dashingPower;
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector3.zero;
        isAnimLocked = false;
        yield return new WaitForSeconds(dashCooldown);
        canQuickStep = true;
    }
    void FixedUpdate()
    {
        if (isAnimLocked || playerHealth.isDead) return;

        if (isGrounded())
        {
            animator.SetBool("fallingFromJump", false);
            coyoteTimeCounter = coyoteTime;

            if (canQuickStep && playerInput.Player.QuickStep.IsPressed())
            {
                StartCoroutine(QuickStep());
                coyoteTimeCounter = 0f;
                return;
            }

            GroundMove();
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;

            if (moveInput != Vector2.zero)
            {
                AirAccel();
            }
        }


        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            coyoteTimeCounter = 0f;
            //reset y velocity
            animator.SetBool("fallingFromJump", false);
            animator.SetBool("jump", true);
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, 0.0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpBufferCounter = 0f;
        }


    }

    private void GroundMove()
    {
        float dynamicSpeedFactor = 1;

        if (moveInput != Vector2.zero)
        {
            //move
            Vector2 wishDir = moveInput.normalized;
            dynamicSpeedFactor = (Vector2.Dot(wishDir, transform.forward) + 1.0f) / 2.0f;
            rb.velocity += wishDir * acceleration * (1 + dynamicSpeedFactor);
            animator.SetBool("isMoving", true);
            spriteRenderer.flipX = wishDir.x < 0;

        }
        else if (rb.velocity != Vector2.zero)
        {
            //decelerate
            animator.SetBool("isMoving", true);
            rb.velocity -= rb.velocity * deceleration;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void AirAccel()
    {
        Vector2 wishDir = (moveInput.x * transform.right).normalized;
        float currentSpeed = Vector2.Dot(rb.velocity, wishDir);
        spriteRenderer.flipX = wishDir.x < 0;
        float addSpeed = baseMovementSpeed - currentSpeed;

        if (addSpeed <= 0.0f)
            return;

        rb.velocity += addSpeed * airAcceleration * wishDir;

        rb.velocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0.0f, 0.0f), maxAirSpeed) + rb.velocity.y * Vector3.up;


    }
    bool isGrounded()
    {
        //return Physics.BoxCast(transform.position, boxsize, -transform.up, transform.rotation, distanceToGround + 0.1f);
        return Physics2D.Raycast(transform.position, Vector2.down, distanceToGround + 0.1f, groundLayer);
    }
}
