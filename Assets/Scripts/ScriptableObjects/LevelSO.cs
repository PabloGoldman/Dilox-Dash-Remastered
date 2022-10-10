using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public GameObject levelGameObject;
    public int levelId;
    public bool isLocked;

    public string title;
    public string description;
}
