using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{ 
    public FollowPlayer followPlayer = default;
    public DrawGizmoMap drawGizmoMap = default;

    [SerializeField]
    private Vector2 AspectRatioCamera = default;
    public Vector2 aspectRatioCamera => AspectRatioCamera;

    public static MainCamera instance;

    private void Awake()
    {
        instance = this;
    }
}
