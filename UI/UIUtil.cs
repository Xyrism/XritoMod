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
            if (!(listeningElement is UIPanel)) return;
            UIPanel panel = (UIPanel)listeningElement;
            panel.BackgroundColor = new Color(63, 82, 151, 178);
        }

        public static void MouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!(listeningElement is UIPanel)) return;
            UIPanel panel = (UIPanel)listeningElement;
            panel.BackgroundColor = new Color(44, 57, 105, 178);
        }

        public static void MouseDown(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!(listeningElement is UIPanel)) return;
            UIPanel panel = (UIPanel)listeningElement;
            panel.BackgroundColor = new Color(53, 69, 126, 178);
        }

    }
}
