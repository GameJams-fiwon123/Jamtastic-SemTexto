using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = default;
    [SerializeField]
    private SpriteRenderer sprRenderer = default;

    private int index = default;

    // Start is called before the first frame update
    void Start()
    {
        sprRenderer.sprite = sprites[0];

        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;
    }

    private void OnChangeRoom(int nextRoom)
    {
        Debug.Log("[BackgroundManager] OnChangeRoom: " + nextRoom);
        Next();
    }

    public void Next()
    {
        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }

        sprRenderer.sprite = sprites[index];
    }
}
