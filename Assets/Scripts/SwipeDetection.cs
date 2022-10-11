using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public LevelSelector levelSelector;

    private Vector2 startPos;

    public int pixelDistToDetect = 20; //Cantidad de pixeles que hay que swipear para que detecte el swipe

    private bool fingerDown;

    // Update is called once per frame
    void Update()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (Input.touches[0].position.x <= startPos.x - pixelDistToDetect)
            {
                fingerDown = false;
                levelSelector.LeftClick();
                Debug.Log("swipe left");
            }
            else if (Input.touches[0].position.x >= startPos.x + pixelDistToDetect)
            {
                fingerDown = false;
                levelSelector.RightClick();
                Debug.Log("swipe right");
            }
            else if (Input.touches[0].position.y >= startPos.y + pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("swipe up");
            }
            else if (Input.touches[0].position.y <= startPos.y - pixelDistToDetect)
            {
                fingerDown = false;
                Debug.Log("swipe down");
            }
        }

        if (fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            fingerDown = false;
        }

        if (fingerDown && Input.GetMouseButtonUp(0))
        {
            fingerDown = false;
        }
    }
}
