using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum type { Note, Hammer, Bomb, Key1, Key2 }
    public type currentType;

    [SerializeField]
    private Collider2D collider2d;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BagManager.instance.AddItem(transform);
            collider2d.enabled = false;
        }
    }
}
