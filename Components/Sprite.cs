using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibraryEngine.Components

{
    public class Sprite : IComponent
    {
        public Texture2D Texture;

        public double Width => Texture.Width;
        public double Height => Texture.Height;

        public Vector2 Origin;
        
        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            this.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public Sprite Copy()
        {
            // shallow copy, remove later pls
            return (Sprite)this.MemberwiseClone();
        }

    }
}
