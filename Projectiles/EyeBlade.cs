using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class EyeBlade : ModProjectile
{
	public bool spawned;

	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 10;
		((Entity)Projectile).height = 10;
		Projectile.aiStyle = 0;
		Projectile.penetrate = -1;
		Projectile.scale = 1.3f;
		Projectile.alpha = 0;
		DrawOffsetX = -6;
		DrawOriginOffsetY = -6;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.tileCollide = true;
		Projectile.friendly = true;
	}

	public override void AI()
	{
		Vector2 between = ((Entity)Projectile).Center - ((Entity)Main.player[Projectile.owner]).Center;
		_ = ((Entity)Main.player[Projectile.owner]).Center - ((Entity)Projectile).Center;
		Projectile.rotation = (float)Math.Atan2(between.Y, between.X) + MathHelper.ToRadians(135f);
		Vector2 dustCenter = ((Entity)Projectile).Center - Utils.SafeNormalize(between, Vector2.UnitX) * 16f - new Vector2(4f, 4f);
		Vector2 dustSpread = default(Vector2);
		dustSpread = new Vector2((float)Main.rand.Next(-2, 2), (float)Main.rand.Next(-2, 2));
		for (int d = 0; d < 2; d++)
		{
			Dust.NewDust(dustCenter + dustSpread, 0, 0, Mod.Find<ModDust>("BloodDust").Type, 0f, 10f, 0, default(Color), 1f);
		}
		if (!spawned)
		{
			SoundEngine.PlaySound(SoundID.Item1, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
			spawned = true;
		}
		if (Main.myPlayer != Projectile.owner || Projectile.ai[0] != 0f)
		{
			return;
		}
		Player obj = Main.player[Projectile.owner];
		Vector2 vectorToPlayer = ((Entity)obj).Center - ((Entity)Projectile).Center;
		float distanceToPlayer = vectorToPlayer.Length();
		if (obj.channel)
		{
			Projectile.tileCollide = true;
			float maxDistance = 18f;
			Vector2 vectorToCursor = Main.MouseWorld - ((Entity)Projectile).Center;
			float distanceToCursor = vectorToCursor.Length();
			if (distanceToPlayer >= 1500f)
			{
				Projectile.Kill();
			}
			if (distanceToCursor > maxDistance)
			{
				distanceToCursor = maxDistance / distanceToCursor;
				vectorToCursor *= distanceToCursor;
			}
			int num = (int)(vectorToCursor.X * 1000f);
			int oldVelocityXBy1001 = (int)(((Entity)Projectile).velocity.X * 1000f);
			int velocityYBy1001 = (int)(vectorToCursor.Y * 1000f);
			int oldVelocityYBy1001 = (int)(((Entity)Projectile).velocity.Y * 1000f);
			if (num != oldVelocityXBy1001 || velocityYBy1001 != oldVelocityYBy1001)
			{
				Projectile.netUpdate = true;
			}
			((Entity)Projectile).velocity = vectorToCursor;
		}
		else
		{
			Projectile.tileCollide = false;
			if (distanceToPlayer <= 25f || distanceToPlayer >= 1500f)
			{
				Projectile.Kill();
			}
			distanceToPlayer = 18f / distanceToPlayer;
			vectorToPlayer *= distanceToPlayer;
			int num2 = (int)(vectorToPlayer.X * 1000f);
			int oldVelocityXBy1000 = (int)(((Entity)Projectile).velocity.X * 1000f);
			int velocityYBy1000 = (int)(vectorToPlayer.Y * 1000f);
			int oldVelocityYBy1000 = (int)(((Entity)Projectile).velocity.Y * 1000f);
			if (num2 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
			{
				Projectile.netUpdate = true;
			}
			((Entity)Projectile).velocity = vectorToPlayer;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
