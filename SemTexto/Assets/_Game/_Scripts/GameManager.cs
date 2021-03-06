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

    private int puzzleResolved = default;

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
        puzzleResolved++;

        switch (puzzleResolved)
        {
            case 1:
                maxGhostSpawns = 1;
                chanceSpawn = 30;
                break;
            case 2:
                maxGhostSpawns = 2;
                chanceSpawn = 70;
                break;
            case 3:
                maxGhostSpawns = 2;
                chanceSpawn = 100;
                break;
        }

    }

    public void SpawnGhost()
    {
        currentGhostSpawns++;
    }

    public void DespawnGhost()
    {
        currentGhostSpawns--;
    }

    public int CanSpawnGhost()
    {
        int spawnGhost = default;

        if (currentGhostSpawns < maxGhostSpawns &&
               Random.Range(0, 100) < chanceSpawn &&
               BagManager.instance.HasItems())
        {
            int randNumber = Random.Range(0, 100);
            switch (maxGhostSpawns)
            {
                case 1:
                    spawnGhost = 1;
                    break;
                case 2:
                    if (randNumber < 75)
                    {
                        spawnGhost = 1;
                    } else
                    {
                        spawnGhost = 2;
                    }
                    break;
                case 3:
                    if (randNumber < 50)
                    {
                        spawnGhost = 1;
                    }
                    else
                    {
                        spawnGhost = 2;
                    }
                    break;
            }
        }

        spawnGhost = Mathf.Clamp(spawnGhost, 0, maxGhostSpawns - currentGhostSpawns);

        return spawnGhost;

    }

    // Update is called once per frame
    public void YouWin()
    {
        Debug.Log("You Win");
        isStart = false;
        finalPlayable.Play();
    }
}
