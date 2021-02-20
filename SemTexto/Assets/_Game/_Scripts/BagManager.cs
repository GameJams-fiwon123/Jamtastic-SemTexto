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
