using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Items
{
    public class 老马徽章 : ModItem
    {

        public override void SetDefaults()
        {
            Item.accessory = true; // 设置为饰品
            Item.rare = ItemRarityID.Red; // 饰品稀有度
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            // 在这里可以进行饰品的额外效果和逻辑更新
            player.jumpBoost = true;
            player.jumpSpeedBoost += 2f;
            player.lifeRegen += 50;//生命回复+50
            player.statLifeMax2 += 500;//最大生命+500，注意，是lifemax2，lifemax是存档生命上限（吃生命水晶的那种）
            player.statManaMax2 += 150;//同理魔法值也是如此
            player.statDefense += 50;//防御力+50
            player.GetDamage(DamageClass.Generic) += 1f;//攻击力倍率可以加算也可以乘算，但是乘算容易数值膨胀
            player.GetCritChance(DamageClass.Generic) += 1.5f;//暴击率同理
            player.maxMinions += 10;//召唤上限+10
            player.endurance += 0.2f;//伤害减免+90%
            player.GetAttackSpeed(DamageClass.Melee) += 2f;//近战攻速+20% 
            //player.extraAccessorySlots += 2;
            if (player.statLife < player.statLifeMax2 * 0.5f)
            {
                player.moveSpeed += 0.3f; // 当玩家血量低于50%时增加移速
            }
            else
            {
                player.moveSpeed += 0.1f;
            }
        }
    }
}