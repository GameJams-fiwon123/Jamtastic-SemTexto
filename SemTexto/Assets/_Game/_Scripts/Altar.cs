using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider2d = default;
    [SerializeField]
    private Note[] notes = default;
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
            Note bagNote = item.GetComponent<Note>();
            for (int i = 0; i < notes.Length; i++)
            {
                if (bagNote.currentAudio.clip == notes[i].currentAudio.clip)
                {
                    bagNote.transform.parent = notes[i].transform.parent;
                    bagNote.ChangeSpatialBlend(1f);
                    bagNote.DiscoverRooms();
                    bagNote.transform.localPosition = Vector3.zero;
                    Destroy(notes[i].gameObject);
                    notes[i] = bagNote;
                    index++;
                    break;
                }
            }

            if (!alreadyPlay)
            {
                SFXManager.instance.PlayAltar();
                alreadyPlay = true;
            }

            yield return null;
        }

        if (index >= notes.Length)
        {
            collider2d.enabled = false;
            GameManager.instance.YouWin();
        }
    }
}
