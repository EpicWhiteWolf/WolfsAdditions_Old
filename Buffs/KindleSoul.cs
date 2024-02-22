using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Buffs;

internal class KindleSoul : ModBuff
{
	public override void SetStaticDefaults()
	{
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetModPlayer<WolfsPlayer>().kindleSoul = 1;
	}
}
