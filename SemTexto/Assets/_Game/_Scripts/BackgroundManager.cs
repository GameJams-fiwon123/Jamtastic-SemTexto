using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BackgroundManager : MonoBehaviour
{
    public enum typeTime { Fundo7, Fundo1, Fundo2, Fundo4, Fundo5 }
    public typeTime currentTime = default;

    [SerializeField]
    private Animator    anim = default;

    private int index = default;

    public static BackgroundManager instance;

    public delegate void ChangedBackground(int idRoom);
    public event ChangedBackground changedBackground;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;
    }

    private void OnChangeRoom(int nextRoom)
    {
        // Debug.Log("[BackgroundManager] OnChangeRoom: " + nextRoom);
        Next(nextRoom);
    }

    public void Next(int nextRoom)
    {
        index++;
        if (index >= Enum.GetValues(typeof(typeTime)).Length)
        {
            index = 0;
        }

        currentTime = (typeTime)index;

        anim.Play(currentTime.ToString());

        changedBackground?.Invoke(nextRoom);
    }
}
