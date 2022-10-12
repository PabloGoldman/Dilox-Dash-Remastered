using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Shop : MonoBehaviour, ISaveable
{
    #region Singleton
    public static Shop instance;

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

    public bool[] isPurchased; //Se usa para guardar los personajes desbloqueados post partida

    public List<ShopItemSO> shopItemsList;

    [SerializeField] Animator noCoinsAnim;

    [SerializeField] CoinsUIManager coinsManager;

    [SerializeField] GameObject itemTemplate;
    [SerializeField] Transform shopScrollView;
    [SerializeField] GameObject shopPanel;

    Button buyButton;

    // Start is called before the first frame update
    void Start()
    {
        int length = shopItemsList.Count;

        for (int i = 0; i < length; i++)
        {
            shopItemsList[i].isPurchased = isPurchased[i];
        }

        for (int i = 0; i < length; i++)
        {
            GameObject go = Instantiate(itemTemplate, shopScrollView);
            go.transform.GetChild(0).GetComponent<Image>().sprite = shopItemsList[i].image;
            go.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItemsList[i].price.ToString();

            buyButton = go.transform.GetChild(2).GetComponent<Button>();

            if (shopItemsList[i].isPurchased)
            {
                DisableBuyButton();
            }

            buyButton.AddEventListener(i, OnShopItemButtonClicked);
        }
    }

    void OnShopItemButtonClicked(int itemIndex)
    {
        if (GameManager.instance.HasEnoughCoins(shopItemsList[itemIndex].price))
        {
            GameManager.instance.UseCoins(shopItemsList[itemIndex].price);

            isPurchased[itemIndex] = true;
            shopItemsList[itemIndex].isPurchased = true;

            buyButton = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();

            DisableBuyButton();

            coinsManager.UpdateAllCoinsUIText();

            Profile.instance.AddAvatar(shopItemsList[itemIndex].image);
        }
        else
        {
            noCoinsAnim.SetTrigger("NoCoins");
        }

    }

    void DisableBuyButton()
    {
        buyButton.interactable = false;
        buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    public object SaveState()
    {
        return new SaveData()
        {
            isPurchased = this.isPurchased
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        isPurchased = saveData.isPurchased;
    }

    [Serializable]
    public struct SaveData
    {
        public bool[] isPurchased;
    }
}
