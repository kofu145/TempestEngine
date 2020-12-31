using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibraryEngine
{
    public class GameStateManager
    {
        private static GameStateManager instance;
        private ContentManager content;

        // stack
        private Stack<IGameState> screens = new Stack<IGameState>();

        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }
                return instance;
            }
        }

        public void SetContent(ContentManager targetContent)
        {
            content = targetContent;
        }

        public void AddScreen(IGameState screen)
        {
            try
            {
                // add screen to the stack

                screens.Push(screen);

                if (content != null)
                {
                    screens.Peek().LoadContent(content);
                }

                screens.Peek().Initialize();

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        // Remove top screen from the stack
        public void RemoveScreen()
        {
            if (screens.Count > 0)
            {
                try
                {
                    // var screen = screens.Peek();
                    screens.Pop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // clears all the screens from the list
        public void ClearScreens()
        {
            screens.Clear();
        }

        // purges stack and adds a new screen
        public void ChangeScreen(IGameState screen)
        {
            ClearScreens();
            AddScreen(screen);
        }

        public void Update(GameTime gameTime)
        {
            if (screens.Count > 0)
            {
                screens.Peek().Update(gameTime);

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (screens.Count > 0)
            {
                screens.Peek().Draw(spriteBatch);
            }
        }

        // Unloads the content from the screen
        public void UnloadContent()
        {
            foreach (IGameState state in screens)
            {
                state.UnloadContent();
            }
        }

    }
}
