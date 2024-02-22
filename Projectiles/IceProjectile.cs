using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WolfsAdditions.Buffs;

namespace WolfsAdditions.Projectiles;

internal class IceProjectile : ModProjectile
{
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 8;
		((Entity)Projectile).height = 8;
		Projectile.timeLeft = 30;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.tileCollide = true;
		Projectile.ignoreWater = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.aiStyle = 0;
	}

	public override void AI()
	{
		Lighting.AddLight(((Entity)Projectile).Center, 0f, 0.25f, 1f);
		Color glow = default(Color);
		glow = new Color(1f, 1f, 1f, 1f);
		GetAlpha(glow);
		Projectile.rotation = Utils.ToRotation(((Entity)Projectile).velocity) + MathHelper.ToRadians(90f);
		int dustnumber = Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 67, 0f, 0f, 100, default(Color), 1.2f);
		Main.dust[dustnumber].noGravity = true;
	}

	public override void OnKill(int timeLeft)
	{
		Dust.NewDust(((Entity)Projectile).position, ((Entity)Projectile).width, ((Entity)Projectile).height, 67, 0f, 0f, 100, default(Color), 1.2f);
		SoundEngine.PlaySound(SoundID.Item27, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
	}

	public override bool PreKill(int timeLeft)
	{
		for (int i = 0; i < 3; i++)
		{
			Vector2 projSpread = Utils.RotatedBy(new Vector2(((Entity)Projectile).velocity.X, ((Entity)Projectile).velocity.Y), (double)MathHelper.ToRadians((float)(10 * i - 10)), default(Vector2));
			Projectile.NewProjectile(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).position.X, ((Entity)Projectile).position.Y - 4f), projSpread, Mod.Find<ModProjectile>("IceShardProj").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f, 0f);
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<FrostedDebuff>(), 480, false);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		Collision.HitTiles(((Entity)Projectile).position, ((Entity)Projectile).velocity, ((Entity)Projectile).width, ((Entity)Projectile).height);
		return true;
	}
}
