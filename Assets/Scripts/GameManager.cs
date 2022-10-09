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

    [SerializeField] TextMeshProUGUI[] allCoinsUIText;
    public int coins;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllCoinsUIText();
    }

    public void UseCoins(int amount)
    {
        coins -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return (coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        foreach (TextMeshProUGUI coinText in allCoinsUIText)
        {
            coinText.text = coins.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
