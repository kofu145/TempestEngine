using Microsoft.Xna.Framework;
using TestLibraryEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibraryEngine.Systems
{
    public class AnimationSystem : ISystem
    {

        public void Update(Scene scene, GameTime gameTime)
        {
            var entitiesToAnimate = scene.Entities
                .Where(e => e.HasComponent<Sprite>())
                .Where(e => e.HasComponent<Animation>());

            foreach (var entity in entitiesToAnimate) 
            {
                var sprite = entity.GetComponent<Sprite>();
                var animation = entity.GetComponent<Animation>();
                animation.FrameProgress += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animation.Frames == null || animation.Frames.Length == 0)
                {
                    continue;
                }
                if (animation.FrameProgress > animation.FrameTime)
                {
                    animation.CurrentFrame = 
                        (
                            animation.CurrentFrame 
                            + (int)(animation.FrameProgress/animation.FrameTime)
                        ) 
                        % animation.Frames.Length;
                }

                sprite.Texture = animation.Frames[animation.CurrentFrame % animation.Frames.Length];



            }

        }
    }
}
