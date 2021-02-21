using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTime : MonoBehaviour
{
    [SerializeField]
    private GameObject openObject = default;
    [SerializeField]
    private Collider2D collider2d = default;

    private float waitTime = 20f;

    [SerializeField]
    private Transform collectableItem = default;

    private Vector3 newScale = default;

    private bool isCollected = default;

    private void Start()
    {
        newScale = transform.localScale;
    }

    private void Update()
    {
        if (collectableItem && collectableItem.parent == BagManager.instance.transform)
        {
            collider2d.enabled = false;
            newScale.x = -1;
            transform.localScale = newScale;
            openObject.SetActive(false);
            isCollected = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(DeactivateObject());
        }
    }

    IEnumerator DeactivateObject()
    {
        openObject.SetActive(false);
        newScale = transform.localScale;
        newScale.x = -1;
        transform.localScale = newScale;

        yield return new WaitForSeconds(waitTime);

        if (!isCollected)
        {
            openObject.SetActive(true);
            newScale.x = 1;
            transform.localScale = newScale;
        }
    }
}
