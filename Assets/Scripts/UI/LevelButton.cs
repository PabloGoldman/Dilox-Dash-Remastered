using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public bool isLocked;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                Debug.LogError($"current touch {Input.touches}");
                Debug.LogError($"touch count is {Input.touchCount}");
                Vector3 touchPos = t.position;
                if (t.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touchPos);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit.collider != null && hit.transform == transform)
                    {
                        Debug.Log($"Hit: {hit.transform.name}");
                        MyOnMouseDown();
                        break;
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isLocked)
        {
            Debug.Log(levelIndex);
        }
    }

    private void MyOnMouseDown()
    {
        if (!isLocked)
        {
            Debug.Log(levelIndex);
        }
    }
}
