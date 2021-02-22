using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum type { Note, Hammer, Bomb, Key1, Key2 }
    public type currentType = default;

    [SerializeField]
    private Collider2D Collider2d = default;
    public Collider2D collider2d => Collider2d;
    [SerializeField]
    private Animator Anim = default;
    public Animator anim => Anim; 

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BagManager.instance.AddItem(transform);
            Collider2d.enabled = false;
            DetectPlayer();
        }
    }

    public virtual void DetectPlayer() { }
}
