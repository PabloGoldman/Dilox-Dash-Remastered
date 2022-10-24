using UnityEngine;
using UnityEngine.UI;

public class FinalCoinsDisplayer : MonoBehaviour
{
    [SerializeField] GameObject[] coinsUI;

    GameCoin[] gameCoins;

    Button backButton;

    private void Awake()
    {
        gameCoins = FindObjectsOfType<GameCoin>();
        backButton = transform.GetChild(0).GetComponent<Button>();
    }

    private void Start()
    {
        backButton.onClick.AddListener(AdsManager.instance.ShowInterstitial);
        SetCoinsAchieved();
    }

    void SetCoinsAchieved()
    {
        for (int i = 0; i < gameCoins.Length; i++)
        {
            if (!gameCoins[i].isAvailable)
            {
                coinsUI[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
