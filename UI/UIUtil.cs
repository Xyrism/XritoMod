using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XritoMod.UI
{
    static class UIUtil
    {

        public static void MouseHover(UIMouseEvent evt, UIElement listeningElement)
        {
            ReColor(listeningElement, new Color(63, 82, 151, 178));
        }

        public static void MouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            ReColor(listeningElement, new Color(44, 57, 105, 178));
        }

        public static void MouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            ReColor(listeningElement, new Color(53, 69, 126, 178));
        }

        private static void ReColor(UIElement element, Color color)
        {
            if (!(element is UIPanel)) return;
            UIPanel panel = (UIPanel)element;
            panel.BackgroundColor = color;
        }

    }
}
