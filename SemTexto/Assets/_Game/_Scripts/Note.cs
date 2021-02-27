using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Item
{
    private Vector2 AspectRatioCamera => MainCamera.instance.aspectRatioCamera;

    private int[] idRoomNeighboor = default;
    [SerializeField]
    private AudioSource audioSource = default;

    private int xRoom = default,
            yRoom = default,
            currentRoom = default;

    private bool isFirstTimeCollect = true;


    // Start is called before the first frame update
    void Start()
    {
        idRoomNeighboor = new int[5];
        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;


        DiscoverRooms();
    }

    private void Update()
    {
        if (!GameManager.instance.isStart)
        {
            ChangeSpatialBlend(0f);
        }
    }

    public void DiscoverRooms()
    {
        xRoom = (int)(transform.position.x / AspectRatioCamera.x);
        yRoom = (int)(transform.position.y / AspectRatioCamera.y);
        currentRoom = (xRoom + 1) + (int)MainCamera.instance.drawGizmoMap.sizeMap.x * yRoom;

        idRoomNeighboor[0] = currentRoom - 1; // left
        idRoomNeighboor[1] = currentRoom + 1; // right
        idRoomNeighboor[2] = currentRoom + 5; // up
        idRoomNeighboor[3] = currentRoom - 5; // down
        idRoomNeighboor[4] = currentRoom; // currentRoom
    }

    private void OnChangeRoom(int idRoom)
    {
        if (transform.parent == BagManager.instance.transform)
        {
            return;
        }

        foreach(int id in idRoomNeighboor)
        {
            if (idRoom == id)
            {
                audioSource.volume = 1f;
                return;
            }
        }

        audioSource.volume = 0f;
    }


    public override void DetectPlayer()
    {
        ChangeSpatialBlend(0f);

        if (isFirstTimeCollect)
        {
            isFirstTimeCollect = false;
            GameManager.instance.CollectNote();
        }

    }

    public void ChangeSpatialBlend(float value)
    {
        audioSource.spatialBlend = value;
    }
}
