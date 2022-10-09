using UnityEngine;

[CreateAssetMenu(menuName = "ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public Sprite image;
    public int price;
    public bool isPurchased;
}
