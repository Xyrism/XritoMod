using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XritoMod.UI;
using System;
using System.Collections.Generic;

namespace XritoMod
{
    public class ClickerGUI : UIState
    {
        public double points = 0;
        public double pointsPerSecond = 10;

        private TimeSpan currentSpan;
        private TimeSpan lastSpan;

        public bool isVisible = false;
        private bool isLocked = false;
        private bool isDragging = false;
        private int lastMouseX = 0;
        private int lastMouseY = 0;

        private float panelSpacing = 4f;

        //Textures
        public static Texture2D mainButtonUp;
        public static Texture2D mainButtonDown;
        public static Texture2D closeButtonCross;
        public static Texture2D closeButtonDot;
        public static Texture2D lockButtonOpen;
        public static Texture2D lockButtonClosed;

        //Back panel and elements
        private UIPanel backPanel;
        private float panelWidth;
        private float panelHeight;
        private float panelLeft;
        private float panelTop;

        private UIImageButton closeButton;
        private UIImageButton lockButton;

        //Title panel and elements
        private UIPanel titlePanel;

        //Main panel and elements
        private UIPanel mainPanel;

        private UIElement centerInfo;
        private UIText pointInfo;
        private UIText ppsInfo;

        private UIImageButton mainButton;
        private bool mainPressed = false;
        
        //Upgrade panel and elements
        private UIPanel upgradePanel;

        private List<UIImageButton> upgrades;
        private UIList upgradeList;
        private UIScrollbar upgradeScrollbar;
        private UIUpgradeButton testUpgrade;

        public override void OnInitialize()
        {
            panelWidth = 600f;
            panelHeight = 300f;
            panelTop = Main.instance.invBottom + 10;
            panelLeft = (Main.screenWidth / 2) - (panelWidth / 2);

            closeButton = new UIImageButton(closeButtonCross);
            closeButton.OnClick += CloseClicked;
            Append(closeButton);

            lockButton = new UIImageButton(lockButtonOpen);
            lockButton.OnClick += LockClicked;
            Append(lockButton);

            backPanel = new UIPanel();
            backPanel.BackgroundColor = backPanel.BackgroundColor * 0.8f;
            backPanel.Width.Set(panelWidth, 0f);
            backPanel.Height.Set(panelHeight, 0f);
            backPanel.SetPadding(10f);
            Append(backPanel);

            titlePanel = new UIPanel();
            titlePanel.BackgroundColor = new Color(76, 92, 148);
            titlePanel.HAlign = 0.5f;
            titlePanel.Top.Set(-35f, 0f);
            titlePanel.Width.Set(240f, 0f);
            titlePanel.Height.Set(65f, 0f);
            float panelYOffset = titlePanel.Height.Pixels + titlePanel.Top.Pixels + (panelSpacing * 2);
            backPanel.Append(titlePanel);

            UIText title = new UIText("Terra Clicker", 0.8f, true);
            title.HAlign = 0.5f;
            title.VAlign = 0.5f;
            titlePanel.Append(title);

            mainPanel = new UIPanel();
            mainPanel.Left.Set(0f, 0f);
            mainPanel.Top.Set(panelYOffset, 0f);
            mainPanel.Width.Set(-panelSpacing, 0.5f);
            mainPanel.Height.Set(-panelYOffset, 1f);
            backPanel.Append(mainPanel);

            centerInfo = new UIElement();
            centerInfo.Width.Set(180f, 0f);
            centerInfo.Height.Set(180f, 0f);
            centerInfo.HAlign = 0.5f;
            centerInfo.VAlign = 0.5f;
            mainPanel.Append(centerInfo);

            mainButton = new UIImageButton(mainButtonUp);
            mainButton.HAlign = 0.5f;
            mainButton.VAlign = 0.5f;
            mainButton.OnMouseDown += MainPressed;
            mainButton.OnMouseUp += MainReleased;
            mainButton.OnMouseOut += MainReleased;
            centerInfo.Append(mainButton);

            pointInfo = new UIText(points.ToString() + " points");
            pointInfo.HAlign = 0.5f;
            pointInfo.Top.Set(16f, 0.5f);
            centerInfo.Append(pointInfo);

            ppsInfo = new UIText(pointsPerSecond.ToString() + " points per second", 0.8f);
            ppsInfo.HAlign = 0.5f;
            ppsInfo.Top.Set(32f, 0.5f);
            centerInfo.Append(ppsInfo);

            upgradePanel = new UIPanel();
            upgradePanel.SetPadding(8f);
            upgradePanel.Left.Set(panelSpacing, 0.5f);
            upgradePanel.Top.Set(panelYOffset, 0f);
            upgradePanel.Width.Set(-panelSpacing, 0.5f);
            upgradePanel.Height.Set(-panelYOffset, 1f);
            backPanel.Append(upgradePanel);

            upgradeScrollbar = new UIScrollbar();
            upgradeScrollbar.HAlign = 1f;
            upgradeScrollbar.VAlign = 0.5f;
            upgradeScrollbar.SetView(100f, 1000f);
            upgradeScrollbar.Height.Set(-12f, 1f);
            upgradePanel.Append(upgradeScrollbar);

            upgradeList = new UIList();
            upgradeList.SetScrollbar(upgradeScrollbar);
            upgradeList.ListPadding = 5f;
            upgradeList.Width.Set(0f, 1f);
            upgradeList.Height.Set(0f, 1f);
            upgradePanel.Append(upgradeList);

            InitUpgrades();
            PlaceGUI(panelLeft, panelTop);
        }

        public void OnWorldLoad()
        {
            pointInfo.SetText(points.ToString() + " points");
            currentSpan = ConvertToUnixTime(DateTime.Now);
            lastSpan = currentSpan;
        }

        private void InitUpgrades()
        {
            testUpgrade = new UIUpgradeButton("Test", "blank");
            testUpgrade.Width.Set(-25f, 1f);
            testUpgrade.Height.Set(38f, 0f);
            upgradeList.Add(testUpgrade);
        }

        private void PlaceGUI(float newX, float newY)
        {
            panelLeft = newX > Main.screenWidth - panelWidth ? Main.screenWidth - panelWidth : newX;
            panelLeft = panelLeft < 0 ? 0 : panelLeft;
            panelTop = newY > Main.screenHeight - panelHeight ? Main.screenHeight - panelHeight : newY;
            panelTop = panelTop < 0 ? 0 : panelTop;

            backPanel.Left.Set(panelLeft, 0f);
            backPanel.Top.Set(panelTop, 0f);
            backPanel.Recalculate();

            closeButton.Left.Set(panelLeft + panelWidth - 30f, 0f);
            closeButton.Top.Set(panelTop - 30f, 0f);
            closeButton.Recalculate();

            lockButton.Left.Set(panelLeft + panelWidth - 60f, 0f);
            lockButton.Top.Set(panelTop - 30f, 0f);
            lockButton.Recalculate();
        }

        public override void Update(GameTime gameTime)
        {
            try
            {
                //Update idle game values
                currentSpan = ConvertToUnixTime(DateTime.Now);
                if(currentSpan.TotalMilliseconds >= lastSpan.TotalMilliseconds + 100)
                {
                    double diff = (currentSpan.TotalMilliseconds - lastSpan.TotalMilliseconds) / 1000d;
                    points += Math.Round(pointsPerSecond * diff, 0);
                    pointInfo.SetText(points.ToString() + " points");
                    lastSpan = currentSpan;
                }

                //Update UI elements
                if (isVisible)
                {
                    if ((backPanel.IsMouseHovering || closeButton.IsMouseHovering || lockButton.IsMouseHovering) &&
                            !upgradeScrollbar.IsMouseHovering)
                    {
                        Main.LocalPlayer.delayUseItem = true;
                    }
                    if (Main.mouseLeft && !Main.mouseLeftRelease)
                    {
                        if (!isDragging && !isLocked && backPanel.IsMouseHovering)
                        {
                            isDragging = true;
                            lastMouseX = Main.mouseX;
                            lastMouseY = Main.mouseY;
                        }
                    }
                    if (IsMouseInWindow())
                    {
                        if (isDragging && !isLocked)
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
                        if (isDragging)
                        {
                            isDragging = false;
                        }
                    }
                }
            }
            catch (Exception e)
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
                    backPanel.Draw(spriteBatch);

                    upgradeList.Draw(spriteBatch);

                    mainButton.Draw(spriteBatch);
                    closeButton.Draw(spriteBatch);
                    lockButton.Draw(spriteBatch);
                }
            }
            catch (Exception e)
            {
                Main.NewTextMultiline(e.ToString());
            }
        }

        public void CalculatePPS()
        {

        }

        public void MainClicked()
        {
            points++;
            pointInfo.SetText(points.ToString() + " points");
            Main.PlaySound(SoundID.MenuTick);
        }

        private void MainPressed(object a, object b)
        {
            if (!isVisible) return;
            mainButton.SetImage(mainButtonDown);
            mainPressed = true;
        }

        private void MainReleased(object a, object b)
        {
            if (!isVisible) return;
            mainButton.SetImage(mainButtonUp);
            if (mainPressed) MainClicked();
        }

        private void LockClicked(object a, object b)
        {
            if (!isVisible) return;
            isLocked = !isLocked;
            if (isLocked)
            {
                lockButton.SetImage(lockButtonClosed);
                closeButton.SetImage(closeButtonDot);
            }
            else
            {
                lockButton.SetImage(lockButtonOpen);
                closeButton.SetImage(closeButtonCross);
            }
        }

        private void CloseClicked(object a, object b)
        {
            if (!isVisible) return;
            if (isLocked) return;
            isVisible = false;
        }

        private static TimeSpan ConvertToUnixTime(DateTime date)
        {
            return date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
        }

        private static bool IsMouseInWindow()
        {
            return Main.graphics.GraphicsDevice.Viewport.Bounds.Contains(Main.mouseX, Main.mouseY);
        }
    }
}
