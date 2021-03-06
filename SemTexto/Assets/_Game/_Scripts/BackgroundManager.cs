using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BackgroundManager : MonoBehaviour
{
    public enum typeTime { Fundo1, Fundo2, Fundo3, Fundo4, Fundo5, Fundo6, Fundo7 }
    public typeTime currentTime = default;

    [SerializeField]
    private Animator    anim = default;

    private int index = default;
    private int beforeIndex = default;

    public static BackgroundManager instance;

    public delegate void ChangedBackground(int idRoom);
    public event ChangedBackground changedBackground;

    float waitTime = 1f;
    float currentWaitTime = 0f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;
    }

    private void Update()
    {
        currentWaitTime += Time.deltaTime;
    }

    private void OnChangeRoom(int nextRoom)
    {
        // Debug.Log("[BackgroundManager] OnChangeRoom: " + nextRoom);
        Next(nextRoom);
    }

    public void Next(int nextRoom)
    {
        if (currentWaitTime > waitTime)
        {
            beforeIndex = index;
            index++;
        }
        else
        {
            int auxIndex = index;
            index = beforeIndex;
            beforeIndex = auxIndex;
        }


        currentWaitTime = 0f;

        if (index >= Enum.GetValues(typeof(typeTime)).Length)
        {
            index = 0;
        } 
        else if (index < 0)
        {
            index = Enum.GetValues(typeof(typeTime)).Length - 1;
        }

        currentTime = (typeTime)index;

        anim.Play(currentTime.ToString());

        changedBackground?.Invoke(nextRoom);
    }
}
