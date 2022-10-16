using UnityEngine;

public class GameCoin : MonoBehaviour
{
    LevelManager levelManager;

    public bool isAvailable;

    public int coinIndex;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isAvailable)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

    public void SetIfAvailable()
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

                levelManager.DisableGameCoin(coinIndex);

                isAvailable = false;

                GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }
}
