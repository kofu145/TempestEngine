using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibraryEngine.Components;


namespace TestLibraryEngine.Systems
{
    public class xSpriteRenderer
    {
        public void Render(Scene scene, SpriteBatch spriteBatch)
        {
            var renderingAnything = false;


            var spriteEntities = scene.Entities
                .Where(e => e.HasComponent<Sprite>())
                .Where(e => e.HasComponent<Transform>());

            var cameraEntities = scene.Entities
                .Where(e => e.HasComponent<Camera>())
                .Where(e => e.HasComponent<Transform>());

            foreach (var entity in cameraEntities)
            {
                var camera = entity.GetComponent<Camera>();
                var cameraTransform = entity.GetComponent<Transform>();

                var cameraViewport = new Rectangle(
                    (int)cameraTransform.Position.X,
                    (int)cameraTransform.Position.Y,
                    (int)camera.WorldWidth,
                    (int)camera.WorldHeight);

                var entitiesToRender = spriteEntities
                    .Where(e => {
                        var spriteTransform = e.GetComponent<Transform>();
                        var sprite = e.GetComponent<Sprite>();
                        var spriteRect = new Rectangle(
                            (int)(spriteTransform.Position.X - sprite.Origin.X),
                            (int)(spriteTransform.Position.Y - sprite.Origin.Y),
                            (int)sprite.Width,
                            (int)sprite.Height);
                        return cameraViewport.Intersects(spriteRect);

                    }
                    );
                foreach(var spriteEntity in entitiesToRender)
                {
                    renderingAnything = true;
                    var spriteTransform = spriteEntity.GetComponent<Transform>();
                    var sprite = spriteEntity.GetComponent<Sprite>();
                    var spriteRect = new Rectangle(
                            (int)spriteTransform.Position.X,
                            (int)spriteTransform.Position.Y,
                            (int)sprite.Width,
                            (int)sprite.Height);

                    spriteBatch.Begin();
                    spriteBatch.Draw
                    (
                        sprite.Texture, 
                        new Rectangle(
                            camera.RenderTarget.X 
                                + (int)(
                                    (spriteTransform.Position.X - sprite.Origin.X - cameraTransform.Position.X)
                                    * (camera.RenderTarget.Width/camera.WorldWidth)
                                ),

                            camera.RenderTarget.Y 
                                + (int)(
                                    (spriteTransform.Position.Y - sprite.Origin.Y - cameraTransform.Position.Y)
                                    * (camera.RenderTarget.Height / camera.WorldHeight)
                                ),

                            (int)(
                                sprite.Width 
                                * (
                                    camera.RenderTarget.Width/camera.WorldWidth
                                )
                            ),


                            (int)(
                                sprite.Height
                                * (
                                    camera.RenderTarget.Height / camera.WorldHeight
                                )
                            )
                        ),
                        
                        new Rectangle(0, 0, sprite.Texture.Width, sprite.Texture.Height),
                        Color.White,
                        (float)spriteTransform.Rotation.Z,

                        sprite.Origin, 
                        SpriteEffects.None,
                        1
                    );
                    spriteBatch.End();

                }
                var playerTransform = scene.Entities.Where(e => e.HasComponent<Player>()).First().GetComponent<Transform>();
                Debug.WriteLine((renderingAnything ? "RENDERING: " : "OFFSCREEN: ") + $"({playerTransform.Position.X}, {playerTransform.Position.Y})");

            }

            

            


        }
    }
}
