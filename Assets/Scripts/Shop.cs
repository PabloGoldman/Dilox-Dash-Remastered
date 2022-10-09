using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem //Pasar a scriptable object
    {
        public Sprite image;
        public int price;
        public bool isPurchased = false;
    }

    [SerializeField] List<ShopItem> shopItemsList;

    GameObject itemTemplate;
    [SerializeField] Transform shopScrollView;

    [SerializeField] Animator noCoinsAnim;
    [SerializeField] TextMeshProUGUI coinsText;

    Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        int lenght = shopItemsList.Count;

        for (int i = 0; i < lenght; i++)
        {
            GameObject go = Instantiate(itemTemplate, shopScrollView);
            go.transform.GetChild(0).GetComponent<Image>().sprite = shopItemsList[i].image;
            go.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItemsList[i].price.ToString();

            buyButton = go.transform.GetChild(2).GetComponent<Button>();
            buyButton.interactable = !shopItemsList[i].isPurchased;

            buyButton.AddEventListener(i, OnShopItemButtonClicked);
        }

        Destroy(itemTemplate);

        SetUICoins();
    }

    void OnShopItemButtonClicked(int itemIndex)
    {
        if (GameManager.instance.HasEnoughCoins(shopItemsList[itemIndex].price))
        {
            GameManager.instance.UseCoins(shopItemsList[itemIndex].price);
            shopItemsList[itemIndex].isPurchased = true;

            buyButton = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();

            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";

            SetUICoins();
        }
        else
        {
            noCoinsAnim.SetTrigger("NoCoins");
            Debug.Log("Not enough coins!");
        }
        
    }

    void SetUICoins()
    {
        coinsText.text = GameManager.instance.coins.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
