using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibraryEngine.Systems
{
    public interface ISystem
    {
        public void Update(Scene scene, GameTime gameTime);
    }
}
