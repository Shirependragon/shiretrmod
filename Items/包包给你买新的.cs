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
    public class 包包给你买新的 : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 60; // 设置法杖的伤害值
            Item.DamageType = MagicDamageClass.Magic; // 标记法杖为魔法武器
            Item.mana = 5; // 每次使用消耗的法力值
            Item.width = 40; // 设置法杖的宽度
            Item.height = 40; // 设置法杖的高度
            Item.useTime = 5; // 设置法杖使用的时间（以帧为单位）
            Item.useAnimation = 5; // 设置法杖使用时的动画时间（以帧为单位）
            Item.useStyle = ItemUseStyleID.Swing; // 设置法杖的使用方式
            Item.noMelee = true; // 禁止法杖进行近战攻击
            Item.knockBack = 2f; // 设置法杖的击退力量
            Item.value = Item.sellPrice(silver: 10); // 设置法杖的售价
            Item.rare = ItemRarityID.White; // 设置法杖的稀有度
            Item.UseSound = SoundID.Item20; // 设置法杖的使用声音
            Item.autoReuse = true; // 设置法杖是否自动重复使用
            Item.shootSpeed = 10f; // 设置法杖发射的弹幕速度
            Item.mana = 5; // 每次使用消耗的法力值
            Item.useTurn = true; // 设置法杖使用时是否可以转向
            Item.UseSound = SoundID.Item8; // 设置法杖的使用声音
            Item.staff[Item.type] = true;//让本武器为法杖类型（因为贴图是45度，如果不这么做发射时就会倾斜45度）
            Item.shoot = ModContent.ProjectileType<衣服给你买新的>(); // 设置法杖发射的弹幕类型
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //本函数用于在武器执行发射弹幕时的操作，返回false可阻止武器原本的发射。true则保留。
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity.RotatedBy(+2), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity.RotatedBy(-2), type, damage, knockback, player.whoAmI);
            //这里我额外生成两个散射剑气,注意rotatedby是将向量偏转指定弧度，（6.28也就是2PI为一圈）
            //生成一个弹幕，source是生成源，直接使用参数即可。第二个参数是生成位置，position在玩家处。
            //第三个参数是速度，决定弹幕的初始速度（二维向量），第四个参数是ID，第五个参数是伤害
            //第七个参数是弹幕所有者的索引，通常有player参数时直接填player.whoami，不填这个参数可能会引发错误。
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            Lighting.AddLight(player.Center, new Vector3(1f, 1f, 1f)); // 设置光照颜色
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * 0.8f; // 设置物品颜色和透明度
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 spawnPosition = target.Center; // 获取击中的NPC中心位置
            int projectileType = ModContent.ProjectileType<衣服给你买新的>(); // 替换为你想生成的弹幕类型

            // 生成新的弹幕
            Projectile.NewProjectile((IEntitySource)player, spawnPosition.X, spawnPosition.Y, 0f, 0f, projectileType, 0, 0f, Main.myPlayer);

        }
        //public override void HoldItem(Player player)
        //{
        //    float time = (float)Main.time / 30f; // 根据游戏时间来计算时间参数
        //    float speedFactor = 1f; // 控制波浪的速度
        //    float speedRange = 20f; // 控制速度范围
        //    float speedOffset = (float)Math.Sin(time * speedFactor) * speedRange;
        //    Item.useTime += (int)speedOffset;
        //    Item.useAnimation += (int)speedOffset;
        //}
    }
}