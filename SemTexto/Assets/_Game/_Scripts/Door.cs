﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Item.type typeItem = default;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool usedItem = BagManager.instance.UseItem(typeItem);
            if (usedItem)
            {
                SFXManager.instance.PlayOpenKey();
                Destroy(gameObject);
            }
        }
    }
}
