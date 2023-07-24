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
    public class 海澜之家 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 3276; // 设置法杖的伤害值
            Item.DamageType = MagicDamageClass.Magic; // 标记法杖为魔法武器
            Item.mana = 5; // 每次使用消耗的法力值
            Item.width = 40; // 设置法杖的宽度
            Item.height = 40; // 设置法杖的高度
            Item.useTime = 60; // 设置法杖使用的时间（以帧为单位）
            Item.useAnimation = 60; // 设置法杖使用时的动画时间（以帧为单位）
            Item.useStyle = ItemUseStyleID.Swing; // 设置法杖的使用方式
            Item.noMelee = true; // 禁止法杖进行近战攻击
            Item.knockBack = 2f; // 设置法杖的击退力量
            Item.value = Item.sellPrice(silver: 10); // 设置法杖的售价
            Item.rare = ItemRarityID.Pink; // 设置法杖的稀有度
            Item.UseSound = SoundID.Item20; // 设置法杖的使用声音
            Item.autoReuse = true; // 设置法杖是否自动重复使用
            Item.shootSpeed = 10f; // 设置法杖发射的弹幕速度
            Item.mana = 120; // 每次使用消耗的法力值
            Item.useTurn = true; // 设置法杖使用时是否可以转向
            Item.UseSound = SoundID.Item8; // 设置法杖的使用声音
            Item.staff[Item.type] = true;//让本武器为法杖类型（因为贴图是45度，如果不这么做发射时就会倾斜45度）
            Item.shoot = ModContent.ProjectileType<爷的衣柜>(); // 设置法杖发射的弹幕类型
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 在鼠标指针的上方生成一个投掷物
            Vector2 pos = new Vector2(Main.MouseWorld.X, player.position.Y - 800);
            Projectile.NewProjectile(source, pos, new Vector2(0, 0), type, damage, knockback, player.whoAmI);
            return false; // 禁用默认射击函数
        }
        public override void UpdateInventory(Player player)
        {
            Lighting.AddLight(player.Center, new Vector3(1f, 1f, 1f)); // 设置光照颜色
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Cyan * 0.8f; // 设置物品颜色和透明度
        }
    }
}