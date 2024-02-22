using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles.Turret;

internal class SimpleTurretHead : ModProjectile
{
	private int turretAttack;

	public int fireRate;

	private int slow;

	private int ammoCount;

	private bool deployed;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		Main.projFrames[Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 14;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		DrawOffsetX = 0;
		DrawOriginOffsetY = 0;
		Projectile.aiStyle = 0;
		Projectile.hide = true;
		Projectile.timeLeft = 3600;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}

	public override bool? CanHitNPC(NPC target)
	{
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void AI()
	{
		WolfsPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<WolfsPlayer>();
		if (modPlayer.overClock)
		{
			turretAttack = 15;
		}
		if (!modPlayer.overClock)
		{
			turretAttack = 25;
		}
		if (!deployed)
		{
			ammoCount = 100;
			deployed = true;
		}
		Vector2 direction = Vector2.Zero;
		float range = 600f;
		bool targetLock = false;
		NPC target = null;
		for (int j = 0; j < 200; j++)
		{
			if (((Entity)Main.npc[j]).active && !Main.npc[j].dontTakeDamage && !Main.npc[j].friendly && Main.npc[j].lifeMax > 5)
			{
				target = Main.npc[j];
				Vector2 newDirection = ((Entity)Main.npc[j]).Center - ((Entity)Projectile).Center;
				if ((float)Math.Sqrt(newDirection.X * newDirection.X + newDirection.Y * newDirection.Y) < range && Collision.CanHitLine(((Entity)Projectile).Center + new Vector2(0f, -6f), 0, 0, ((Entity)target).Center + new Vector2(0f, -6f), 0, 0))
				{
					direction = newDirection;
					targetLock = true;
				}
			}
		}
		Projectile parent = Main.projectile[(int)Projectile.ai[0]];
		((Entity)Projectile).Center = ((Entity)parent).Center + new Vector2(0f, -2f);
		if (targetLock)
		{
			Projectile.rotation = MathF.Atan2(direction.Y, direction.X);
			slow++;
			if (slow == 8)
			{
				Projectile.frame = 0;
				slow = 0;
			}
			fireRate++;
			if (ammoCount > 0 && fireRate >= turretAttack)
			{
				fireRate = 0;
				ammoCount--;
				SoundEngine.PlaySound(SoundID.Item11, (Vector2?)((Entity)Projectile).Center, (SoundUpdateCallback)null);
				Projectile.frame = 1;
				direction.Normalize();
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).Center.X, ((Entity)Projectile).Center.Y - 4f), direction * 10f, 14, Projectile.damage, 2f, Main.myPlayer, 0f, 0f, 0f);
			}
		}
		if (!targetLock)
		{
			int num = (int)Projectile.ai[1];
			if (num == 1)
			{
				Projectile.rotation = MathHelper.ToRadians(0f);
			}
			if (num == -1)
			{
				Projectile.rotation = MathHelper.ToRadians(180f);
			}
		}
		if (ammoCount <= 0 && deployed)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust.NewDust(((Entity)Projectile).Center, 5, 5, 31, Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f), 0, default(Color), 1f);
			}
			SoundEngine.PlaySound(SoundID.Item94, (Vector2?)((Entity)Projectile).Center, (SoundUpdateCallback)null);
			Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		if (Main.netMode != 1)
		{
			Main.projectile[(int)Projectile.ai[0]].Kill();
		}
	}
}
