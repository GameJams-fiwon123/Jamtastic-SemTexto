using UnityEngine;
using System.Collections;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsCounterGui = null;

    float deltaTime = 0.0f;
    float msec, fps;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime;

        fpsCounterGui.text = string.Format("{1:0.}", msec, fps);
    }
}