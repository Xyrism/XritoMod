using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace XritoMod.Items
{
    class ClickerAccess : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Clicker");
            Tooltip.SetDefault("Your very own resource generator\n[c/777777:I sense carpal tunnel in your future]");
        }

        public override void SetDefaults()
        {
            item.consumable = true;
            item.maxStack = 99;
            item.autoReuse = true;
            item.useTime = 10;
            item.useAnimation = 14;
            item.useStyle = 1;
            item.rare = 1;
            item.createTile = mod.TileType("ClickerAccess");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
