using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CraftingCounter
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            GraphicsEvents.OnPostRenderGuiEvent += GraphicsEvents_OnPostRenderGuiEvent;
        }





        /*********
        ** Private methods
        *********/
        private void DrawHoverTextBox(SpriteFont font, string description, int x, int y)
        {
            Vector2 stringLength = font.MeasureString(description);
            int width = (int)stringLength.X + Game1.tileSize / 2 + 40;
            int height = (int)stringLength.Y + Game1.tileSize / 3 + 5;

            if (x < 0)
                x = 0;

            if (y + height > Game1.graphics.GraphicsDevice.Viewport.Height)
                y = Game1.graphics.GraphicsDevice.Viewport.Height - height;

            IClickableMenu.drawTextureBox(Game1.spriteBatch, Game1.menuTexture, new Rectangle(0, 256, 60, 60), x, y, width, height, Color.White);
            Utility.drawTextWithShadow(Game1.spriteBatch, description, font, new Vector2(x + Game1.tileSize / 4, y + Game1.tileSize / 4), Game1.textColor);
        }

        private void GraphicsEvents_OnPostRenderGuiEvent(object sender, EventArgs e)
        {
            GameMenu gm = null;
            if ((gm = (Game1.activeClickableMenu as GameMenu)) != null &&
                gm.currentTab == 4)
            {
                CraftingPage cpage =
                    (CraftingPage)Helper.Reflection
                        .GetPrivateField<List<IClickableMenu>>(gm, "pages").GetValue()[4];
                CraftingRecipe cr = Helper.Reflection
                    .GetPrivateField<CraftingRecipe>(cpage, "hoverRecipe").GetValue();
                Point p = Game1.getMousePosition();

                if (cr != null)
                {
                    DrawHoverTextBox(Game1.smallFont, "Number crafted: " + Game1.player.craftingRecipes[cr.name],
                        p.X + 31, p.Y - 30);
                }
            }
        }
    }
}