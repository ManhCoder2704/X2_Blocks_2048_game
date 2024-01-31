using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemData", menuName = "ScriptableObjects/ShopItemSO")]
public class ShopItemSO : ScriptableObject
{
    [SerializeField] private List<ShopItemData> _listShopItems;

    public int ShopItemListCount()
    {
        return _listShopItems.Count;
    }
    public ShopItemData GetShopItemByIndex(int index)
    {
        return _listShopItems[index];
    }
}
