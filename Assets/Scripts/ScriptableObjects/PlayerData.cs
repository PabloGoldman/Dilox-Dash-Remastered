using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    public Sprite avatarSprite;
    public int coins;
}
