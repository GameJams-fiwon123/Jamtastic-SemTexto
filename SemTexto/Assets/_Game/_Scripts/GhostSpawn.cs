using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnTransformsLeft = default;
    [SerializeField]
    private Transform[] spawnTransformsRight = default;
    [SerializeField]
    private GameObject prefabGhost = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (prefabGhost != null && CanSpawn() && GameManager.instance.CanSpawnGhost() )
        {
            if (Player.instance.isRight)
            {
                Transform t = spawnTransformsRight[Random.Range(0, spawnTransformsRight.Length)];
                Instantiate(prefabGhost, t.position, t.rotation);
                GameManager.instance.SpawnGhost();
            } 
            else
            {
                Transform t = spawnTransformsLeft[Random.Range(0, spawnTransformsRight.Length)];
                Instantiate(prefabGhost, t.position, t.rotation);
                GameManager.instance.SpawnGhost();
            }
        }
    }

    private bool CanSpawn()
    {
        //return BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo7 ||
        //       BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo5 ||
        //       BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo6;
        return BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo5;
    }
}