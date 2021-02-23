using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    [SerializeField]
    private Animator fade = default;

    public void FadeIn()
    {
        fade.SetBool("Fade", true);
    }

    public void FadeCenter()
    {
        // TODO 
    }

    public void FadeOut()
    {
        fade.SetBool("Fade", false);
    }
}
