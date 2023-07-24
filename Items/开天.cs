using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using shiretrmod.Content.Projectile;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using System;

namespace shiretrmod.Items
{
    public class 开天 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 1;
            Item.scale = 1.8f;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<开天Projectile>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 检查玩家是否已经拥有弹幕
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.owner == player.whoAmI && proj.type == ModContent.ProjectileType<开天Projectile>())
                {
                    // 如果玩家已经拥有弹幕，则刷新此弹幕的存在时间，并不发射新的弹幕
                    proj.timeLeft = 300;
                    return false;
                }
            }
            
            return true;
        }
        public override void HoldItem(Player player)
        {
            // 定义循环的参数
            float time = (float)Main.time / 30f; // 根据游戏时间来计算时间参数
            float speedFactor = 1f; // 控制循环的速度
            float damageRange = 4f; // 控制伤害范围
            float speedRange = 3f; // 控制速度范围
            //float scaleRange = 0.5f;

            // 根据循环参数计算伤害和速度的偏移值
            float damageOffset = (float)Math.Sin(time * speedFactor) * damageRange;
            float speedOffset = (float)Math.Sin(time * speedFactor) * speedRange;
            //float scaleOffset = (float)Math.Sin(time * speedFactor) * scaleRange;

            // 增加武器的伤害和挥动速度
            Item.damage += (int)damageOffset;
            Item.useTime -= (int)speedOffset;
            Item.useAnimation -= (int)speedOffset;
            //Item.scale += (int)scaleOffset;

            // 防止武器的使用时间和挥动速度小于1
            if (Item.useTime < 1)
                Item.useTime = 1;
            if (Item.useAnimation < 1)
                Item.useAnimation = 1;
            if (Item.damage < 20)
                Item.damage = 20;
            if (Item.damage > 500)
                Item.damage = 500;
            //if (Item.scale > 5f)
            //    Item.scale = 5f;
            //if (Item.scale < 2.8f)
            //    Item.scale = 2.8f;
        }
        //public override void HoldItem(Player player)
        //{
        //    //Lighting.AddLight(player.position,10f,10f,10f);
        //    Vector2 playerPos = player.Center;
        //    float sword_center_x = playerPos.X;
        //    float sword_center_y = playerPos.Y; // 向上偏移100个单位

        // 根据玩家的血量调整半径
        //    float radius = 50 + (1f - player.statLife / (float)player.statLifeMax2) * 200f;

        //    float rotationAngle = 0f; // 初始旋转角度为0

        //    for (int i = 0; i < 360; i++)
        //    {
        //        float angle = MathHelper.ToRadians(i) + rotationAngle; // 加上旋转角度
        //        float x = sword_center_x + radius * (float)Math.Cos(angle);
        //        float y = sword_center_y + radius * (float)Math.Sin(angle);
        //        Dust dust = Dust.NewDustPerfect(new Vector2(x, y), 6);
        //        dust.noGravity = true;
        //        dust.scale = 1.5f;

        //        float y2 = sword_center_y - radius * (float)Math.Sin(angle); // 新的y2坐标
        //        Dust dust2 = Dust.NewDustPerfect(new Vector2(x, y2), 6); // 新的dust2
        //        dust2.noGravity = true;
        //        dust2.scale = 1.5f;

        // 添加第一个图案函数
        //        float patternRadius1 = radius / 2f; // 第一个图案的半径
        //        float patternX1 = sword_center_x + patternRadius1 * (float)Math.Cos(angle * 5f);
        //        float patternY1 = sword_center_y + patternRadius1 * (float)Math.Sin(angle * 5f);
        //        Dust patternDust1 = Dust.NewDustPerfect(new Vector2(patternX1, patternY1), 6);
        //        patternDust1.noGravity = true;
        //        patternDust1.scale = 1.0f;

        // 添加第二个图案函数
        //        float patternRadius2 = radius / 3f; // 第二个图案的半径
        //        float patternX2 = sword_center_x + patternRadius2 * (float)Math.Cos(angle * 10f);
        //        float patternY2 = sword_center_y + patternRadius2 * (float)Math.Sin(angle * 10f);
        //        Dust patternDust2 = Dust.NewDustPerfect(new Vector2(patternX2, patternY2), 6);
        //        patternDust2.noGravity = true;
        //        patternDust2.scale = 0.8f;
        //        rotationAngle += 0.01f; // 增加旋转角度的步进值
        //    }
        //}
        public override void UpdateInventory(Player player)
        {
            Lighting.AddLight(player.Center, new Vector3(0.5f, 0.5f, 0.5f)); // 设置光照颜色
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Cyan * 0.8f; // 设置物品颜色和透明度
        }
    }
}
