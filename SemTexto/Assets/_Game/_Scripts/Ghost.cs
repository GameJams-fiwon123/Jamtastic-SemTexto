using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Rb2D = default;
    [SerializeField]
    private Collider2D collider = default;
    [SerializeField]
    private float Speed = default;

    private Vector2 axisMove = default;
    private bool flipX = default;

    private Item item = default;
    private Transform roomTransform = default;
    private float distanceRoom = 9999999;

    // Update is called once per frame
    void Update()
    {
        axisMove = Vector2.zero;

        GetMove();

        if (!BagManager.instance.HasItems() && item == null)
        {
            GameManager.instance.DespawnGhost();
            Destroy(gameObject);
        }

        GetDistanceRoom();
        if (distanceRoom < 1f)
        {
            item.transform.parent = roomTransform;
            item.transform.position = roomTransform.position;
            item.collider2d.enabled = true;
            item = null;
            roomTransform = null;
            Destroy(gameObject);
            GameManager.instance.DespawnGhost();
        }
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
        axisMove *= Speed; // * Time.deltaTime;
    }

    private void FixedUpdate()
    {

        Rb2D.velocity = axisMove * Time.deltaTime;

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

            collider.enabled = false;
        }
    }
}
