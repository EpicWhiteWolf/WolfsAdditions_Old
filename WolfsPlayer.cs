using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ModLoader;
using WolfsAdditions.Buffs;
using WolfsAdditions.Items.Accessories;
using WolfsAdditions.Projectiles.Turret;
using static Terraria.IL_Player.HurtModifiers;

namespace WolfsAdditions;

internal class WolfsPlayer : ModPlayer
{
	public bool amgAccessoryPrevious;

	public bool amgAccessory;

	public bool amgHideVanity;

	public bool amgForceVanity;

	public bool amgVisual;

	public bool accHolyGrail;

	public bool wolfsArmorSet;

	public bool furiaSet;

	public bool furiaWings;

	public bool proj1;

	public bool proj2;

	public bool proj3;

	public bool sylSig;

	public int kindleSoul;

	public int fuck;

	public int wolfImmune;

	public float minHP;

	public float curHP;

	public float maxHP;

	public float crtHP;

	public int testOP;

	public float endur;

	public int testY;

	public int testX;

	public float minigun = 30f;

	public bool turretActive;

	public int turretCap = 1;

	public int turretCount;

	public bool overClock;

	public bool auxRemote;

	public bool manaReset;

	private int i;

	public override void ResetEffects()
	{
		if (Player.dashDelay <= 0)
		{
			proj1 = false;
			proj2 = false;
			proj3 = false;
		}
		furiaSet = false;
		furiaWings = false;
		wolfsArmorSet = false;
		accHolyGrail = false;
		sylSig = false;
		overClock = false;
		auxRemote = false;
		amgAccessoryPrevious = amgAccessory;
		amgAccessory = (amgHideVanity = (amgForceVanity = (amgVisual = false)));
	}

	public override void UpdateVisibleVanityAccessories()
	{
		for (int i = 13; i < 18 + Player.GetAmountOfExtraAccessorySlotsToShow(); i++)
		{
			if (Player.armor[i].type == ModContent.ItemType<Items.Accessories.ArchitectsMiningGear>())
			{
				amgHideVanity = false;
				amgForceVanity = true;
			}
		}
	}

	public override void PreUpdate()
	{
		if (auxRemote)
		{
			turretCap = 2;
		}
		if (Player.whoAmI == Main.myPlayer)
		{
			turretCount = Player.ownedProjectileCounts[ModContent.ProjectileType<BasicTurretProjectile>()] + Player.ownedProjectileCounts[ModContent.ProjectileType<SimpleTurretProjectile>()];
			if (turretCount > turretCap)
			{
				turretActive = true;
			}
			if (turretCount <= turretCap)
			{
				turretActive = false;
			}
		}
		if (Player.whoAmI != Main.myPlayer)
		{
			return;
		}
		if (Player.dead && minigun != 30f)
		{
			minigun = 30f;
		}
		if (!PlayerInput.Triggers.Current.MouseLeft || (Player.dead && minigun != 30f))
		{
			if (minigun > 30f)
			{
				minigun = 30f;
			}
			if (minigun < 30f)
			{
				minigun *= 1.01f;
			}
		}
		if (Player.HeldItem.type == Mod.Find<ModItem>("Minigun").Type && PlayerInput.Triggers.Current.MouseLeft && !Player.dead && minigun != 3f)
		{
			if (minigun < 3f)
			{
				minigun = 3f;
			}
			else
			{
				minigun *= 0.99f;
			}
		}
	}

	public override void UpdateEquips()
	{
		if (amgAccessory)
		{
			Player.AddBuff(ModContent.BuffType<Buffs.ArchitectsMiningGear>(), 60, true, false);
			Lighting.AddLight(Player.Center, 1f, 1f, 1f);
		}
		if (accHolyGrail)
		{
			crtHP = Player.statLifeMax2 * 0.15f;
			curHP = Player.statLife - crtHP;
			maxHP = Player.statLifeMax2 - crtHP;
			if (curHP <= 0f)
			{
				minHP = 0f;
			}
			else
			{
				minHP = curHP;
			}
			Player player = Player;
			player.endurance += 0.28f * (1f - minHP / maxHP);
			Lighting.AddLight(Player.Center, 1f, 1f, 0.75f);
		}
	}

	public override void UpdateLifeRegen()
	{
		if (kindleSoul > 0)
		{
			Player player = Player;
			player.statLife += 100;
			kindleSoul--;
		}
	}

	public void SpawnFuriaOrb()
	{
		Vector2 negaVel = default(Vector2);
		if (Player.velocity.X <= 0f)
		{
			int projVel = 7;
			negaVel = new Vector2((float)projVel, 0f);
			Projectile.NewProjectile(Projectile.InheritSource(Player), Player.Center, negaVel, Mod.Find<ModProjectile>("FuriaProjectile").Type, 20, 0f, Main.myPlayer, 0f, 0f, 0f);
		}
		if (Player.velocity.X >= -1f)
		{
			int projVel = -7;
			negaVel = new Vector2((float)projVel, 0f);
			Projectile.NewProjectile(Projectile.InheritSource(Player), Player.Center, negaVel, Mod.Find<ModProjectile>("FuriaProjectile").Type, 20, 0f, Main.myPlayer, 0f, 0f, 0f);
		}
	}

	public override void PostUpdateEquips()
	{
		if (wolfsArmorSet && !manaReset)
		{
			Player.statMana = 0;
			manaReset = true;
		}
		if (!wolfsArmorSet)
		{
			manaReset = false;
		}
		if (sylSig)
		{
			Player player = Player;
			player.maxMinions *= 2;
		}
		if (wolfsArmorSet)
		{
			wolfImmune--;
			if (wolfImmune <= 0)
			{
				wolfImmune = 0;
			}
		}
		if (Player.dashDelay > 0 && furiaWings)
		{
			if (Player.dashDelay == 30 && !proj1)
			{
				proj1 = true;
				SpawnFuriaOrb();
			}
			if (Player.dashDelay == 25 && !proj2)
			{
				proj2 = true;
				SpawnFuriaOrb();
			}
			if (Player.dashDelay == 20 && !proj3)
			{
				proj3 = true;
				SpawnFuriaOrb();
			}
		}
	}

	public override void FrameEffects()
	{
		if ((amgVisual || amgForceVanity) && !amgHideVanity)
		{
			Items.Accessories.ArchitectsMiningGear AMG = ModContent.GetInstance<Items.Accessories.ArchitectsMiningGear>();
			Player.legs = EquipLoader.GetEquipSlot(Mod, ((ModType)AMG).Name, (EquipType)2);
			Player.body = EquipLoader.GetEquipSlot(Mod, ((ModType)AMG).Name, (EquipType)1);
			Player.head = EquipLoader.GetEquipSlot(Mod, ((ModType)AMG).Name, (EquipType)0);
			Player.back = (sbyte)EquipLoader.GetEquipSlot(Mod, ((ModType)AMG).Name, (EquipType)5);
		}
		if (wolfsArmorSet)
		{
			Vector2 playerCenter = Player.MountedCenter + new Vector2(-4f, -4f);
			Vector2 playerArea = default(Vector2);
			playerArea = new Vector2((float)Main.rand.Next(-10, 10), (float)Main.rand.Next(-22, 22));
			if (Utils.NextBool(Main.rand, 50))
			{
				Dust.NewDust(playerCenter + playerArea, 0, 0, Mod.Find<ModDust>("WolfSetDust").Type, Player.velocity.X, Player.velocity.Y, 0, default(Color), 1f);
			}
		}
	}

	public override bool FreeDodge(Player.HurtInfo info)
	{
		if (wolfsArmorSet)
		{
			Player.magicCuffs = false;
			Player.manaRegenDelay = 900f;
			if (wolfImmune > 0)
			{
				return true;
			}
			if (Player.statMana >= info.Damage && wolfImmune <= 0)
			{
				Player player = Player;
				player.statMana -= info.Damage;
				int wolfImmuneTime = 30;
				if (Player.longInvince)
				{
					wolfImmuneTime = 60;
				}
				Player.immune = true;
				Player.immuneTime = wolfImmuneTime;
				wolfImmune = wolfImmuneTime;
				SoundStyle val = new SoundStyle("WolfsAdditions/Sounds/ManaShieldHit");
				SoundEngine.PlaySound(val, Player.Center);
				for (int i = 0; i < 5; i++)
				{
					int num3 = Dust.NewDust(Player.position, Player.width, Player.height, 45, 0f, 0f, 255, default(Color), (float)Main.rand.Next(20, 26) * 0.1f);
					Main.dust[num3].noLight = true;
					Main.dust[num3].noGravity = true;
					Dust obj = Main.dust[num3];
					obj.velocity *= 0.5f;
				}
				return true;
			}
		}
		return false;
	}
	/*
	public override void ModifyHurt(ref Player.HurtModifiers modifiers)
	{
		if (!wolfsArmorSet)
		{
			return;
        }
        ((HurtModifiers)(ref modifiers)).ModifyHurtInfo += (HurtInfoModifier)delegate (ref HurtInfo info)
        {
            if (((ModPlayer)this).Player.statMana < ((HurtInfo)(ref info)).Damage)
            {
                ((HurtInfo)(ref info)).Damage = ((HurtInfo)(ref info)).Damage - ((ModPlayer)this).Player.statMana;
                ((ModPlayer)this).Player.statMana = 0;
            }
        };
    }*/

	public void TestEndurance()
	{
		testOP++;
		if (testOP == 20)
		{
			testOP = 0;
			endur = Player.endurance * 100f;
			Main.NewText($"{(int)endur}", byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
	}
}
