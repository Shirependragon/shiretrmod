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
    public class 滋滋滋 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60; // 设置法杖的伤害值
            Item.DamageType = DamageClass.Ranged; // 标记法杖为魔法武器
            Item.width = 40; // 设置法杖的宽度
            Item.height = 40; // 设置法杖的高度
            Item.useTime = 3; // 设置法杖使用的时间（以帧为单位）
            Item.useAnimation = 3; // 设置法杖使用时的动画时间（以帧为单位）
            Item.useStyle = ItemUseStyleID.Shoot; // 设置法杖的使用方式
            Item.noMelee = true; // 禁止法杖进行近战攻击
            Item.knockBack = 2f; // 设置法杖的击退力量
            Item.rare = ItemRarityID.Pink; // 设置法杖的稀有度
            Item.UseSound = SoundID.Item20; // 设置法杖的使用声音
            Item.autoReuse = true; // 设置法杖是否自动重复使用
            Item.shootSpeed = 10f; // 设置法杖发射的弹幕速度
            Item.useAmmo = AmmoID.Bullet;
            Item.useTurn = true; // 设置法杖使用时是否可以转向
            Item.shoot = ModContent.ProjectileType<崩崩崩>(); // 设置法杖发射的弹幕类型
            Item.channel = true;
            Item.scale = 2f;
            Item.UseSound = SoundID.Item15;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            //本函数为近战攻击到NPC时进行的操作，通常为产生弹幕或者BUFF
            target.AddBuff(BuffID.OnFire, 120);//addbuff方法第一个参数为要上的BUFFID，第二个为持续时间（帧）
            player.AddBuff(BuffID.NebulaUpLife3, 30);//为玩家添加半秒星云回复BUFF
                                                     //远程武器就不需要了
            base.OnHitNPC(player, target, hit, damageDone);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<崩崩崩>(), damage, knockback, player.whoAmI);
            return false;
        }
    }
}
