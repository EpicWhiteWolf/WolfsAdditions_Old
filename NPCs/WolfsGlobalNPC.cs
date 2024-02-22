using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using WolfsAdditions.Buffs;
using WolfsAdditions.Items.Accessories;
using WolfsAdditions.Items.Weapons.Melee;
using WolfsAdditions.Projectiles;

namespace WolfsAdditions.NPCs;

public class WolfsGlobalNPC : GlobalNPC
{
	public bool boneBolt;

	public bool lacerations;

	public bool marked;

	public bool deepChill;

	public bool frosted;

	public bool wolfsArmor;

	public override bool InstancePerEntity => true;

	public override void ResetEffects(NPC npc)
	{
		boneBolt = false;
		lacerations = false;
		marked = false;
		deepChill = false;
		frosted = false;
		wolfsArmor = false;
	}

	public override void SetDefaults(NPC npc)
	{
		npc.buffImmune[ModContent.BuffType<BoneBoltDebuff>()] = npc.buffImmune[169];
		npc.buffImmune[ModContent.BuffType<MarkedDebuff>()] = npc.buffImmune[169];
	}

	public override void UpdateLifeRegen(NPC npc, ref int damage)
	{
		if (boneBolt)
		{
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			int boneBoltCount = 0;
			for (int i = 0; i < 1000; i++)
			{
				Projectile p = Main.projectile[i];
				if (((Entity)p).active && p.type == ModContent.ProjectileType<BoneBoltProjectile>() && p.ai[0] == 1f && p.ai[1] == (float)((Entity)npc).whoAmI)
				{
					boneBoltCount++;
				}
			}
			npc.lifeRegen -= boneBoltCount * 2 * 3;
			if (damage < boneBoltCount * 3)
			{
				damage = boneBoltCount * 3;
			}
		}
		if (lacerations)
		{
			if (npc.lifeRegen > 0)
			{
				npc.lifeRegen = 0;
			}
			npc.lifeRegen -= 100;
			if (damage < 5)
			{
				damage = 5;
			}
		}
	}
	/*
	public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
	{
		if (npc.type == 4 && !Main.expertMode && !Main.masterMode)
		{
			((NPCLoot)(npcLoot)).Add(ItemDropRule.Common(ModContent.ItemType<BladeOfCthulhu>(), 1, 1, 1));
		}
		if ((npc.type == 125 || npc.type == 126) && !Main.expertMode && !Main.masterMode)
		{
			LeadingConditionRule dropHordeCharm = new LeadingConditionRule((IItemDropRuleCondition)new MissingTwin());
			Chains.OnSuccess((IItemDropRule)(object)dropHordeCharm, ItemDropRule.Common(ModContent.ItemType<HordeCharm>(), 1, 1, 1), false);
			((NPCLoot)(npcLoot)).Add((IItemDropRule)(object)dropHordeCharm);
		}
		if (npc.type != 85)
		{
			return;
		}
		foreach (IItemDropRule item in ((NPCLoot)(npcLoot)).Get(true))
		{
			DropBasedOnExpertMode dropBasedOnExpertMode = (DropBasedOnExpertMode)(object)((item is DropBasedOnExpertMode) ? item : null);
			if (dropBasedOnExpertMode != null)
			{
				IItemDropRule ruleForNormalMode = dropBasedOnExpertMode.ruleForNormalMode;
				OneFromOptionsNotScaledWithLuckDropRule oneFromOptionsDrop = (OneFromOptionsNotScaledWithLuckDropRule)(object)((ruleForNormalMode is OneFromOptionsNotScaledWithLuckDropRule) ? ruleForNormalMode : null);
				if (oneFromOptionsDrop != null && oneFromOptionsDrop.dropIds.Contains(554))
				{
					List<int> original = oneFromOptionsDrop.dropIds.ToList();
					original.Add(ModContent.ItemType<HolyGrail>());
					oneFromOptionsDrop.dropIds = original.ToArray();
				}
			}
		}
	}*/

	public override void DrawEffects(NPC npc, ref Color drawColor)
	{
		if (marked)
		{
			drawColor = new Color(1f, 0.3f, 0.3f, 1f);
		}
		if (deepChill)
		{
			drawColor = new Color(0.33f, 0.66f, 1f, 1f);
		}
		if (frosted)
		{
			drawColor = new Color(1f, 1f, 1f, 1f);
		}
	}

	public override void AI(NPC npc)
	{
		if (deepChill)
		{
			if (npc.aiStyle == 1 || npc.aiStyle == 3 || npc.aiStyle == 15 || npc.aiStyle == 19 || npc.aiStyle == 25 || npc.aiStyle == 26 || npc.aiStyle == 38)
			{
				((Entity)npc).velocity.X *= 0.5f;
			}
			else
			{
				((Entity)npc).velocity = ((Entity)npc).velocity * 0.5f;
			}
		}
		if (frosted)
		{
			if (npc.aiStyle == 1 || npc.aiStyle == 3 || npc.aiStyle == 15 || npc.aiStyle == 19 || npc.aiStyle == 25 || npc.aiStyle == 26 || npc.aiStyle == 38)
			{
				((Entity)npc).velocity.X *= 0.9f;
			}
			else
			{
				((Entity)npc).velocity = ((Entity)npc).velocity * 0.9f;
			}
		}
	}
}
