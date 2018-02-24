using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace XritoMod.UIElements
{
    class UIUpgradeButton : UIElement
    {

        private const float spacing = 10f;

        private Texture2D texture;
        private String name;

        public UIUpgradeButton(Texture2D texture, String name)
        {
            this.texture = texture;
            this.name = name;
            Width.Set(texture.Width + 120f + spacing, 0f);
            Height.Set(texture.Height, 0f);
            OverflowHidden = true;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            
        }

        public override void Click(UIMouseEvent evt)
        {
            base.Click(evt);
        }
    }
}
