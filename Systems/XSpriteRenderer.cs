using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryEngine.Components;


namespace LibraryEngine.Systems
{
    public class XSpriteRenderer
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

                var cameraViewport = UpdateBoundingBox(entity);

                // culls anything outside of the bounding box of the camera
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
                                    * (camera.RenderTarget.Width/camera.Bounds.Width)
                                ),

                            camera.RenderTarget.Y 
                                + (int)(
                                    (spriteTransform.Position.Y - sprite.Origin.Y - cameraTransform.Position.Y)
                                    * (camera.RenderTarget.Height / camera.Bounds.Height)
                                ),

                            (int)(
                                sprite.Width 
                                * (
                                    camera.RenderTarget.Width/ camera.Bounds.Width
                                )
                            ),


                            (int)(
                                sprite.Height
                                * (
                                    camera.RenderTarget.Height / camera.Bounds.Height
                                )
                            )
                        ),
                        
                        sprite.sourceRect,
                        Color.White,
                        (float)spriteTransform.Rotation.Z,

                        sprite.Origin, 
                        sprite.SpriteEffect,
                        1
                    );
                    spriteBatch.End();

                }
                var playerTransform = scene.Entities.Where(e => !e.HasComponent<Player>() && e.HasComponent<Sprite>()).First().GetComponent<Transform>();
                Debug.WriteLine((renderingAnything ? "RENDERING: " : "OFFSCREEN: ") + $"({playerTransform.Position.X}, {playerTransform.Position.Y})");

            }

            

            


        }

        private Rectangle UpdateBoundingBox(Entity cameraEntity)
        {
            
                
            // Updating the "visible area" of camera (bounding box used for culling)
            var inverseViewMatrix = Matrix.Invert(cameraEntity.GetComponent<Camera>().transformMatrix);

            var tl = Vector2.Transform
                (
                    Vector2.Zero,
                    inverseViewMatrix
                );
            var tr = Vector2.Transform(new Vector2
                (
                    cameraEntity.GetComponent<Camera>().Bounds.X, 0),
                    inverseViewMatrix
                );
            var bl = Vector2.Transform(new Vector2
                (
                    0,
                    cameraEntity.GetComponent<Camera>().Bounds.Y),
                    inverseViewMatrix
                );
            var br = Vector2.Transform(new Vector2
                (
                    cameraEntity.GetComponent<Camera>().Bounds.Width,
                    cameraEntity.GetComponent<Camera>().Bounds.Height),
                    inverseViewMatrix
                );

            // find the bounding box of the camera area by getting exact bottom left and top right points
            var min = new Vector2
                (
                    MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                    MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y)))
                );
            var max = new Vector2
                (
                    MathHelper.Max(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                    MathHelper.Max(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y)))
                );

            return new Rectangle
                (
                    (int)min.X,
                    (int)min.Y,
                    (int)(max.X - min.X),
                    (int)(max.Y - min.Y)
                );

            
        }
    }
}
