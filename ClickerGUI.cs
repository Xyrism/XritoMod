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
        public int points = 0;
        public int pointsPerSecond = 0;

        public static bool isVisible = false;
        private bool isDragging = false;
        private int lastMouseX = 0;
        private int lastMouseY = 0;

        private UIPanel titlePanel;
        private float panelWidth;
        private float panelHeight;
        private float panelLeft;
        private float panelTop;

        private UIPanel mainPanel;
        private UIPanel upgradePanel;

        private UIElement centerInfo;
        private UIText pointInfo;

        private UIImageButton mainButton;
        public static Texture2D mainButtonUp;
        public static Texture2D mainButtonDown;
        private bool mainPressed = false;

        public override void OnInitialize()
        {
            panelWidth = 500f;
            panelHeight = 300f;
            panelTop = Main.instance.invBottom + 10;
            panelLeft = (Main.screenWidth / 2) - (panelWidth / 2);

            titlePanel = new UIPanel();
            titlePanel.Width.Set(panelWidth, 0f);
            titlePanel.Height.Set(30f, 0f);
            Append(titlePanel);

            UIText title = new UIText("Terra Clicker");
            title.HAlign = 0.5f;
            title.Top.Set(-5f, 0f);
            titlePanel.Append(title);

            mainPanel = new UIPanel();
            mainPanel.Width.Set(300f, 0f);
            mainPanel.Height.Set(panelHeight, 0f);
            Append(mainPanel);

            centerInfo = new UIElement();
            centerInfo.Width.Set(180f, 0f);
            centerInfo.Height.Set(180f, 0f);
            centerInfo.HAlign = 0.5f;
            centerInfo.VAlign = 0.5f;
            mainPanel.Append(centerInfo);

            mainButton = new UIImageButton(mainButtonUp);
            mainButton.HAlign = 0.5f;
            mainButton.VAlign = 0.5f;
            mainButton.OnMouseDown += delegate {
                mainPressed = true;
                mainButton.SetImage(mainButtonDown);
            };
            centerInfo.Append(mainButton);

            pointInfo = new UIText(points.ToString() + " points");
            pointInfo.HAlign = 0.5f;
            pointInfo.Left.Set(0f, 0f);
            pointInfo.Top.Set(15f, 0.5f);
            centerInfo.Append(pointInfo);

            upgradePanel = new UIPanel();
            upgradePanel.Width.Set(200f, 0f);
            upgradePanel.Height.Set(panelHeight, 0f);
            Append(upgradePanel);

            PlaceGUI(panelLeft, panelTop);
        }

        public void OnWorldLoad()
        {
            pointInfo.SetText(points.ToString() + " points");
        }

        private void PlaceGUI(float newX, float newY)
        {
            panelLeft = newX > Main.screenWidth - panelWidth ? Main.screenWidth - panelWidth : newX;
            panelLeft = panelLeft < 0 ? 0 : panelLeft;
            panelTop = newY > Main.screenHeight - panelHeight ? Main.screenHeight - panelHeight : newY;
            panelTop = panelTop < 0 ? 0 : panelTop;

            titlePanel.Left.Set(panelLeft, 0f);
            titlePanel.Top.Set(panelTop, 0f);

            mainPanel.Left.Set(panelLeft, 0f);
            mainPanel.Top.Set(panelTop + 30, 0f);

            upgradePanel.Left.Set(panelLeft + mainPanel.Width.Pixels, 0f);
            upgradePanel.Top.Set(panelTop + 30, 0f);

            mainPanel.Recalculate();
            titlePanel.Recalculate();
            upgradePanel.Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            try
            {
                if (isVisible)
                {
                    if (Main.mouseLeft && !Main.mouseLeftRelease)
                    {
                        if (!isDragging && (IsMouseCollide(mainPanel) || IsMouseCollide(titlePanel)))
                        {
                            isDragging = true;
                            lastMouseX = Main.mouseX;
                            lastMouseY = Main.mouseY;
                        }
                    }
                    if (IsMouseInWindow())
                    {
                        if (isDragging)
                        {
                            float diffX = Main.mouseX - lastMouseX;
                            float diffY = Main.mouseY - lastMouseY;
                            lastMouseX = Main.mouseX;
                            lastMouseY = Main.mouseY;
                            PlaceGUI(panelLeft + diffX, panelTop + diffY);
                        }
                    }
                    if (Main.mouseLeftRelease)
                    {
                        if (mainPressed)
                        {
                            mainPressed = false;
                            mainButton.SetImage(mainButtonUp);
                            Clicked();
                        }
                        if (isDragging)
                        {
                            isDragging = false;
                        }
                    }
                }
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
                if (isVisible)
                {
                    titlePanel.Draw(spriteBatch);
                    mainPanel.Draw(spriteBatch);
                    upgradePanel.Draw(spriteBatch);

                    mainButton.Draw(spriteBatch);
                }
            }
            catch(Exception e)
            {
                Main.NewTextMultiline(e.ToString());
            }
        }

        public void Clicked()
        {
            points++;
            pointInfo.SetText(points.ToString() + " points");
            Main.NewText("points: " + points);
        }

        private static bool IsMouseCollide(UIElement element)
        {
            if (element.ContainsPoint(Main.MouseScreen)) return true;
            return false;
        }

        private static bool IsMouseInWindow()
        {
            return Main.graphics.GraphicsDevice.Viewport.Bounds.Contains(Main.mouseX, Main.mouseY);
        }
    }
}
