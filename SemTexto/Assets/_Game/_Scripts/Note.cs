using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Item
{
    [SerializeField]
    private int[] idRoomActivate = default;
    [SerializeField]
    private AudioSource audioSource = default;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;
    }

    private void OnChangeRoom(int idRoom)
    {
        if (!collider2d.enabled)
        {
            return;
        }

        foreach(int id in idRoomActivate)
        {
            if (idRoom == id)
            {
                audioSource.enabled = true;
                return;
            }
        }

        audioSource.enabled = false;
    }

    private void OnDestroy()
    {
        MainCamera.instance.followPlayer.changeRoom -= OnChangeRoom;
    }
}
