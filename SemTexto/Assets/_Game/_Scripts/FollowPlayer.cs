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

    private void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        idRoom = idRoomLast = GetIdRoom(); 
    }

    private int GetIdRoom()
    {
        return (xRoom+1) + (int)MainCamera.instance.drawGizmoMap.sizeMap.x * yRoom;
    }

    void Update()
    {
        nextPosition = Vector2.zero;

        UpdatePositionRoom();
        UpdatePositionCamera();

        transform.position = new Vector2(nextPosition.x, nextPosition.y);

        idRoomLast = idRoom;
        idRoom = GetIdRoom();

        if (idRoom != idRoomLast)
        {
           changeRoom?.Invoke(idRoom);
        }

    }

    private void UpdatePositionCamera()
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
