using BleachMod.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace BleachMod.Common.Players
{
	class PressureBar : UIState
	{
		private UIText text;
		private UIElement area;
		private UIImage BarFrame;
		private Color gradientA;
		private Color gradientB;
		public override void OnInitialize()
		{
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 600, 1f); 
			area.Top.Set(30, 0f); 
			area.Width.Set(182, 0f); 
			area.Height.Set(60, 0f);

			BarFrame = new UIImage(ModContent.Request<Texture2D>("BleachMod/Common/Players/PressureBar").Value);
			BarFrame.Left.Set(22, 0f);
			BarFrame.Top.Set(0, 0f);
			BarFrame.Width.Set(138, 0f);
			BarFrame.Height.Set(34, 0f);
			

			text = new UIText("0/0", 0.8f);
			text.Width.Set(138, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(40, 0f);
			text.Left.Set(0, 0f);

			gradientA = new Color(123, 25, 138); 
			gradientB = new Color(187, 91, 201); 

			area.Append(text);
			area.Append(BarFrame);
			Append(area);


		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (Main.LocalPlayer.GetModPlayer<BleachPlayer>().MaxPressure <= 0)
				return;

			base.Draw(spriteBatch);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			var modPlayer = Main.LocalPlayer.GetModPlayer<BleachPlayer>();
			float quotient = (float)modPlayer.C_Pressure / modPlayer.MaxPressure;
			quotient = Utils.Clamp(quotient, 0f, 1f);

			Rectangle hitbox = BarFrame.GetInnerDimensions().ToRectangle();
			hitbox.X += 12;
			hitbox.Width -= 24;
			hitbox.Y += 8;
			hitbox.Height -= 16;

			int left = hitbox.Left;
			int right = hitbox.Right;
			int steps = (int)((right - left) * quotient);
			for (int i = 0; i < steps; i += 1)
			{
				float percent = (float)i / (right - left);
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height), Color.Lerp(gradientA, gradientB, percent));
			}
		}
		public override void Update(GameTime gameTime)
		{
			if (Main.LocalPlayer.GetModPlayer<BleachPlayer>().MaxPressure == 0)
				return;

			var modPlayer = Main.LocalPlayer.GetModPlayer<BleachPlayer>();
			text.SetText($"Spiritual Pressure: {modPlayer.C_Pressure} / {modPlayer.MaxPressure}");
			base.Update(gameTime);
		}


	}

}
