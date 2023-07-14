using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace shiretrmod.Items
{
    public class 随机袋 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 2;
            Item.value = Item.sellPrice(copper: 1); // 设置售价为1铜币
            Item.useTurn = true;
            Item.autoReuse = false;
        }


        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            SpawnRewards(player);
            Item.stack--;
            return true;
        }

        void SpawnRewards(Player player)
        {
            SpawnCoins(player);
            SpawnItems(player);
        }

        void SpawnCoins(Player player)
        {
            int coinAmount = Main.rand.Next(1, 6); // 生成1到5个金币
            player.QuickSpawnItem(new EntitySource_DebugCommand($"{nameof(shiretrmod)}_{nameof(shiretrmod)}"), ItemID.CopperCoin, coinAmount);
        }

        void SpawnItems(Player player)
        {
            int[] itemIds = { ItemID.DirtBlock, ItemID.Wood }; // 需要生成的物品ID数组

            int itemAmount = Main.rand.Next(1, 10); // 生成0到3个物品

            for (int i = 0; i < itemAmount; i++)
            {
                int randomIndex = Main.rand.Next(itemIds.Length);
                int itemId = itemIds[randomIndex];
                player.QuickSpawnItem(new EntitySource_DebugCommand($"{nameof(shiretrmod)}_{nameof(shiretrmod)}"), itemId, 1);
            }
        }
    }
}
