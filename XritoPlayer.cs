using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;

namespace XritoMod
{
    class XritoPlayer : ModPlayer
    {
        public bool isClickerOpen = false;

        public override void UpdateDead()
        {
            if (Main.player[Main.myPlayer].dead)
            {
                isClickerOpen = false;
            }
        }
    }
}
