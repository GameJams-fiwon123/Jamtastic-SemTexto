using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector2 AspectRatioCamera => MainCamera.instance.aspectRatioCamera;
    private Vector2 nextPosition = default;

    void Update()
    {
        nextPosition = Vector2.zero;

        nextPosition.x = (int) (Player.instance.transform.position.x / AspectRatioCamera.x) * AspectRatioCamera.x;
        nextPosition.y = (int) (Player.instance.transform.position.y / AspectRatioCamera.y) * AspectRatioCamera.y;

        transform.position = new Vector2(nextPosition.x, nextPosition.y);
       
    }
}
