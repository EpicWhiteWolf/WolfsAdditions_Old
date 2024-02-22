using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WolfsAdditions.Buffs;

namespace WolfsAdditions.Projectiles;

internal class LacerationProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 10;
		((Entity)Projectile).height = 10;
		Projectile.timeLeft = 240;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.aiStyle = 0;
		Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		Color glow = default(Color);
		glow = new Color(1f, 1f, 1f, 1f);
		GetAlpha(glow);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<LacerationsDebuff>(), 240, false);
	}

	public override void OnKill(int timeLeft)
	{
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
		SoundEngine.PlaySound(SoundID.Item10, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
	}
}
