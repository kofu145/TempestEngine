using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibraryEngine.Components;

namespace TestLibraryEngine.Systems
{
    public class CameraSystem
    {
        
        private void UpdateMatrix(Entity cameraEntity)
        {
            // updating the transform matrix
            var cameraTransform = cameraEntity.GetComponent<Transform>();
            cameraEntity.GetComponent<Camera>().transformMatrix =
                Matrix.CreateTranslation(new Vector3((int)-cameraTransform.Position.X, (int)-cameraTransform.Position.Y, 0)) *
                // cos theta, negative sin theta, sin theta, cos theta = rotation matrix in 2d
                Matrix.CreateRotationZ(cameraTransform.Rotation.Z) *
                Matrix.CreateScale(cameraEntity.GetComponent<Camera>().Zoom, cameraEntity.GetComponent<Camera>().Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(
                    cameraEntity.GetComponent<Camera>().Bounds.Width * 0.5f,
                    cameraEntity.GetComponent<Camera>().Bounds.Height * 0.5f, 0
                    )
                );

        }

        private void UpdateBoundingBox(Entity cameraEntity)
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

            cameraEntity.GetComponent<Camera>().RenderTarget = new Rectangle
                (
                    (int)min.X,
                    (int)min.Y,
                    (int)(max.X - min.X),
                    (int)(max.Y - min.Y)
                );


        }

        

        public void Update(Scene scene, Viewport viewPort)
        {
            var cameraEntities = scene.Entities
                .Where(e => e.HasComponent<Camera>())
                .Where(e => e.HasComponent<Transform>());

            foreach (var cameraEntity in cameraEntities)
            {
                UpdateMatrix(cameraEntity);
                cameraEntity.GetComponent<Camera>().Bounds = viewPort.Bounds;

                



            }
            
        } 
    }
}
