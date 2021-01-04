using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibraryEngine.Components
{
    public class Camera : IComponent
    {
        public Matrix transformMatrix;
        private Viewport view;
        private Vector2 center;
        public float Zoom { get; set; }
        
        public Rectangle Bounds { get; set; }
        public Rectangle RenderTarget { get; set; }


        public double WorldWidth { get; private set; }
        public double WorldHeight { get; private set; }

        public Color DefaultColor = Color.CornflowerBlue;


        public Camera(double worldWidth, double worldHeight, float zoom, Viewport viewPort)
        {
            this.WorldWidth = worldWidth;
            this.WorldHeight = worldHeight;
            this.Zoom = zoom;
            this.Bounds = viewPort.Bounds;
            
        }

        //public Camera (Viewport viewPort)
        //{
        //    Bounds = viewPort.Bounds;
        //}



    }
}
