using UnityEngine;
using UnityEngine.UI;


public class FinalCoinsDisplayer : MonoBehaviour
{
    [SerializeField] GameObject[] coinsUI;

    GameCoin[] gameCoins;

    private void Awake()
    {
        gameCoins = FindObjectsOfType<GameCoin>();
    }

    private void Start()
    {
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
