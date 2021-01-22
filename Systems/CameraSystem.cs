using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryEngine.Components;

namespace LibraryEngine.Systems
{
    public class CameraSystem : ISystem
    {
        private Viewport viewPort;
        public CameraSystem(Viewport viewport)
        {
            this.viewPort = viewport;
        }

        private void UpdateMatrix(Entity cameraEntity)
        {
            // updating the transform matrix
            var cameraTransform = cameraEntity.GetComponent<Transform>();
            cameraEntity.GetComponent<Camera>().transformMatrix =
                Matrix.CreateTranslation(new Vector3((int)-cameraTransform.Position.X, (int)-cameraTransform.Position.Y, 0)) *
               
                Matrix.CreateRotationZ(cameraTransform.Rotation.Z) *
                Matrix.CreateScale(cameraEntity.GetComponent<Camera>().Zoom, cameraEntity.GetComponent<Camera>().Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(
                    cameraEntity.GetComponent<Camera>().Bounds.Width * 0.5f,
                    cameraEntity.GetComponent<Camera>().Bounds.Height * 0.5f, 0
                    )
                );

        }

        

        

        public void Update(Scene scene, GameTime gameTime)
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
