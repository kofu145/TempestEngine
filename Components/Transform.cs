using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibraryEngine
{
    public class Transform : IComponent
    {
        public Vector2 Position;
        public Vector3 Rotation;
        public Transform()
        {
            this.Position = new Vector2(0, 0);
            this.Rotation = new Vector3(0, 0, 0);
        }

        public Transform(float x, float y, float rx, float ry, float rz) : this(new Vector2(x, y), new Vector3(rx, ry, rz)) { }

        public Transform(Vector2 position, Vector3 rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        

    }
}
