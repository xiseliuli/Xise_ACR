using FFXIVClientStructs.FFXIV.Client.Game;

namespace Xise.Common;

public class ItemHelper
{
    /// <summary>
    /// 获取背包内指定物品的数量
    /// </summary>
    public static unsafe int FindItem(uint itemId, ItemFlag flag = ItemFlag.NQHQ)
    {
        var count = 0;

        for (var i = 0; i < 4; ++i)
        {
            var container = InventoryManager.Instance()->GetInventoryContainer((InventoryType)i);
            for (var j = 0; j < container->Size; j++)
            {
                var item = container->GetInventorySlot(j);
                if (item == null)
                    continue;
                //匹配物品id
                if (item->ItemId != itemId)
                    continue;
                //匹配flag
                var f = item->Flags;
                switch (flag)
                {
                    case ItemFlag.NQ:
                        if (f != InventoryItem.ItemFlags.None)
                            continue;
                        break;
                    case ItemFlag.HQ:
                        if (f != InventoryItem.ItemFlags.HighQuality)
                            continue;
                        break;
                    case ItemFlag.NQHQ:
                        if (f != InventoryItem.ItemFlags.HighQuality && f != InventoryItem.ItemFlags.None)
                            continue;
                        break;
                    case ItemFlag.Collectable:
                        if (f != InventoryItem.ItemFlags.Collectable)
                            continue;
                        break;
                }

                count += (int)item->Quantity;
            }
        }

        return count;
    }
}

public enum ItemFlag
{
    NQ,
    HQ,
    NQHQ,
    Collectable,
}

public enum Potion
{
    _8级刚力之幻药 = 39727,
    _8级巧力之幻药 = 39728,
    _8级智力之幻药 = 39730,
    _8级意力之幻药 = 39731,
}