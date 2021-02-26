using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D Rb2D = default;
    [SerializeField]
    private Animator anim = default;
    [SerializeField]
    private float Speed = default;
    [SerializeField]
    private float JumpForce = default;

    [SerializeField]
    private BoxCollider2D floorChecker = default;
    [SerializeField]
    private LayerMask floorLayer = default;


    private float greaterGravity = 6, lessGravity = 5; //Less 2
    private Vector2 axisMove = default;
    public bool isRight = default;
    private bool inFloor = default;
    private float jumpTimeCounter = default;
    public float JumpTime = default;
    private bool canJump = default;
    //private float inFloorRadius = 0.05f;
    private bool isJumping = default;
    private bool flipX = default;
    private bool isRunning = default;
    private bool canStun = default;
    private bool isStunning = default;
    public float stunDuration = 3f;

    public static Player instance;

    private bool startMove = default;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        axisMove = Vector2.zero;

        if (isStunning)
        {
            return;
        }

        GetJump();

        GetMove();

        SetAnimator();
    }

    private void FixedUpdate()
    {

        if (axisMove.y > 0f)
        {
            Rb2D.velocity = axisMove * Time.deltaTime;
        } 
        else
        {
            Rb2D.velocity = new Vector2(axisMove.x * Time.deltaTime, Rb2D.velocity.y);
        }

        Move();

        Jump();

        Flip();

    }

    private void GetJump()
    {
        if (inFloor && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)))
        {
            canJump = true;
            jumpTimeCounter = JumpTime;
            //Rb2D.velocity = Vector2.up * JumpForce * Time.deltaTime;
            //SfxManager.Instance.PlaySfxPlayerLand();
            axisMove.y = 1f;
            axisMove.y *= JumpForce; //* Time.deltaTime;
            if (!startMove)
            {
                MainCamera.instance.fade.FadeOut();
                startMove = true;
            }
        }

        if (canJump && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)))
        {
            if (jumpTimeCounter > 0)
            {
                //Rb2D.velocity = Vector2.up * JumpForce * Time.deltaTime;
                jumpTimeCounter -= Time.deltaTime;
                axisMove.y = 1f;
                axisMove.y *= JumpForce; //* Time.deltaTime;
            }
            else
            {
                canJump = false;
                axisMove.y = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space))
        {
            canJump = false;
            axisMove.y = 0f;
        }
    }

    private void GetMove()
    {
        axisMove.x = Input.GetAxis("Horizontal");
        axisMove.x *= Speed; // * Time.deltaTime;
        if (axisMove.x > 0)
        {
            isRight = true;
        } 
        else if (axisMove.x < 0)
        {
            isRight = false;
        }
    }

    private void SetAnimator()
    {
        anim.SetBool("InFloor", inFloor);
        anim.SetBool("IsRunning", isRunning);
        anim.SetBool("IsJumping", isJumping);
        anim.SetBool("IsStunning", isStunning);
    }

    private void Move()
    {
        // Rb2D.velocity = new Vector2(axisMove.x * Speed * Time.deltaTime, Rb2D.velocity.y);

        if (axisMove.x == 0)
        {
            isRunning = false;
        }
        else
        {
            if (!startMove)
            {
                MainCamera.instance.fade.FadeOut();
                startMove = true;
            }

            isRunning = true;
        }
    }

    private void Jump()
    {
        inFloor = Physics2D.BoxCast(floorChecker.transform.position, floorChecker.size, 0f, Vector2.down, 0.05f, floorLayer, 0f, 0f);

        if (Rb2D.velocity.y < 0)
        {
            Rb2D.gravityScale = lessGravity;
            isJumping = false;
            if (Rb2D.velocity.y < -22f)
            {
                canStun = true;
            }
        }
        else if (Rb2D.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rb2D.gravityScale = greaterGravity;
            isJumping = true;
        }
        else
        {
            Rb2D.gravityScale = 1;
            isJumping = false;
        }

        if (canStun && inFloor)
        {
            StartCoroutine(Stunning());
        }
    }

    private IEnumerator Stunning()
    {
        isStunning = true;
        canStun = false;
        SetAnimator();
        MainCamera.instance.animCamera.Play("ShakeStun");
        yield return new WaitForSeconds(stunDuration);
        isStunning = false;
    }

    private void Flip()
    {
        if ((axisMove.x < 0 && flipX) || (axisMove.x > 0 && !flipX))
        {
            flipX = !flipX;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }
}
