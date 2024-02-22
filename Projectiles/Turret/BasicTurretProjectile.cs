using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles.Turret;

public class BasicTurretProjectile : ModProjectile
{
	private bool deploying;

	private bool deployed;

	private int ammoCount;

	public int fireRate;

	private bool thrown;

	private int projectileDirection;

	private int slow;

	private int turretAttack;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		Main.projFrames[Projectile.type] = 13;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 22;
		((Entity)Projectile).height = 18;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.penetrate = -1;
		DrawOffsetX = 0;
		DrawOriginOffsetY = 0;
		Projectile.aiStyle = 0;
		Projectile.hide = true;
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
		if (!deploying)
		{
			deploying = true;
			ammoCount = 40;
		}
		((Entity)Projectile).velocity = new Vector2(0f, 0f);
		return false;
	}

	public override void AI()
	{
		WolfsPlayer modPlayer = Main.player[Projectile.owner].GetModPlayer<WolfsPlayer>();
		if (modPlayer.overClock)
		{
			turretAttack = 30;
		}
		if (!modPlayer.overClock)
		{
			turretAttack = 40;
		}
		if (!thrown)
		{
			if (((Entity)Projectile).velocity.X >= 0f)
			{
				projectileDirection = 1;
				Projectile.spriteDirection = 1;
			}
			if (((Entity)Projectile).velocity.X < 0f)
			{
				projectileDirection = -1;
				Projectile.spriteDirection = -1;
			}
			thrown = true;
		}
		if (deploying && !deployed && Projectile.frameCounter < 11)
		{
			slow++;
			if (slow == 2)
			{
				slow = 0;
				Projectile projectile = Projectile;
				projectile.frameCounter++;
				Projectile projectile2 = Projectile;
				projectile2.frame++;
				if (Projectile.frame == 11)
				{
					deployed = true;
				}
			}
		}
		if (deployed)
		{
			slow++;
			if (slow == 8)
			{
				Projectile.frame = 11;
				slow = 0;
			}
			fireRate++;
			if (ammoCount > 0 && fireRate >= turretAttack)
			{
				fireRate = 0;
				ammoCount--;
				SoundEngine.PlaySound(SoundID.Item11, (Vector2?)((Entity)Projectile).Center, (SoundUpdateCallback)null);
				Projectile.frame = 12;
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).Center.X, ((Entity)Projectile).Center.Y - 4f), new Vector2((float)(projectileDirection * 10), 0f), 14, Projectile.damage, 2f, Main.myPlayer, 0f, 0f, 0f);
			}
		}
		Projectile.rotation = 0f;
		((Entity)Projectile).velocity.Y += 0.2f;
		((Entity)Projectile).velocity.X *= 0.99f;
		if (((Entity)Projectile).velocity.Y > 12f)
		{
			((Entity)Projectile).velocity.Y = 12f;
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
}
