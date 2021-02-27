using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{ 
    public FollowPlayer followPlayer = default;
    public DrawGizmoMap drawGizmoMap = default;
    public FadeManager fade = default;
    public Animator animCamera = default;

    [SerializeField]
    private Vector2 AspectRatioCamera = default;
    public Vector2 aspectRatioCamera => AspectRatioCamera;


    public static MainCamera instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayStun()
    {
        animCamera.Play("ShakeStun");
    }

    public void PlayExplosion()
    {
        animCamera.Play("ShakeExplosion");
    }

    public void PlayBox()
    {
        animCamera.Play("ShakeBox");
    }

    public void PlayDoor()
    {
        animCamera.Play("ShakeDoor");
    }
}
