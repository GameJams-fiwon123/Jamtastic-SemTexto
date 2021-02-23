using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public PlayableDirector finalPlayable;
    public static GameManager instance;
    public bool isStart = default;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isStart = true;
    }

    // Update is called once per frame
    public void YouWin()
    {
        Debug.Log("You Win");
        isStart = false;
        finalPlayable.Play();
    }
}
