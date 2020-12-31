using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibraryEngine
{
    public interface IGameState
    {
        // Initialize the game settings here      
        void Initialize();

        // Load all content here
        void LoadContent(ContentManager content);

        // Unload any content here
        void UnloadContent();

        // Updates the game
        void Update(GameTime gameTime);

        // Draws the game
        void Draw(SpriteBatch spriteBatch);
    }
}
