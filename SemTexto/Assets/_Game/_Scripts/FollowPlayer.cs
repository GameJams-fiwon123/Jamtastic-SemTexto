using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector2 AspectRatioCamera => MainCamera.instance.aspectRatioCamera;
    private Vector2 nextPosition = default;

    public delegate void ChangeRoom(int nextRoom);
    public event ChangeRoom changeRoom;

    private int xRoom = default, 
                yRoom = default;
 
    private int idRoomLast = default;
    private int idRoom = default;

    [SerializeField]
    private bool isSmooth = default;
    [SerializeField]
    private float smoothSpeed = 5f;

    private void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        idRoom = idRoomLast = GetIdRoom();
        UpdatePositionRoom();
        UpdateNextPositionCamera();
        transform.position = nextPosition;
    }

    private int GetIdRoom()
    {
        return (xRoom+1) + (int)MainCamera.instance.drawGizmoMap.sizeMap.x * yRoom;
    }

    void Update()
    {
        nextPosition = Vector2.zero;

        UpdatePositionRoom();
        UpdateNextPositionCamera();

        if (isSmooth)
        {
            transform.position = Vector3.Lerp(transform.position, nextPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = nextPosition;
        }

        idRoomLast = idRoom;
        idRoom = GetIdRoom();

        if (idRoom != idRoomLast)
        {
           changeRoom?.Invoke(idRoom);
        }

    }

    private void UpdateNextPositionCamera()
    {
        nextPosition.x = xRoom * AspectRatioCamera.x;
        nextPosition.y = yRoom * AspectRatioCamera.y;
    }

    private void UpdatePositionRoom()
    {
        xRoom = (int)(Player.instance.transform.position.x / AspectRatioCamera.x);
        yRoom = (int)(Player.instance.transform.position.y / AspectRatioCamera.y);
    }
}
