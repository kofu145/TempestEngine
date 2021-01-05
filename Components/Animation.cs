using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibraryEngine.Components
{
    public class Animation : IComponent
    {
        public Texture2D[] Frames;
        public int FrameRate;
        public int CurrentFrame;
        public float FrameProgress;
        public float FrameTime => 1 / FrameRate;

        public Animation(Texture2D[] frames, int frameRate)
        {
            this.Frames = frames;
            this.FrameRate = frameRate;
            this.CurrentFrame = 0;
            this.FrameProgress = 0;
        }
    }
}
