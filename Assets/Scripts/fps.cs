using UnityEngine;
using UnityEngine.UI;

public class fps : MonoBehaviour
{
    Text fpsText;
    public int refreshRate = 10;
    int frameCounter;
    float totalTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fpsText = GetComponent<Text>();
        frameCounter = 0;
        totalTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCounter == refreshRate)
        {
            float averageFps = (1.0f / (totalTime / refreshRate));
            fpsText.text = averageFps.ToString("F1");
            frameCounter = 0;
            totalTime = 0;
        } else
        {
            totalTime += Time.deltaTime;
            frameCounter++;
        }
    }
}
