using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    private bool isQuitting = false;
    private bool isQuit = false;
    [SerializeField]
    private SpriteRenderer sprRenderer = default;
    [SerializeField]
    private float waitTime = 4;
    private float currentTime = default;
    private Color currentColor = default;
    [SerializeField]
    private GameObject startTimeLine = default;
         
    private void Start()
    {
        currentColor = sprRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startTimeLine.activeSelf)
        {
            return;
        }

        if (isQuit)
        {
            currentColor.a = Mathf.Clamp(currentColor.a + 1 * Time.deltaTime, 0f, 1f);
            sprRenderer.color = currentColor;

            if (currentColor.a == 1f)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isQuitting)
            {
                isQuit = true;
            }
            else
            {
                currentTime = waitTime;
                isQuitting = true;
            }
        }

        currentTime -= Time.deltaTime;
        if (currentTime < 0f)
        {
            currentColor.a = Mathf.Clamp(currentColor.a - 1 * Time.deltaTime, 0f, 0.5f);
            isQuitting = false;
        }
        else
        {
            currentColor.a = Mathf.Clamp(currentColor.a + 1 * Time.deltaTime, 0f, 0.5f);
        }

        sprRenderer.color = currentColor;
    }
}
