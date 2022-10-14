using System;
using UnityEngine;

public class CoinsManager : MonoBehaviour, ISaveable
{
    [SerializeField] GameCoin[] gameCoins;

    public bool[] isCoinAvailable;

    private void Awake()
    {
        for (int i = 0; i < gameCoins.Length; i++)
        {
            gameCoins[i].isAvailable = isCoinAvailable[i];
            gameCoins[i].coinIndex = i;
        }
    }
     
    public void DisableGameCoin(int coinIndex)
    {
        isCoinAvailable[coinIndex] = false;
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
