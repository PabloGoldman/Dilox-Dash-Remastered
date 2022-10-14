using UnityEngine;

public class GameCoin : MonoBehaviour
{
    CoinsManager coinsManager;

    public bool isAvailable;

    public int coinIndex;

    private void Awake()
    {
        coinsManager = FindObjectOfType<CoinsManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isAvailable)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isAvailable)
            {
                GameManager.instance.AddCoins();

                coinsManager.DisableGameCoin(coinIndex);

                isAvailable = false;

                GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }
}
