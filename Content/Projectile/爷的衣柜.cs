using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;

namespace shiretrmod.Content.Projectile
{
    public class 爷的衣柜 : ModProjectile
    {
        Vector2 mousePos;
        public override void SetDefaults()
        {
            Projectile.width = 16;//弹幕宽度
            Projectile.height = 16;//弹幕高度
            // Projectile.aiStyle = -1; // 使用散射型弹幕的 AI 样式
            // AIType = ProjectileID.Bullet;
            Projectile.velocity = new Vector2(0, 0);
            Projectile.friendly = true;//是否友善,false时不对敌人造成伤害
            Projectile.hostile = false;//是否敌对,true时对玩家造成伤害
            Projectile.tileCollide = false;//是否穿墙
            Projectile.timeLeft = 600;//存留时间
            Projectile.alpha = 1;//弹幕透明度
            Projectile.light = 1f;//弹幕发光
            Projectile.scale = 1f;//弹幕大小
            Projectile.DamageType = DamageClass.Magic;//伤害类型
            Projectile.penetrate = 1;//穿透数量
            Projectile.usesLocalNPCImmunity = false;//是否独立无敌帧
            Projectile.ignoreWater = true;//是否水中减速
            Projectile.extraUpdates = 1;//额外刷新
            Projectile.localNPCHitCooldown = 1;//独立无敌帧
            mousePos = Main.MouseWorld;
        }
        void DrawStar(Vector2 position, float radius, float timeRatio)
        {
            // 根据投掷物的timeleft属性绘制五角星
            const float tau = MathHelper.TwoPi;
            const float increment = tau / 5f;

            float angle = timeRatio * tau;
            for (int i = 0; i < 5; i++)
            {
                Vector2 offset = new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * radius;
                Dust.NewDustPerfect(position + offset, DustID.Vortex);
                angle += increment;
            }
        }
        private void DrawLine(Vector2 startPos, Vector2 endPos, float percent)
        {
            Vector2 position = startPos * (1f - percent) + endPos * percent;
            Dust.NewDustPerfect(position, DustID.Vortex).noGravity = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 0)
            {
                float percent = ((600 - Projectile.timeLeft) % 180) / 180f;
                // float percent2 = (percent + 0.5f) % 1f;
                Vector2 point1 = mousePos + new Vector2(0, -100);
                Vector2 point2 = mousePos + new Vector2(-(float)95.1, -(float)30.9);
                Vector2 point3 = mousePos + new Vector2((float)95.1, -(float)30.9);
                Vector2 point4 = mousePos + new Vector2(-(float)30.9, (float)95.1);
                Vector2 point5 = mousePos + new Vector2((float)30.9, (float)95.1);
                DrawLine(point2, point3, percent);
                DrawLine(point3, point4, percent);
                DrawLine(point4, point1, percent);
                DrawLine(point1, point5, percent);
                DrawLine(point5, point2, percent);

                // DrawLine(point2, point3, percent2);
                // DrawLine(point3, point4, percent2);
                // DrawLine(point4, point1, percent2);
                // DrawLine(point1, point5, percent2);
                // DrawLine(point5, point2, percent2);

            }

            // if (Projectile.timeLeft > 50)
            // {
            //     Player player = Main.player[Projectile.owner];
            //     float rotationSpeed = 0.05f;

            //     // 控制粒子生成的频率
            //     int particleFrequency = 5; // 调整生成频率，值越大生成的粒子越少

            //     if (Projectile.timeLeft % particleFrequency == 0)
            //     {
            //         // 添加粒子效果
            //         Vector2 offset = new Vector2(0, -20); // 调整粒子位置偏移
            //         Vector2 pos = Projectile.Center + offset;
            //         Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30)); // 随机旋转粒子速度
            //         Dust.NewDustPerfect(pos, 229, velocity); // 使用火焰效果的粒子，你可以根据需要更改粒子类型
            //     }

            //     // 调整速度和位置
            //     Projectile.velocity = Projectile.velocity.RotatedBy(rotationSpeed);
            //     Projectile.position = player.position + Projectile.velocity * 0.1f * (300f - Projectile.timeLeft);
            // }
            // else
            // {
            //     // 使用插值渐进运动追击鼠标
            //     Vector2 targetPos = Main.MouseWorld;
            //     Vector2 pos = Vector2.Lerp(Projectile.Center, targetPos, 0.3f);
            //     Projectile.velocity = pos - Projectile.Center;

            // }

            if (Projectile.timeLeft < 420)
            {
                Projectile.velocity.Y += 0.25f;
                Projectile.velocity.X = 0;
            }
            // 到达鼠标位置前与方块不发生碰撞
            if (mousePos.Y > Projectile.position.Y)
            {
                Projectile.tileCollide = false;
                // if (!WorldGen.TileEmpty((int)Projectile.position.X, (int)Projectile.position.Y))
                // {
                //     WorldGen.KillTile((int)Projectile.position.X, (int)Projectile.position.Y);
                // }
            }
            else
            {
                Projectile.tileCollide = true;
            }
            // 贴图翻转和修正
            if (Projectile.velocity.X > 0)
            {
                Projectile.direction = Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.direction = Projectile.spriteDirection = 1;
                Projectile.rotation += MathHelper.Pi;
            }
            for (int i = 0; i < 5; i++)
            {
                float alpha = 0.5f - (float)i * 0.1f; // 设置透明度递减
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].position = Projectile.Center;
                Main.dust[dustIndex].velocity = Vector2.Zero;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale = 1.5f; // 设置粒子尺寸
                Main.dust[dustIndex].fadeIn = 1f; // 淡入效果
                Main.dust[dustIndex].noLight = false; // 禁用粒子光照
                Main.dust[dustIndex].noLightEmittence = false; // 禁用粒子发光
            }
        }
        private void expload()
        {
            // 对距离内的敌人造成伤害
            foreach (NPC npc in Main.npc)
            {
                Rectangle hitbox = npc.getRect();
                double bias = Math.Sqrt((hitbox.Width / 2) ^ 2 + (hitbox.Height / 2) ^ 2);
                float dist = (npc.position - Projectile.position).Length() - (float)bias;
                if (npc.active && !npc.friendly && dist < 240f)
                {
                    NPC.HitInfo hit = npc.CalculateHitInfo(Projectile.damage, 0, false, 0, DamageClass.Magic);
                    npc.StrikeNPC(hit);
                }
            }
            // 生成爆炸粒子效果
            for (int i = 0; i < 300; i++)
            {
                int dustIndex = Dust.NewDust(Projectile.position, 0, 0, DustID.Vortex);
                Dust dust = Main.dust[dustIndex];
                dust.scale = 3f;
                // 初始速度单位元化
                float length = dust.velocity.Length();
                dust.velocity /= length;
                // 分类化速度
                int randomNumber = Main.rand.Next(3000);
                float vel = randomNumber / 100;
                dust.velocity *= vel;

                dust.noGravity = true;
                dust.fadeIn = 0.75f;
                dust.color = Color.WhiteSmoke;
            }
            // 播放爆炸音效
            SoundEngine.PlaySound(SoundID.Item14);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // 撞击障碍物时
            expload();
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 撞击NPC时爆炸
            expload();
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            // 撞击玩家时爆炸
            expload();
            base.OnHitPlayer(target, info);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // Texture2D texture = (Texture2D)TextureAssets.Projectile[Projectile.type];
            // Rectangle rectangle = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            // Vector2 origin = rectangle.Size() / 2f;

            // Vector2 toPlayer = Main.player[Projectile.owner].Center - Projectile.Center;
            // float rotation;

            // if (Projectile.timeLeft > 0)
            // {
            //     rotation = toPlayer.ToRotation() + MathHelper.PiOver2;
            //     rotation += MathHelper.ToRadians(Main.GameUpdateCount % 360);
            // }
            // else
            // {
            //     Vector2 toMouse = Main.MouseWorld - Projectile.Center;
            //     rotation = toMouse.ToRotation() + MathHelper.PiOver2;

            // }

            // Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, rectangle, lightColor, rotation, origin, 1f, Projectile.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

            Texture2D texture = ModContent.Request<Texture2D>("shiretrmod/Content/Texture/test").Value;
            Vector2 point1 = mousePos + new Vector2(0, -100);
            Vector2 point2 = mousePos + new Vector2(-(float)95.1, -(float)30.9);
            Vector2 point3 = mousePos + new Vector2((float)95.1, -(float)30.9);
            Vector2 point4 = mousePos + new Vector2(-(float)30.9, (float)95.1);
            Vector2 point5 = mousePos + new Vector2((float)30.9, (float)95.1);
            for (int i = 0; i < 10; i++)
            {
                float p = i / 10f;
                Vector2 pos1 = point2 * (1f - p) + point3 * p;
                Vector2 pos2 = point3 * (1f - p) + point4 * p;
                Vector2 pos3 = point4 * (1f - p) + point1 * p;
                Vector2 pos4 = point1 * (1f - p) + point5 * p;
                Vector2 pos5 = point5 * (1f - p) + point2 * p;
                Main.EntitySpriteDraw(texture, pos1, null, default(Color), 0, new Vector2(0, texture.Height / 2), 1, SpriteEffects.None);
                Main.EntitySpriteDraw(texture, pos2, null, default(Color), 0, new Vector2(0, texture.Height / 2), 1, SpriteEffects.None);
                Main.EntitySpriteDraw(texture, pos3, null, default(Color), 0, new Vector2(0, texture.Height / 2), 1, SpriteEffects.None);
                Main.EntitySpriteDraw(texture, pos4, null, default(Color), 0, new Vector2(0, texture.Height / 2), 1, SpriteEffects.None);
                Main.EntitySpriteDraw(texture, pos5, null, default(Color), 0, new Vector2(0, texture.Height / 2), 1, SpriteEffects.None);
            }
            return true;
        }

    }
}
