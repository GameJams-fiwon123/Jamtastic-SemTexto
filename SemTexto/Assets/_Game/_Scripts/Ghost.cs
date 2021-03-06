﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public enum states { Appear, FollowPlayer, Stunned, PreDash, Dash, FollowRoom, Vanish }
    public states currentState = default;

    [SerializeField]
    private Rigidbody2D Rb2D = default;
    [SerializeField]
    private Collider2D collider = default;
    [SerializeField]
    private SpriteRenderer sprRenderer = default;
    private Color currentColor = default;

    private Vector2 axisMove = default;
    private bool flipX = default;

    private Item item = default;
    private Transform roomTransform = default;
    private float distanceRoom = 9999999;
    private float distancePlayer = 999999;

    private float waitTime = 0f;

    private float totalDash = 3f;
    private float currentDash = 0f;

    private void Start()
    {
        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;
        currentColor = sprRenderer.color;
        currentColor.a = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BagManager.instance.HasItems() && item == null)
        {
            currentState = states.Vanish;
        }

        switch (currentState)
        {
            case states.Appear:
                axisMove = Vector2.zero;
                currentColor.a = Mathf.Clamp(currentColor.a + 1 * Time.deltaTime, 0f, 1f);
                sprRenderer.color = currentColor;
                if(sprRenderer.color.a == 1f)
                {
                    currentState = states.FollowPlayer;
                }
                break;
            case states.FollowPlayer:
                axisMove = Vector2.zero;
                GetMove();
                GetDistancePlayer();
                if (distancePlayer < 5f)
                { 
                    waitTime = 0.5f;
                    currentState = states.PreDash;
                }
                break;
            case states.PreDash:
                axisMove = Vector2.zero;
                if (waitTime > 0f)
                {
                    waitTime -= Time.deltaTime;
                }
                else
                {
                    axisMove = Player.instance.transform.position - transform.position;
                    axisMove = axisMove.normalized;
                    if (Player.instance.isRight)
                    {
                        axisMove.x += 0.1f;
                    }
                    else
                    {
                        axisMove.x -= 0.1f;
                    }

                    currentDash++;
                    currentState = states.Dash;
                    waitTime = 0.5f;
                }
                break;
            case states.Dash:
                if (waitTime > 0f)
                {
                    waitTime -= Time.deltaTime;
                }
                else
                {
                    currentState = states.Stunned;
                    waitTime = 1f;
                }
                break;
            case states.Stunned:
                collider.enabled = false;
                axisMove = Vector2.zero;
                if (waitTime > 0f)
                {
                    currentColor.a = 0.5f;
                    sprRenderer.color = currentColor;
                    waitTime -= Time.deltaTime;
                }
                else
                {
                    currentColor.a = 1f;
                    sprRenderer.color = currentColor;

                    if (currentDash == totalDash)
                    {
                        currentState = states.Vanish;
                    }
                    else
                    {
                        collider.enabled = true;
                        currentState = states.FollowPlayer;
                    }
                }
                break;
            case states.FollowRoom:
                currentColor.a = 1f;
                sprRenderer.color = currentColor;

                axisMove = Vector2.zero;
                GetMove();
                GetDistanceRoom();
                if (distanceRoom < 1f)
                {
                    item.transform.parent = roomTransform;
                    item.transform.position = roomTransform.position;
                    item.collider2d.enabled = true;
                    if (item.currentType == Item.type.Note)
                    {
                        item.GetComponent<Note>().ChangeSpatialBlend(1f);
                        item.GetComponent<Note>().DiscoverRooms();
                    }
                    item = null;
                    roomTransform = null;
                    currentState = states.Vanish;
                }
                break;
            case states.Vanish:
                collider.enabled = false;
                axisMove = Vector2.zero;
                currentColor.a = Mathf.Clamp(currentColor.a - 1 * Time.deltaTime, 0f, 1f);
                sprRenderer.color = currentColor;
                if (sprRenderer.color.a == 0f)
                {
                    GameManager.instance.DespawnGhost();
                    Destroy(gameObject);
                }
                break;
        }



    }

    private void GetDistancePlayer()
    {
        distancePlayer = Vector2.Distance(Player.instance.transform.position, transform.position);
    }

    private void OnChangeRoom(int nextRoom)
    {
        if (currentState == states.Stunned)
        {
            GameManager.instance.DespawnGhost();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        MainCamera.instance.followPlayer.changeRoom -= OnChangeRoom;
    }

    private void GetDistanceRoom()
    {
        if (roomTransform != null)
            distanceRoom = Vector2.Distance(roomTransform.position, transform.position);
        else
            distanceRoom = 9999999;
    }

    private void GetMove()
    {
        if (item == null)
        {
            axisMove = Player.instance.transform.position - transform.position;
        } 
        else
        {
            axisMove = roomTransform.position - transform.position;
        }

        axisMove = axisMove.normalized;
    }

    private void FixedUpdate()
    {
        if (currentState != states.Dash)
        {
            Rb2D.velocity = axisMove * GameManager.instance.speedGhost * Time.deltaTime;
        }
        else
        {
            Rb2D.velocity = axisMove * (GameManager.instance.speedGhost * 2) * Time.deltaTime;
        }
        Flip();

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item = BagManager.instance.GetFirstItem();
        if (item != null)
        {
            item.transform.parent = transform.GetChild(0);
            item.transform.position = transform.GetChild(0).position;

            roomTransform = RoomsManager.instance.rooms[UnityEngine.Random.Range(0, RoomsManager.instance.rooms.Length)];
            currentState = states.FollowRoom;

            collider.enabled = false;
        }
    }
}
