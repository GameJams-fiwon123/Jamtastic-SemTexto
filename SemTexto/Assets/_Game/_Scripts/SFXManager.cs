using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] alavancasClips;
    [SerializeField]
    private AudioClip[] altaresClips;
    [SerializeField]
    private AudioClip openKeyClip;
    [SerializeField]
    private AudioClip hammerClip;
    [SerializeField]
    private AudioClip correctClip;
    [SerializeField]
    private AudioClip wrongClip;
    [SerializeField]
    private AudioClip catchClip;
    [SerializeField]
    private AudioClip jumpClip;
    [SerializeField]
    private AudioClip quedaClip;
    [SerializeField]
    private AudioClip timerClip;

    public static SFXManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAlavanca()
    {
        audioSource.PlayOneShot(alavancasClips[Random.Range(0, alavancasClips.Length)]);
    }

    internal void Stop()
    {
        audioSource.Stop();
    }

    public void PlayAltar()
    {
        audioSource.PlayOneShot(altaresClips[Random.Range(0, altaresClips.Length)]);
    }

    public void PlayOpenKey()
    {
        audioSource.PlayOneShot(openKeyClip);
    }

    public void PlayHammer()
    {
        audioSource.PlayOneShot(hammerClip);
    }

    public void PlayCorrect()
    {
        audioSource.PlayOneShot(correctClip);
    }

    public void PlayWrong()
    {
        audioSource.PlayOneShot(wrongClip);
    }

    public void PlayCatch()
    {
        audioSource.PlayOneShot(catchClip);
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpClip, 0.6f);
    }

    public void PlayQueda()
    {
        audioSource.PlayOneShot(quedaClip);
    }

    public void PlayTimer()
    {
        audioSource.clip = timerClip;
        audioSource.Play();
        //audioSource.PlayOneShot(timerClip);
    }
}
