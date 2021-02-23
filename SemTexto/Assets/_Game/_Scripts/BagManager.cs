using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField]
    private float smoothSpeed = 0.1f;

    public static BagManager instance;

    private void Awake()
    {
        instance = this;
    }

    public bool UseItem(Item.type typeItem)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) && transform.GetChild(i).GetComponent<Item>().currentType == typeItem)
            {
                Destroy(transform.GetChild(i).gameObject);
                return true;
            }
        }

        return false;
    }

    public Item GetItem(Item.type typeItem)
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) && transform.GetChild(i).GetComponent<Item>().currentType == typeItem)
            {
                
                return transform.GetChild(i).GetComponent<Item>();
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePositionItems();
    }

    private void UpdatePositionItems()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, 
                                                          transform.GetChild(i-1).position, 
                                                          smoothSpeed * Time.deltaTime);
        }
    }

    public void AddItem(Transform item)
    {
        item.parent = transform;
    }
}
