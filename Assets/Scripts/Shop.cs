using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    [System.Serializable] class ShopItem
    {
        public Sprite image;
        public int price;
        public bool isPurchased = false;
    }

    [SerializeField] List<ShopItem> shopItemsList;

    GameObject itemTemplate;
    [SerializeField] Transform shopScrollView;

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
    }

    void OnShopItemButtonClicked(int itemIndex)
    {
        shopItemsList[itemIndex].isPurchased = true;

        buyButton = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();

        buyButton.interactable = false;
        buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
