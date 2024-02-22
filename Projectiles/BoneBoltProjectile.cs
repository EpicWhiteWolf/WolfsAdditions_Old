using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WolfsAdditions.Buffs;

namespace WolfsAdditions.Projectiles;

public class BoneBoltProjectile : ModProjectile
{
	private const float maxTicks = 45f;

	public bool isStickingToTarget
	{
		get
		{
			return Projectile.ai[0] == 1f;
		}
		set
		{
			Projectile.ai[0] = (value ? 1f : 0f);
		}
	}

	public float targetWhoAmI
	{
		get
		{
			return Projectile.ai[1];
		}
		set
		{
			Projectile.ai[1] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 10;
		((Entity)Projectile).height = 10;
		Projectile.timeLeft = 960;
		Projectile.penetrate = 3;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = 0;
		Projectile.extraUpdates = 2;
		Projectile.hide = true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Lighting.GetColor((int)((Entity)Projectile).Center.X / 16, (int)((Entity)Projectile).Center.Y / 16);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		if (isStickingToTarget)
		{
			int npcIndex = (int)Projectile.ai[1];
			if (npcIndex >= 0 && npcIndex < 200 && ((Entity)Main.npc[npcIndex]).active)
			{
				if (Main.npc[npcIndex].behindTiles)
				{
					behindNPCsAndTiles.Add(index);
				}
				else
				{
					behindNPCs.Add(index);
				}
				return;
			}
		}
		behindProjectiles.Add(index);
	}

    public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
    {
        width = (height = 10);
        return true;
    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return ((Rectangle)(projHitbox)).Intersects(targetHitbox);
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Dig, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
		Vector2 position = ((Entity)Projectile).position;
		Vector2 rotVector = Utils.ToRotationVector2(Projectile.rotation - MathHelper.ToRadians(90f));
		_ = position + rotVector * 16f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		isStickingToTarget = true;
		targetWhoAmI = ((Entity)target).whoAmI;
		((Entity)Projectile).velocity = (((Entity)target).Center - ((Entity)Projectile).Center) * 0.75f;
		Projectile.netUpdate = true;
		target.AddBuff(ModContent.BuffType<BoneBoltDebuff>(), 960, false);
		Projectile.damage = 0;
		Point[] stickingBolts = (Point[])(object)new Point[15];
		int boltIndex = 0;
		for (int j = 0; j < Main.maxProjectiles; j++)
		{
			Projectile currentProjectile = Main.projectile[j];
			if (j != ((Entity)Projectile).whoAmI && ((Entity)currentProjectile).active && currentProjectile.owner == Main.myPlayer && currentProjectile.type == Projectile.type && currentProjectile.ai[0] == 1f && currentProjectile.ai[1] == (float)((Entity)target).whoAmI)
			{
				stickingBolts[boltIndex++] = new Point(j, currentProjectile.timeLeft);
				if (boltIndex >= stickingBolts.Length)
				{
					break;
				}
			}
		}
		if (boltIndex < stickingBolts.Length)
		{
			return;
		}
		int oldBoltIndex = 0;
		for (int i = 1; i < stickingBolts.Length; i++)
		{
			if (stickingBolts[i].Y < stickingBolts[oldBoltIndex].Y)
			{
				oldBoltIndex = i;
			}
		}
		Main.projectile[stickingBolts[oldBoltIndex].X].Kill();
	}

	public override void AI()
	{
		if (!isStickingToTarget)
		{
			Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		}
		if (!isStickingToTarget)
		{
			return;
		}
		Projectile.ignoreWater = true;
		Projectile.tileCollide = false;
		int aiFactor = 15;
		bool killProj = false;
		bool hitEffect = false;
		Projectile.localAI[0] += 1f;
		hitEffect = Projectile.localAI[0] % 30f == 0f;
		int projTargetIndex = (int)targetWhoAmI;
		if (Projectile.localAI[0] >= (float)(60 * aiFactor) || projTargetIndex < 0 || projTargetIndex >= 200)
		{
			killProj = true;
		}
		else if (((Entity)Main.npc[projTargetIndex]).active && !Main.npc[projTargetIndex].dontTakeDamage)
		{
			((Entity)Projectile).Center = ((Entity)Main.npc[projTargetIndex]).Center - ((Entity)Projectile).velocity * 2f;
			Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
			if (hitEffect)
			{
				Main.npc[projTargetIndex].HitEffect(0, 1.0, (bool?)null);
			}
		}
		else
		{
			killProj = true;
		}
		if (killProj)
		{
			Projectile.Kill();
		}
	}
}
