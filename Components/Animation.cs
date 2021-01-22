using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEngine.Components
{
    public class Animation : IComponent
    {
        public ConcurrentDictionary<string, FrameData> Animations; 
        public string CurrentAnimation; 
        public int CurrentFrame;
        public float FrameProgress;

        public float FrameTime => 1 / Animations[CurrentAnimation].FrameRate;

        public Animation()
        {

            this.CurrentFrame = 0;
            this.FrameProgress = 0;

            this.Animations = new ConcurrentDictionary<string, FrameData>();
            
        }
    }

    public struct FrameData
    {
        public Texture2D TextureAtlas;
        public Rectangle[] Frames;
        public int FrameRate;
        public (int Rows, int Column) Dimensions;

        public FrameData(Texture2D textureAtlas, Rectangle[] frames, int frameRate, (int Rows, int Column) dimensions)
        {
            this.TextureAtlas = textureAtlas;
            this.Frames = frames;
            this.FrameRate = frameRate;
            this.Dimensions = dimensions;
        }
    }
    
}
