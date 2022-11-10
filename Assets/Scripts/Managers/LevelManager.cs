using System;
using UnityEngine;

public class LevelManager : MonoBehaviour, ISaveable
{
    [SerializeField] GameCoin[] gameCoins;

    public bool[] isCoinAvailable;

    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject pauseButton;

    ParticleSystem coinParticles;
    
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        endGamePanel.SetActive(false);
        coinParticles = GameObject.Find("Coin Particles").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        gameCoins = FindObjectsOfType<GameCoin>();

        player.onEndGame += EnableEndGameScreen;

        GameCoin.onCoinReached += EnableCoinPartycleSystem;

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
        GameManager.instance.ChangeToMenu();
    }

    public void DisableGameCoin(int coinIndex)
    {
        isCoinAvailable[coinIndex] = false;
    }

    public void EnableEndGameScreen()
    {
        endGamePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    void EnableCoinPartycleSystem(Transform spawnPoint)
    {
        //coinParticles.gameObject.SetActive(true);
        coinParticles.transform.position = spawnPoint.position;
        coinParticles.Play();
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
