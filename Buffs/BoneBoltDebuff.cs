using Terraria;
using Terraria.ModLoader;
using WolfsAdditions.NPCs;

namespace WolfsAdditions.Buffs;

internal class BoneBoltDebuff : ModBuff
{
	public override void SetStaticDefaults()
	{
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.GetGlobalNPC<WolfsGlobalNPC>().boneBolt = true;
	}
}
