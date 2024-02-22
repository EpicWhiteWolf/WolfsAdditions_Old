using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class FuriaProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 12;
		((Entity)Projectile).height = 12;
		Projectile.timeLeft = 960;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 1f);
	}

	public override void AI()
	{
		Vector2 move = Vector2.Zero;
		float distance = 400f;
		bool target = false;
		for (int i = 0; i < 200; i++)
		{
			if (((Entity)Main.npc[i]).active && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].lifeMax > 5)
			{
				Vector2 newMove = ((Entity)Main.npc[i]).Center - ((Entity)Projectile).Center;
				float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
				if (distanceTo < distance)
				{
					move = newMove;
					distance = distanceTo;
					target = true;
				}
			}
		}
		if (target)
		{
			AdjustMagnitude(move);
			((Entity)Projectile).velocity = (10f * ((Entity)Projectile).velocity + move) / 11f;
			AdjustMagnitude(((Entity)Projectile).velocity);
		}
	}

	private void AdjustMagnitude(Vector2 vector)
	{
		float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
		if (magnitude > 6f)
		{
			vector *= 6f / magnitude;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
		SoundEngine.PlaySound(SoundID.Item10, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
		return true;
	}
}
