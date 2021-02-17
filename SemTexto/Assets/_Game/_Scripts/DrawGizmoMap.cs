using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmoMap : MonoBehaviour
{
    [SerializeField]
    private Vector2 AspectRatioCamera = default;
    [SerializeField]
    private Vector2 SizeMap = default;

    private void OnDrawGizmos()
    {
        // Width
        for (int i = 0; i <= SizeMap.x; i++)
        {
            Gizmos.DrawLine(new Vector3(i * AspectRatioCamera.x, 0, 0), new Vector3(i * AspectRatioCamera.x, 1 * AspectRatioCamera.y * SizeMap.y, 0));
        }

        // Height
        for (int i = 0; i <= SizeMap.y; i++)
        {
            Gizmos.DrawLine(new Vector3(0, i * AspectRatioCamera.y, 0), new Vector3(AspectRatioCamera.x * SizeMap.x, i * AspectRatioCamera.y, 0));
        }
    }
}
