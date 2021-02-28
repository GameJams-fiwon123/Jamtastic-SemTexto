using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public PlayableDirector finalPlayable;
    public bool isStart = default;

    [SerializeField]
    private int maxGhostSpawns = 0;
    [SerializeField]
    private int currentGhostSpawns = 0;
    [SerializeField]
    [Range(0, 100)]
    private int chanceSpawn = 10;
    [SerializeField]
    private float SpeedGhost = 225f;
    public float speedGhost => SpeedGhost;

    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isStart = true;
    }

    public void CollectNote()
    {
        maxGhostSpawns++;
        chanceSpawn += 10;
        //SpeedGhost += 50f;
    }

    public void SpawnGhost()
    {
        currentGhostSpawns++;
    }

    public void DespawnGhost()
    {
        currentGhostSpawns--;
    }

    public bool CanSpawnGhost()
    {
        return currentGhostSpawns < maxGhostSpawns &&
               Random.Range(0, 100) < chanceSpawn &&
               BagManager.instance.HasItems();
    }

    // Update is called once per frame
    public void YouWin()
    {
        Debug.Log("You Win");
        isStart = false;
        finalPlayable.Play();
    }
}
