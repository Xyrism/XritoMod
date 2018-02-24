using Terraria;
using Terraria.ModLoader;
using XritoMod.Items;
using System;
using Terraria.ModLoader.IO;

namespace XritoMod
{
	public class XritoWorld : ModWorld
    {
		public override void PreUpdate()
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

        public override TagCompound Save()
        {
            TagCompound tagCompound = new TagCompound();
            tagCompound["terraPoints"] = XritoMod.Instance.clickerGUI.points;
            return tagCompound;
        }

        public override void Load(TagCompound tag)
        {
            XritoMod.Instance.clickerGUI.points = tag.GetInt("terraPoints");
        }
    }
}