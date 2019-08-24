using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform throw_point_player;
    public float jumpForce, speed;
    public float time_to_fall = 1f;
    public string hori;
    public KeyCode jump;
    public KeyCode bombKey;
    public GameObject bomb;

    Vector3 playerAreaXAxis;
    Rigidbody2D playerRigidBody;
    Animator animator;
    SpriteRenderer sprite;

    float horizontal, jumpDelay;
    bool isJumping;
    bool canThrow = true;
    float fall = -3f;
    bool isGrounded = false;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        isJumping = false;
        jumpDelay = 1;
    }
    void Update()
    {
        jumpDelay -= Time.deltaTime;
        horizontal = Input.GetAxis(hori);
        transform.position += new Vector3(horizontal, 0, 0) * speed;
        if (Input.GetKeyDown(jump) && !isJumping)
        {
            FindObjectOfType<AudioManager>().Play("Jump");
            StartCoroutine(jumpAnimation());
        }
        if(Input.GetKeyDown(bombKey) && canThrow)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            canThrow = false;
            Invoke("ToggleBombDuration", 3f);
        }
        if (playerRigidBody.velocity.y < 0)
        {
            playerRigidBody.velocity += Vector2.down * Physics2D.gravity * (fall) * Time.deltaTime;
        }
        if (!isJumping)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontal));
        }
        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        if (playerRigidBody.velocity.y < -4 && !isJumping)
        {
            StartCoroutine(fallAnimation());
        }

    }
    void Jump()
    {
        isJumping = true;
        CharacterControls.dashCharge = 1;
        float x_distance;
        x_distance = (throw_point_player.position.x - 2) + (Mathf.Sign(horizontal)) * throw_point_player.position.x; // (+/- 2) changed according to the player destination

        float y_distance;
        y_distance = (throw_point_player.position.y + 2) - throw_point_player.position.y;

        float thrown_angle;
        thrown_angle = Mathf.Atan((y_distance + 4.905f * (time_to_fall * time_to_fall)) / x_distance);

        float intial_velocity = x_distance / (Mathf.Cos(thrown_angle) * time_to_fall);

        float x_velocity, y_velocity;
        x_velocity = intial_velocity * Mathf.Cos(thrown_angle);
        y_velocity = intial_velocity * Mathf.Sin(thrown_angle);

        playerRigidBody.velocity = new Vector2(x_velocity, y_velocity);
    }
    void RestrictPlayerArea()
    {
        playerAreaXAxis = new Vector3(0, 0, 0);
        playerAreaXAxis.x = Mathf.Clamp(transform.position.x, -8.65f, 8.5f);
        playerAreaXAxis.y = Mathf.Clamp(transform.position.y, -8.65f, 8.5f);
        transform.position = playerAreaXAxis;
    }
    void OnCollisionStay2D(Collision2D collisionInfo)
    {

        if (collisionInfo.gameObject.tag == "Floor" && jumpDelay <= 0)
        {
            isJumping = false;
            jumpDelay = 1;
        }
        if(collisionInfo.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
        {
            isGrounded = false;
        }
    }
    void ToggleBombDuration()
    {
        canThrow = !canThrow;
    }
    IEnumerator jumpAnimation()
    {
        animator.SetTrigger("jump");
        yield return new WaitForSeconds(0.23f);
        Jump();
        yield return new WaitUntil(() => playerRigidBody.velocity.y < 0);
        animator.ResetTrigger("jump");
        animator.SetBool("fall", true);
        yield return new WaitUntil(() => isJumping == false);
        animator.SetBool("fall", false);
        animator.SetTrigger("land");
        yield return new WaitForSeconds(0.22f);
        animator.ResetTrigger("land");
    }
    IEnumerator fallAnimation()
    {
        animator.SetBool("fall", true);
        animator.SetBool("IsJumping", false);
        yield return new WaitUntil(() => isGrounded == true);
        animator.SetBool("fall", false);
        animator.SetBool("IsJumping", true);
        animator.SetTrigger("land");
        yield return new WaitForSeconds(0.22f);
        animator.ResetTrigger("land");
    }
   
}
