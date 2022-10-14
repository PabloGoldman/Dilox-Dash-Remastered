using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Transform[] levels;

    [SerializeField] LevelSO[] levelsSO; 

    [SerializeField] CinemachineVirtualCamera myCinemachine;

    int currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].GetChild(0).GetComponent<TMP_Text>().text = levelsSO[i].title;
            levels[i].GetChild(1).GetComponent<TMP_Text>().text = levelsSO[i].description;

            levels[i].GetChild(3).gameObject.SetActive(GameManager.instance.levelsLocked[i]);
        }
    }

    public void ChangeToGameplay()
    {
        GameManager.instance.ChangeToGameplay(currentIndex);
    }

    public void RightClick()
    {
        Move(1);
    }

    public void LeftClick()
    {
        Move(-1);
    }

    void Move(int dir)
    {
        currentIndex += dir;

        currentIndex = (currentIndex < 0) ? levels.Length - 1 : currentIndex;
        currentIndex = (currentIndex >= levels.Length) ? 0 : currentIndex;

        myCinemachine.m_Follow = levels[currentIndex];
    }
}
