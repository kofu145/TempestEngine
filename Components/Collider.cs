using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEngine.Components
{
    public class Collider : IComponent
    {
        public Rectangle BoundingBox { get; set; }
        // is circle or not or something
        // naabb
        // isTrigger: pass in a function?

        public Collider(Rectangle boundingBox)
        {
            this.BoundingBox = boundingBox;
        }

        public Collider Copy()
        {
            // shallow copy, remove later pls
            return (Collider)this.MemberwiseClone();
        }
    }
}
