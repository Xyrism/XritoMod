using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace XritoMod
{
    public class ClickerGUI : UIState
    {
        public static int points = 0;

        private static UIPanel mainPanel = new UIPanel();
        private static float panelLeft;
        private static float panelTop;
        private static float panelWidth;
        private static float panelHeight;

        private static UIImageButton mainButton;

        public override void OnInitialize()
        {
            panelWidth = 300f;
            panelHeight = 300f;
            panelTop = Main.instance.invBottom + 40;
            panelLeft = (Main.screenWidth / 2) - (panelWidth / 2);
            mainPanel.Left.Set(panelLeft, 0f);
            mainPanel.Top.Set(panelTop, 0f);
            mainPanel.Width.Set(panelWidth, 0f);
            mainPanel.Height.Set(panelHeight, 0f);
            mainPanel.Recalculate();

            mainButton = new UIImageButton(XritoMod.Instance.GetTexture("ClickerButton"));
            mainButton.Left.Set((mainPanel.GetInnerDimensions().Width / 2) - (mainButton.Width.Pixels / 2), 0f);
            mainButton.Top.Set((mainPanel.GetInnerDimensions().Height / 2) - (mainButton.Height.Pixels / 2), 0f);
            mainButton.Recalculate();
            mainPanel.Append(mainButton);

            base.Append(mainPanel);
        }

        public override void Update(GameTime time)
        {
            try
            {

            }
            catch(Exception e)
            {
                Main.NewTextMultiline(e.ToString());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (Main.player[Main.myPlayer].GetModPlayer<XritoPlayer>(XritoMod.Instance).isClickerOpen)
                {
                    mainPanel.Draw(spriteBatch);
                }
            }
            catch(Exception e)
            {
                Main.NewTextMultiline(e.ToString());
            }
        }
    }
}
