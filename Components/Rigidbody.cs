using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibraryEngine.Components
{
    public class Rigidbody : IComponent
    {
        public Vector2 Velocity;
        public bool ApplyGravity;
        public Vector2 Acceleration;

        public Rigidbody(bool applyGravity)
        {
            this.ApplyGravity = applyGravity;
        }
    }
}
