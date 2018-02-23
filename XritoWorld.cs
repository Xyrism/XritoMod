using Terraria;
using Terraria.ModLoader;
using XritoMod.Items;
using System;

namespace XritoMod
{
	public class XritoWorld : ModWorld
    {
		public override void PostUpdate()
        {
			if(RemoteSundial.isSkipping)
            {
				int speed = RemoteSundial.skipSpeed;
				Main.time += speed;
				if(Main.dayTime)
                {
					if(Main.time >= 54000)
                    {
							Main.time = 0;
							Main.dayTime = false;
					}
				}
                else
                {
					if(Main.time >= 32400)
                    {
							Main.time = 0;
							Main.dayTime = true;
							RemoteSundial.isSkipping = false;
					}
				}
			}
		}
	}
}