using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BackgroundManager : MonoBehaviour
{
    public enum typeTime { night, morning }
    public typeTime currentTime = default;

    [SerializeField]
    private Sprite[] sprites = default;
    [SerializeField]
    private SpriteRenderer sprRenderer = default;

    private int index = default;

    public static BackgroundManager instance;

    public delegate void ChangedBackground(int idRoom);
    public event ChangedBackground changedBackground;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprRenderer.sprite = sprites[0];

        MainCamera.instance.followPlayer.changeRoom += OnChangeRoom;
    }

    private void OnChangeRoom(int nextRoom)
    {
        // Debug.Log("[BackgroundManager] OnChangeRoom: " + nextRoom);
        Next(nextRoom);
    }

    public void Next(int nextRoom)
    {
        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }

        currentTime = (typeTime)index;

        sprRenderer.sprite = sprites[index];

        changedBackground?.Invoke(nextRoom);
    }
}
