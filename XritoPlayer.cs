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
                ClickerGUI.isVisible = false;
            }
        }

        public override void OnEnterWorld(Player player)
        {
            XritoMod.Instance.clickerGUI.OnWorldLoad();
        }
    }
}
