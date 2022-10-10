using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
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

    [SerializeField] PlayerData playerData;

    [SerializeField] TextMeshProUGUI[] allCoinsUIText;

    public LevelSO levelToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllCoinsUIText();
    }

    public void UseCoins(int amount)
    {
        playerData.coins -= amount;
    }

    public void AddCoins()
    {
        playerData.coins++;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (playerData.coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        foreach (TextMeshProUGUI coinText in allCoinsUIText)
        {
            coinText.text = playerData.coins.ToString();
        }
    }

    public void SetPlayerAvatar(Sprite img)
    {
        playerData.avatarSprite = img;
    }

    public Sprite GetPlayerAvatar()
    {
        return playerData.avatarSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
