using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.UI;

namespace XritoMod
{
    public class XritoMod : Mod
    {

        internal static XritoMod Instance;
        private UserInterface clickerInterface;
        public ClickerGUI clickerGUI;

        public override void Load()
        {
            Instance = this;

            if (!Main.dedServ)
            {
                ClickerGUI.mainButtonUp = GetTexture("ClickerButton");
                ClickerGUI.mainButtonDown = GetTexture("ClickerButton_Pressed");
                ClickerGUI.closeButtonCross = GetTexture("CloseButton");
                ClickerGUI.closeButtonDot = GetTexture("CloseButton_Locked");
                ClickerGUI.lockButtonOpen = GetTexture("LockButton");
                ClickerGUI.lockButtonClosed = GetTexture("LockButton_Closed");

                clickerInterface = new UserInterface();
                clickerGUI = new ClickerGUI();
                clickerGUI.Activate();
                clickerInterface.SetState(clickerGUI);
            }
        }

        public override void Unload()
        {
            ClickerGUI.mainButtonUp = null;
            ClickerGUI.mainButtonDown = null;
            ClickerGUI.closeButtonCross = null;
            ClickerGUI.closeButtonDot = null;
            ClickerGUI.lockButtonOpen = null;
            ClickerGUI.lockButtonClosed = null;
            Instance = null;
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int newIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (newIndex != -1)
            {
                layers.Insert(newIndex, new LegacyGameInterfaceLayer(
                        "XritoMod: Clicker",
                        delegate
                        {
                            clickerGUI.Draw(Main.spriteBatch);
                            return true;
                        },
                        InterfaceScaleType.UI
                    )
                );
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            clickerInterface.Update(gameTime);
        }
    }
}
