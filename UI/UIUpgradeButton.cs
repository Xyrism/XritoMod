using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace XritoMod.UI
{
    class UIUpgradeButton : UIElement
    {

        private String name;
        private String desc;

        private UIPanel mainPanel;
        private UIPanel hoverPanel;
        private UITextPanel<String> buyButton;

        public event MouseEvent onBuyButtonClick;

        private UIText nameLabel;
        private UIText descLabel;

        public UIUpgradeButton(String name, String desc)
        {
            this.name = name;
            this.desc = desc;
        }

        public override void OnInitialize()
        {
            mainPanel = new UIPanel();
            mainPanel.Width.Set(0f, 1f);
            mainPanel.Height.Set(0f, 1f);
            mainPanel.OnMouseOver += UIUtil.MouseHover;
            mainPanel.OnMouseOut += UIUtil.MouseOut;
            Append(mainPanel);

            nameLabel = new UIText(name, 0.8f);
            nameLabel.VAlign = 0.5f;
            mainPanel.Append(nameLabel);

            buyButton = new UITextPanel<String>("Buy", 0.6f);
            buyButton.VAlign = 0.5f;
            buyButton.SetPadding(8f);
            buyButton.Left.Set(-45f, 1f);
            buyButton.Width.Set(40f, 0f);
            buyButton.Height.Set(20f, 0f);
            buyButton.OnClick += delegate(UIMouseEvent evt, UIElement listeningElement)
            {
                if(onBuyButtonClick != null)
                    onBuyButtonClick(evt, listeningElement);
            };
            buyButton.OnMouseOver += UIUtil.MouseHover;
            buyButton.OnMouseOut += UIUtil.MouseOut;
            buyButton.OnMouseDown += UIUtil.MouseDown;
            buyButton.OnMouseUp += UIUtil.MouseHover;
            mainPanel.Append(buyButton);

            hoverPanel = new UIPanel();

            descLabel = new UIText(desc);
            hoverPanel.Append(descLabel);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            mainPanel.Draw(spriteBatch);
            buyButton.Draw(spriteBatch);

            if (IsMouseHovering)
            {
                
                //hoverPanel.Draw(spriteBatch);
            }
        }
    }
}
