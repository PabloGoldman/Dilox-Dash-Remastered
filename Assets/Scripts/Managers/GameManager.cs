using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour, ISaveable
{
    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Action onSceneChanged;
    public Action onLevelUnlocked;

    [SerializeField] PlayerData playerData;

    public int playerCoins;

    public bool[] levelsLocked;

    public void UseCoins(int amount)
    {
        playerCoins -= amount;
    }

    public void AddCoins()
    {
        playerCoins++;
    }

    public int GetCoins()
    {
        return playerCoins;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (playerCoins >= amount);
    }

    public void ChangeToGameplay(int level)
    {
        if (!levelsLocked[level])
        {
            onSceneChanged?.Invoke();
            SceneManager.LoadScene("Level" + level);
        }
    }

    public void ChangeToLevelSelection()
    {
        onSceneChanged?.Invoke();
        SceneManager.LoadScene(1);
    }

    public void ChangeToProfile()
    {
        onSceneChanged?.Invoke();
        SceneManager.LoadScene(0);
    }

    public void UnlockLevel(int index)
    {
        onLevelUnlocked?.Invoke();

        levelsLocked[index] = false;
    }

    public void SetPlayerAvatar(Sprite img)
    {
        playerData.avatarSprite = img;
    }

    public Sprite GetPlayerAvatar()
    {
        return playerData.avatarSprite;
    }

    public object SaveState()
    {
        return new SaveData()
        {
            playerCoins = this.playerCoins,
            levelsUnlocked = this.levelsLocked
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        playerCoins = saveData.playerCoins;
        levelsLocked = saveData.levelsUnlocked;
    }

    [Serializable]
    public struct SaveData
    {
        public int playerCoins;
        public bool[] levelsUnlocked;
    }
}
