using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider2d = default;
    [SerializeField]
    private GameObject[] notes = default;
    private int index = default;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(VerifyBag());
        }
    }

    private IEnumerator VerifyBag()
    {
        while (BagManager.instance.UseItem(Item.type.Note))
        {
            notes[index].SetActive(true);
            index++;

            yield return null;
        }

        if (index >= notes.Length)
        {
            collider2d.enabled = false;
            GameManager.instance.YouWin();
        }
    }
}
