using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider2d = default;
    [SerializeField]
    private Transform[] notePositions = default;
    private int index = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isStart)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(VerifyBag());
        }
    }

    private IEnumerator VerifyBag()
    {
        Item item;
        bool alreadyPlay = false;
        while (item = BagManager.instance.GetItem(Item.type.Note))
        {
            item.GetComponent<Note>().ChangeSpatialBlend(1f);
            item.GetComponent<Note>().DiscoverRooms();
            item.transform.parent = notePositions[index].transform;
            item.transform.position = notePositions[index].position;
            index++;
            GameManager.instance.CollectNote();
            if (!alreadyPlay)
            {
                SFXManager.instance.PlayAltar();
                alreadyPlay = true;
            }

            yield return null;
        }

        if (index >= notePositions.Length)
        {
            collider2d.enabled = false;
            GameManager.instance.YouWin();
        }
    }
}
