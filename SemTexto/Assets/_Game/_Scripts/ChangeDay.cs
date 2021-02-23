using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDay : MonoBehaviour
{
    [SerializeField]
    private GameObject unlockItem = default;
    [SerializeField]
    private int idRoomUnlock = default;

    private bool isUnlocked = default;

    // Start is called before the first frame update
    void Start()
    {
        BackgroundManager.instance.changedBackground += OnChangeRoom;
    }

    private void OnChangeRoom(int idRoom)
    {
        if (!isUnlocked && idRoom == idRoomUnlock && BackgroundManager.instance.currentTime == BackgroundManager.typeTime.Fundo7)
        {
            unlockItem.SetActive(true);
            isUnlocked = true;
        }
    }
}
