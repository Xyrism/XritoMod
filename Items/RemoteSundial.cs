using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XritoMod.Items
{
    public class RemoteSundial : ModItem
    {

        public static bool isSkipping = false;
        public static int skipSpeed = 90;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Remote Sundial");
            Tooltip.SetDefault("A portable sundial with no cooldown time");
        }
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.useStyle = 4;
            item.useAnimation = 16;
            item.useTime = 16;
            item.reuseDelay = 20;
            item.value = Item.sellPrice(0, 10, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item4;
        }

        public override bool UseItem(Player player)
        {
            if (!isSkipping)
            {
                isSkipping = true;
                return true;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemID.Sundial, 3);
            //recipe.AddIngredient(ItemID.LunarBar, 15);
            //recipe.AddTile(TileID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
