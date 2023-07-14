using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Content.Projectile
{
    public class 黑洞 : ModProjectile
    {
        private float decelerationRate = 0.02f; // 速度递减率
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1; // 不使用默认的 AI 样式
            Projectile.penetrate = -1;//穿透数量
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 600; // 弹幕存在时间
        }

        public override void AI()
        {
            // 控制黑洞的移动
            float speed = Projectile.velocity.Length();
            if (speed > 0)
            {
                Projectile.velocity *= 1f - decelerationRate; // 速度递减
            }
            else
            {
                Projectile.velocity = Vector2.Zero; // 停止移动
            }

            // 检测并吸引附近的敌人
            int range = 500; // 吸引范围
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Projectile.Center) < range)
                {
                    Vector2 direction = npc.Center - Projectile.Center;
                    direction.Normalize();
                    float attractionStrength = 5f - (npc.Distance(Projectile.Center) / range); // 吸引力量随距离增加而减弱
                    npc.velocity -= direction * (attractionStrength * 0.12f); // 吸引敌人

                    // 在一定范围内杀死敌人
                    if (npc.Distance(Projectile.Center) < 32)
                    {
                        NPC.HitInfo hitInfo = new NPC.HitInfo();
                        hitInfo.Damage = 10; // 设置伤害值为50
                        hitInfo.Knockback = 0; // 设置击退量为6
                        hitInfo.Crit = true; // 设置为暴击
                        npc.StrikeNPC(hitInfo, false, false);
                    }
                }
            }
            if (Main.rand.NextBool(5))
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, 0f, 0f, 100, Color.Black, 2f);
                Main.dust[dust].noGravity = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            Terraria.Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, (Projectile.Center).SafeNormalize(Vector2.Zero), ModContent.ProjectileType<黑洞爆炸>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }

    public class 黑洞爆炸 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1; // 不使用默认的 AI 样式
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;//穿透数量
            Projectile.timeLeft = 3; // 爆炸效果存在时间
            Projectile.alpha = 255; // 初始透明度
            Projectile.scale = 2f; // 初始大小
        }

        public override void AI()
        {
            int range = 1200;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Projectile.Center) < range)
                {
                    Vector2 direction = npc.Center - Projectile.Center;
                    direction.Normalize();
                    float attractionStrength = 10f - (npc.Distance(Projectile.Center) / range); // 吸引力量随距离增加而减弱
                    npc.velocity -= direction * (attractionStrength * 0.25f); // 吸引敌人
                }
            }
            // 控制爆炸效果的透明度和大小
            Projectile.alpha -= 5; // 透明度渐变
            Projectile.scale += 3f; // 大小缩小

            // 在爆炸范围内伤害敌人
            int explosionRange = 100; // 爆炸范围
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && !npc.dontTakeDamage && npc.Distance(Projectile.Center) < explosionRange)
                {
                    NPC.HitInfo hitInfo = new NPC.HitInfo();
                    hitInfo.Damage = 1000; // 设置伤害值为50
                    hitInfo.Knockback = 0; // 设置击退量为6
                    hitInfo.Crit = true; // 设置为暴击
                    npc.StrikeNPC(hitInfo, false, false);
                }
            }
        }
    }
}
