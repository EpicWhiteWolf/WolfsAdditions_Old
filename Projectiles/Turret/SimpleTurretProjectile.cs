using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles.Turret;

public class SimpleTurretProjectile : ModProjectile
{
	private int projectileDirection;

	private bool thrown;

	private bool deploying;

	private bool deployed;

	public int fireRate;

	private int slow;

	private bool setup;

	public int Parent
	{
		get
		{
			return (int)Projectile.ai[1];
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
		Main.projFrames[Projectile.type] = 12;
	}

	public override void SetDefaults()
	{
		Projectile.width = 14;
		Projectile.height = 18;
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
		if (!deploying)
		{
			deploying = true;
		}
		((Entity)Projectile).velocity = new Vector2(0f, 0f);
		return false;
	}

	public override void AI()
	{
		Main.player[Projectile.owner].GetModPlayer<WolfsPlayer>();
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
		if (!thrown)
		{
			if (((Entity)Projectile).velocity.X >= 0f)
			{
				projectileDirection = 1;
			}
			if (((Entity)Projectile).velocity.X < 0f)
			{
				projectileDirection = -1;
			}
			thrown = true;
		}
		if (deployed && !setup)
		{
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).Center.X, ((Entity)Projectile).Center.Y - 2f), new Vector2(0f, 0f), ModContent.ProjectileType<SimpleTurretHead>(), Projectile.damage, 2f, Main.myPlayer, (float)Projectile.identity, (float)projectileDirection, 0f);
			setup = true;
		}
		Projectile.rotation = 0f;
		((Entity)Projectile).velocity.Y += 0.2f;
		((Entity)Projectile).velocity.X *= 0.99f;
		if (((Entity)Projectile).velocity.Y > 12f)
		{
			((Entity)Projectile).velocity.Y = 12f;
		}
	}
}
