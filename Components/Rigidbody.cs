using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEngine.Components
{
    public class Rigidbody : IComponent
    {
        public Vector2 Velocity;
        public bool ApplyGravity;
        public Vector2 Acceleration;
        public Vector2 Force;
        public float Mass;
       
        public Rigidbody(bool applyGravity, float mass=1)
        {
            this.ApplyGravity = applyGravity;
            this.Mass = mass;
        }
    }
}
