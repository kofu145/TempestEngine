using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibraryEngine
{
    public class Sprite : IComponent
    {
        public Texture2D Texture;
        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
        }
    }
}
