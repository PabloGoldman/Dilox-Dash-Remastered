using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject endGamePanel;
    // Start is called before the first frame update
    void Start()
    {
        endGamePanel.SetActive(false);

        if (GameManager.instance.levelToInstantiate.levelGameObject)
        {
            Instantiate(GameManager.instance.levelToInstantiate.levelGameObject);
        }
    }

    public void AbleEndGameScreen()
    {
        endGamePanel.SetActive(true);
    }

    public void ChangeToProfile()
    {
        GameManager.instance.ChangeToProfile();
    }
}
