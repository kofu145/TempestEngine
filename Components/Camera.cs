using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEngine.Components
{
    public class Camera : IComponent
    {
        public Matrix transformMatrix;

        public float Zoom { get; set; }
        
        public Rectangle Bounds { get; set; }
        public Rectangle RenderTarget { get; set; }



        // public Color DefaultColor = Color.CornflowerBlue;


        public Camera(float zoom, Viewport viewPort, Rectangle renderTarget)
        {
            this.Zoom = zoom;
            this.Bounds = viewPort.Bounds;
            this.RenderTarget = renderTarget;
            this.transformMatrix = 
                Matrix.CreateTranslation(new Vector3((int)0, (int)0, 0)) *

                Matrix.CreateRotationZ(0) *
                Matrix.CreateScale(Zoom, Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(
                    Bounds.Width * 0.5f,
                    Bounds.Height * 0.5f, 0
                    )
                );

        }

        //public Camera (Viewport viewPort)
        //{
        //    Bounds = viewPort.Bounds;
        //}



    }
}
