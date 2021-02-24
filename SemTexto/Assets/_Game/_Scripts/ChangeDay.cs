using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDay : MonoBehaviour
{
    [SerializeField]
    private GameObject unlockItem = default;
    [SerializeField]
    private int idRoomUnlock = default;
    [SerializeField]
    Animator estalactiteAnim = default;
    [SerializeField]
    SpriteRenderer estalactiteSpr = default;
    [SerializeField]
    ParticleSystem particlMoon = default;
    [SerializeField]
    ParticleSystem particlDestroy = default;
    [SerializeField]
    ParticleSystem particlExplosion = default;
    [SerializeField]
    float waitTime = 5f;

    private bool isUnlocked = default;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundManager.instance.changedBackground += OnChangeRoom;
    }

    private void OnChangeRoom(int idRoom)
    {
        if (idRoom == idRoomUnlock && BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo7)
        {
            particlMoon.Play();
        } else
        {
            particlMoon.Stop();
        }

        if (!isUnlocked && idRoom == idRoomUnlock && BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo7)
        {
            particlDestroy.Play();
            estalactiteAnim.SetBool("IsShake", true);
            StartCoroutine(WaitTime(waitTime));
        } else
        {
            estalactiteAnim.SetBool("IsShake", false);
            StopAllCoroutines();
            particlDestroy.Stop();
        }
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        unlockItem.SetActive(true);
        isUnlocked = true;
        particlDestroy.Stop();
        particlExplosion.Play();
        estalactiteSpr.enabled = false;
    }
}
