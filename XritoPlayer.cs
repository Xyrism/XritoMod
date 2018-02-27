using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace XritoMod
{
    class XritoPlayer : ModPlayer
    {
        public override void UpdateDead()
        {
            if (Main.player[Main.myPlayer].dead)
            {
                XritoMod.Instance.clickerGUI.isVisible = false;
            }
        }

        public override void OnEnterWorld(Player player)
        {
            XritoMod.Instance.clickerGUI.isVisible = false;
            XritoMod.Instance.clickerGUI.OnWorldLoad();
        }
    }
}
