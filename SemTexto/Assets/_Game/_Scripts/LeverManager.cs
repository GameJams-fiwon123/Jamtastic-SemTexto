using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject openObject = default;
    [SerializeField]
    private Lever[] leversOrder = default;

    private int nextIndex = default;
    private bool isCorrect = true;

    public void activateLever(Lever lever)
    {
        if (leversOrder[nextIndex] != lever){
            isCorrect = false;
        }
        nextIndex++;

        if (nextIndex >= leversOrder.Length)
        {
            nextIndex = 0;
            if (isCorrect)
                Destroy(openObject);
            else
            {
                StartCoroutine(DeactivateAllLevers());
                isCorrect = true;
            }
        }
    }

    private IEnumerator DeactivateAllLevers()
    {

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < leversOrder.Length; i++)
        {
            leversOrder[i].deactivate();
        }
    }
}
