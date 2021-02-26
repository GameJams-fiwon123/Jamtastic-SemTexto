using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] Rooms;
    public Transform[] rooms => Rooms;

    public static RoomsManager instance;

    private void Awake()
    {
        instance = this;
    }

}
