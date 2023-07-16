using Terraria;
using Terraria.ModLoader;

namespace shiretrmod.Content.Buff;
public class DamageReduction : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        player.endurance += 0.2f; // 减免20%伤害
        player.moveSpeed += 1; // 加1倍移速
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = "实质化的信念与你的身体带来了无尽的力量";
        base.ModifyBuffText(ref buffName, ref tip, ref rare);
    }
}