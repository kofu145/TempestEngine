using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TestLibraryEngine.Components;


namespace TestLibraryEngine
{
    public class SpriteRenderer
    {
        public void Render(Scene scene, SpriteBatch spriteBatch)
        {
            var spriteEntities = scene.Entities
                .Where(e => e.HasComponent<Sprite>())
                .Where(e => e.HasComponent<Transform>());
            
            foreach(var entity in spriteEntities)
            {
                var sprite = entity.GetComponent<Sprite>();
                var transform = entity.GetComponent<Transform>();
                spriteBatch.Draw(sprite.Texture, new Rectangle((int)transform.Position.X, (int)transform.Position.Y, sprite.Texture.Width, sprite.Texture.Height), new Rectangle(0, 0, sprite.Texture.Width, sprite.Texture.Height), Color.White, (float)transform.Rotation.Z, new Vector2(sprite.Texture.Width/2, sprite.Texture.Height / 2), SpriteEffects.None, 1);
                // spriteBatch.Draw(sprite.Texture, new Rectangle((int)transform.Position.X, (int)transform.Position.Y, sprite.Texture.Width, sprite.Texture.Height), Color.White);

            }


        }
    }
}
