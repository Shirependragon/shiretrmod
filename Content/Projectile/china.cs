using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;


namespace shiretrmod.Content.Projectile
{
    public class china : ModProjectile
    {
        public override void SetDefaults()
        {
            // 设置弹幕的属性
            Projectile.width = 16;//弹幕宽度
            Projectile.height = 16;//弹幕高度
            Projectile.aiStyle = -1; // 使用散射型弹幕的 AI 样式
            // AIType = ProjectileID.Bullet;
            Projectile.friendly = true;//是否友善false时不对敌人造成伤害
            Projectile.hostile = false;//是否敌对true时对玩家造成伤害
            Projectile.tileCollide = false;//是否穿墙
            Projectile.timeLeft = 1000;//存留时间
            Projectile.alpha = 0;//弹幕透明度
            Projectile.light = 1f;//弹幕发光
            Projectile.scale = 0.01f;//弹幕大小
            Projectile.DamageType = DamageClass.MeleeNoSpeed;//伤害类型
            Projectile.penetrate = 1;//穿透数量
            Projectile.usesLocalNPCImmunity = false;//是否独立无敌帧
            Projectile.ignoreWater = false;//是否水中减速
            Projectile.extraUpdates = 1;//额外刷新
            Projectile.localNPCHitCooldown = 1;//独立无敌帧
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.townNPC)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = -Projectile.velocity;
            return false;
        }


        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            NPC targetNPC = null;
            float minDistance = float.MaxValue;

            // 寻找最近的敌对NPC
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.CanBeChasedBy() && npc.lifeMax > 5 && npc.life > 0 && npc.friendly == false)
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetNPC = npc;
                    }
                }
            }

            if (targetNPC != null)
            {
                // 使用正交惯性追击最近的敌对NPC
                Vector2 targetPos = targetNPC.Center;
                float MaxSpeed = 20f; // 横纵向最大速度
                float accSpeed = 0.5f; // 横纵向加速度

                if (Projectile.Center.X - targetPos.X < 0f)
                    Projectile.velocity.X += Projectile.velocity.X < 0 ? 2 * accSpeed : accSpeed;
                else
                    Projectile.velocity.X -= Projectile.velocity.X > 0 ? 2 * accSpeed : accSpeed;

                if (Projectile.Center.Y - targetPos.Y < 0f)
                    Projectile.velocity.Y += Projectile.velocity.Y < 0 ? 2 * accSpeed : accSpeed;
                else
                    Projectile.velocity.Y -= Projectile.velocity.Y > 0 ? 2 * accSpeed : accSpeed;

                if (Math.Abs(Projectile.velocity.X) > MaxSpeed)
                    Projectile.velocity.X = MaxSpeed * Math.Sign(Projectile.velocity.X);

                if (Math.Abs(Projectile.velocity.Y) > MaxSpeed)
                    Projectile.velocity.Y = MaxSpeed * Math.Sign(Projectile.velocity.Y);
            }
            else
            {
                // 没有检测到敌人时，围绕玩家转动
                float rotationSpeed = 0.05f;
                Projectile.velocity = Projectile.velocity.RotatedBy(rotationSpeed);
                Projectile.position = player.position + Projectile.velocity * 0.1f * (300f - Projectile.timeLeft);
            }

            // 产生粒子效果
            for (int i = 0; i < 5; i++)
            {
                float alpha = 0.5f - (float)i * 0.1f; // 设置透明度递减
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].position = Projectile.Center;
                Main.dust[dustIndex].velocity = Vector2.Zero;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale = 1.5f; // 设置粒子尺寸
                Main.dust[dustIndex].fadeIn = 1f; // 淡入效果
                Main.dust[dustIndex].noLight = true; // 禁用粒子光照
                Main.dust[dustIndex].noLightEmittence = true; // 禁用粒子发光
            }
        }





        //public override void OnSpawn(IEntitySource source)
        //{
        //    Dust.NewDust(Projectile.position, 16, 16, DustID.RainbowMk2, 0, 0, 0, default, 2f);
        //}
    }

}