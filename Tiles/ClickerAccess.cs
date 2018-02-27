using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XritoMod.Tiles
{
    class ClickerAccess : ModTile
    {

        private bool isPressed = false;

        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
            TileObjectData.addTile(Type);
            ModTranslation label = CreateMapEntryName();
            label.SetDefault("Terra Clicker Access");
            AddMapEntry(new Color(0, 183, 138), label);
            disableSmartCursor = true;
            dustType = 209;
            animationFrameHeight = 36;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("ClickerAccess"));
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;
            if(frameCounter > 30)
            {
                frameCounter = 0;
                frame++;
                if(frame > 3)
                {
                    frame = 0;
                }
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = -1;

            if (!XritoMod.Instance.clickerGUI.isVisible)
            {
                player.showItemIconText = "Right click to access\nLeft click to operate";

                if (Main.mouseLeft && !Main.mouseLeftRelease)
                {
                    isPressed = true;
                }
                if (isPressed && Main.mouseLeftRelease)
                {
                    XritoMod.Instance.clickerGUI.MainClicked();
                    isPressed = false;
                }
            }
            else
            {
                player.showItemIconText = "Right click to close menu";
            }
        }

        public override void RightClick(int i, int j)
        {
            XritoMod.Instance.clickerGUI.isVisible = !XritoMod.Instance.clickerGUI.isVisible;
        }
    }
}
