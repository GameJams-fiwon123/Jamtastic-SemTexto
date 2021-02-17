using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Rb2D = default;
    [SerializeField]
    private float Speed = default;
    [SerializeField]
    private float JumpForce = default;

    [SerializeField]
    private Transform floorChecker = default;
    [SerializeField]
    private LayerMask floorLayer = default;


    private float greaterGravity = 6, lessGravity = 1; //Less 2
    private Vector2 axisMove = default;
    private bool inFloor = default;
    private float jumpTimeCounter = default;
    public float JumpTime = default;
    private bool canJump = default;
    private float inFloorRadius = 0.05f;
    //private bool isJumping = default;

    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetJump();

        GetMove();
    }

    private void FixedUpdate()
    {

        Move();

        Jump();

        //Flip();
    }

    private void GetJump()
    {
        if (inFloor && (Input.GetKeyDown(KeyCode.UpArrow)))
        {
            canJump = true;
            jumpTimeCounter = JumpTime;
            Rb2D.velocity = Vector2.up * JumpForce;
            //SfxManager.Instance.PlaySfxPlayerLand();
        }

        if (canJump && (Input.GetKey(KeyCode.UpArrow)))
        {
            if (jumpTimeCounter > 0)
            {
                Rb2D.velocity = Vector2.up * JumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                canJump = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            canJump = false;
        }
    }

    private void GetMove()
    {
        axisMove.Set(Input.GetAxis("Horizontal"), 0f);
    }

    private void Move()
    {
        Rb2D.velocity = new Vector2(axisMove.x * Speed, Rb2D.velocity.y);

        //if (axisMove == 0)
        //{
        //    isRunning = false;
        //}
        //else
        //{
        //    isRunning = true;
        //}
    }

    private void Jump()
    {
        inFloor = Physics2D.OverlapCircle(floorChecker.position, inFloorRadius, floorLayer);

        if (Rb2D.velocity.y < 0)
        {
            Rb2D.gravityScale = lessGravity;
            //isJumping = false;
        }
        else if (Rb2D.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rb2D.gravityScale = greaterGravity;
            //isJumping = true;
        }
        else
        {
            Rb2D.gravityScale = 1;
            //isJumping = false;
        }
    }

    //private void Flip()
    //{
    //    if ((axisMove < 0 && flipX) || (axisMove > 0 && !flipX))
    //    {
    //        flipX = !flipX;
    //        Vector3 newScale = transform.localScale;
    //        newScale.x *= -1;
    //        transform.localScale = newScale;
    //    }
    //}
}
