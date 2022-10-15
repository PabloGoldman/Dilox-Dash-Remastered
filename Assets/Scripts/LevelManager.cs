using System;
using UnityEngine;

public class LevelManager : MonoBehaviour, ISaveable
{
    [SerializeField] GameCoin[] gameCoins;

    public bool[] isCoinAvailable;

    [SerializeField] GameObject endGamePanel;

    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        endGamePanel.SetActive(false);
    }

    void Start()
    {
        player.onEndGame += EnableEndGameScreen;

        SaveLoadSystem.instance.Load();

        Invoke(nameof(LoadCoinsData), 0.5f);
    }

    void LoadCoinsData()
    {
        for (int i = 0; i < gameCoins.Length; i++)
        {
            gameCoins[i].isAvailable = isCoinAvailable[i];

            gameCoins[i].coinIndex = i;

            gameCoins[i].SetIfAvailable();
        }
    }

    public void ChangeToProfile()
    {
        GameManager.instance.ChangeToProfile();
    }

    public void DisableGameCoin(int coinIndex)
    {
        isCoinAvailable[coinIndex] = false;
    }

    public void EnableEndGameScreen()
    {
        endGamePanel.SetActive(true);
    }

    public object SaveState()
    {
        return new SaveData()
        {
            isCoinAvailable = this.isCoinAvailable
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        isCoinAvailable = saveData.isCoinAvailable;
    }

    [Serializable]
    public struct SaveData
    {
        public bool[] isCoinAvailable;
    }
}
