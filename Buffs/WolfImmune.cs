using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Buffs;

public class WolfImmune : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[Type] = true;
		Main.debuff[Type] = false;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.immune = true;
		player.immuneAlpha = 0;
	}
}
