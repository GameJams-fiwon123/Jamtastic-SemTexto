using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    private Item.type typeItem = default;
    [SerializeField]
    private Item unlockItem = default;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool usedItem = BagManager.instance.UseItem(typeItem);
            if (usedItem)
            {
                SFXManager.instance.PlayHammer();
                unlockItem.anim.enabled = true;
                unlockItem.collider2d.enabled = true;

                Destroy(gameObject);
            }
        }
    }
}
