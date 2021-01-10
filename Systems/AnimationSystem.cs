using Microsoft.Xna.Framework;
using TestLibraryEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

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

                if (animation.CurrentAnimation == null || animation.Animations[animation.CurrentAnimation].Frames.Length == 0)
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
                        % animation.Animations[animation.CurrentAnimation].Frames.Length;
                }

                sprite.sourceRect = animation.Animations[animation.CurrentAnimation].Frames
                    [animation.CurrentFrame % animation.Animations[animation.CurrentAnimation].Frames.Length];



            }

        }

        public void AddAnimation(Entity entity, string animationName, Texture2D textureAtlas, int frameRate, int rows, int columns)
        {
            var animation = entity.GetComponent<Animation>();

            var sourceRects = new Rectangle[rows*columns];
            var currentFrame = 0;
            for (int row=0; row < rows; row++)
            {
                for (int column=0; column < columns; column++)
                {
                    Rectangle sourceRectangle = new Rectangle(textureAtlas.Width * column, textureAtlas.Height * row, textureAtlas.Width, textureAtlas.Height);
                    sourceRects[currentFrame] = sourceRectangle;
                    currentFrame++;
                }
               
            }
            animation.Animations[animationName] = new FrameData(textureAtlas, sourceRects, frameRate, (rows, columns));

        }

        public void PlayAnimation(Entity entity, string animationName)
        {
            var sprite = entity.GetComponent<Sprite>();
            var animation = entity.GetComponent<Animation>();

            // reassign sprite's texture to new texture atlas
            sprite.Texture = animation.Animations[animationName].TextureAtlas;

            // edit frames with new list of source rects to draw with < don't do this and just use Frames in FrameData


            // current frame and frame progress becomes 0
            animation.CurrentFrame = 0;
            animation.FrameProgress = 0;

        }
    }
}
