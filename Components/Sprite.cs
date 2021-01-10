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
        public SpriteEffects SpriteEffect;
        public Rectangle sourceRect;
        public Vector2 Origin;
        
        public Sprite(Texture2D texture, SpriteEffects spriteEffect=SpriteEffects.None)
        {
            this.Texture = texture;
            this.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.SpriteEffect = spriteEffect;
            this.sourceRect = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
        }

        public Sprite Copy()
        {
            // shallow copy, remove later pls
            return (Sprite)this.MemberwiseClone();
        }

    }
}
