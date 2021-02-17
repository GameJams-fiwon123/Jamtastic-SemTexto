using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Vector2 AspectRatioCamera = default;
    public Vector2 aspectRatioCamera => AspectRatioCamera;

    public static MainCamera instance;

    private void Awake()
    {
        instance = this;
    }
}
