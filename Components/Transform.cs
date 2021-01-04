using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestLibraryEngine.Components

{
    public class Transform : IComponent
    {
        public Vector2 Position;
        // NOTE: rotation is in radians, not degrees. 
        // (rotation is a vector3 right now but the first two don't actually do anything yet)
        public Vector3 Rotation;
        public Transform()
        {
            this.Position = new Vector2(0, 0);
            this.Rotation = new Vector3(0, 0, 0);
        }

        public Transform(float x, float y, float rx, float ry, float rz) : 
            this(new Vector2(x, y), new Vector3(rx, ry, rz)) { }

        public Transform(Vector2 position, Vector3 rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public Transform Copy()
        {
            // shallow copy, remove later pls
            return (Transform)this.MemberwiseClone();
        }

    }
}
