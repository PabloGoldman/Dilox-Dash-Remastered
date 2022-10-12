using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] allCoinsUIText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAllCoinsUIText();
    }

    public void UpdateAllCoinsUIText()
    {
        foreach (TextMeshProUGUI coinText in allCoinsUIText)
        {
            coinText.text = GameManager.instance.GetCoins().ToString();
        }
    }
}
