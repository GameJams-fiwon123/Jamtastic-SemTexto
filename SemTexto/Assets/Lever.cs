using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private Collider2D collider2d = default;

    private Vector3 newScale = default;

    private void Start()
    {
        newScale = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<LeverManager>().activateLever(this);
            activate();
        }
    }

    public void activate() 
    {
        collider2d.enabled = false;
        newScale.x = -1;
        transform.localScale = newScale;
    }

    public void deactivate()
    {
        collider2d.enabled = true;
        newScale.x = 1;
        transform.localScale = newScale;
    }
}
