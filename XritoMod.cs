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
        internal ClickerGUI clickerGUI;
        
		public XritoMod()
        {
			Properties = new ModProperties()
            {
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

        public override void Load()
        {
            Instance = this;

            if (!Main.dedServ)
            {
                clickerInterface = new UserInterface();
                clickerGUI = new ClickerGUI();
                clickerGUI.Activate();
                clickerInterface.SetState(clickerGUI);
            }
        }

        public override void Unload()
        {
            Instance = null;
        }

        public override void AddRecipes()
        {
			Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
			if(thoriumMod != null)
            {
					ModRecipe recipe = new ModRecipe(this);
					recipe.AddIngredient(ItemID.IronCrate);
                    recipe.AddTile(TileID.WorkBenches);
                    recipe.SetResult(thoriumMod.ItemType("StrangeCrate"));
                    recipe.AddRecipe();
			}
		}

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int newIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            layers.Insert(newIndex, new LegacyGameInterfaceLayer(
                    "XritoMod: Clicker",
                    delegate
                    {
                        clickerGUI.Draw(Main.spriteBatch);
                        return true;
                    }
                )
            );
        }
    }
}
