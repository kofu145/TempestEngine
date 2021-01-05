using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLibraryEngine.Components;

namespace TestLibraryEngine.Systems
{
    public class XPhysicsSystem 
    {
        public delegate void InputUpdate(Scene scene, GameTime gameTime);
        public void Update(Scene scene, GameTime gameTime, InputUpdate inputUpdate)
        {
            // get previous position here to compare later 
            var collidableEntities = scene.Entities
                .Where(e => e.HasComponent<Collider>())
                .Where(e => e.HasComponent<Sprite>())
                .Where(e => e.HasComponent<Transform>());
            var prevCollidableEntities = new Entity[collidableEntities.Count()];
            for (int i = 0; i < collidableEntities.Count(); i++)
            {
                var copyEntities = collidableEntities.ToArray();
                prevCollidableEntities[i] = new Entity(copyEntities[i].id);
                prevCollidableEntities[i].AddComponent<Transform>(copyEntities.ToArray()[i].GetComponent<Transform>().Copy());
                prevCollidableEntities[i].AddComponent<Sprite>(copyEntities.ToArray()[i].GetComponent<Sprite>().Copy());
                prevCollidableEntities[i].AddComponent<Collider>(copyEntities.ToArray()[i].GetComponent<Collider>().Copy());
            }
           

            var rigidbodyEntities = scene.Entities.Where(e => e.HasComponent<Rigidbody>());
            foreach (var rigidbodyEntity in rigidbodyEntities)
            {
                AddForce(rigidbodyEntity, rigidbodyEntity.GetComponent<Rigidbody>().Velocity, gameTime);
            }
            //inputUpdate(scene, gameTime);


            foreach (var collidableEntity in collidableEntities)
            {
                var entitySprite = collidableEntity.GetComponent<Sprite>();
                var entityTransform = collidableEntity.GetComponent<Transform>();
                // update boundingbox according to their position
                collidableEntity.GetComponent<Collider>().BoundingBox = new Rectangle
                    (
                        (int)entityTransform.Position.X- entitySprite.Texture.Width/2, (int)entityTransform.Position.Y- entitySprite.Texture.Height/2,
                        entitySprite.Texture.Width, entitySprite.Texture.Height
                    );

            }

            foreach(var collidableEntity in collidableEntities)
            {

                // make a new array, populate it with entities OTHER than collidableEntity, then sort by proximity to collidableEntity
                //var collidedComparatorArray = new Entity[collidableEntities.Count()-1];
                var collidedComparatorArray = collidableEntities
                    .OrderBy
                    (e => (collidableEntity.GetComponent<Transform>().Position - e.GetComponent<Transform>().Position).LengthSquared()).ToList();
                collidedComparatorArray.RemoveAt(0);
                foreach (var targetCollidedEntity in collidedComparatorArray)
                {
                    if (collidableEntity.id != targetCollidedEntity.id)
                    {

                        if (collidableEntity.GetComponent<Collider>().BoundingBox.Intersects
                            (targetCollidedEntity.GetComponent<Collider>().BoundingBox))
                        {
                            //Debug.WriteLine("Collision detected!");
                            var prevCollidedEntity = prevCollidableEntities.Where(e => e.id == collidableEntity.id).First();
                            var prevtargetCollidedEntity = prevCollidableEntities.Where(e => e.id == targetCollidedEntity.id).First();


                            var entityHitbox = collidableEntity.GetComponent<Collider>().BoundingBox;
                            var prevEntityHitbox = prevCollidedEntity.GetComponent<Collider>().BoundingBox;


                            var targetEntityHitbox = targetCollidedEntity.GetComponent<Collider>().BoundingBox;
                            var prevTargetEntityHitbox = prevtargetCollidedEntity.GetComponent<Collider>().BoundingBox;


                            // CORNERS:
                            // top left is just position
                            // top right is x + width
                            // bottom left is y + height
                            // bottom right is x + width y + height

                            // do collision
                            if (collidableEntity.HasComponent<Rigidbody>())
                            {
                                //Debug.WriteLine("Collision Calculating!");
                                var entityRigidbody = collidableEntity.GetComponent<Rigidbody>();
                                if (prevEntityHitbox.Bottom <= targetEntityHitbox.Top)
                                {

                                    // top
                                    collidableEntity.GetComponent<Transform>().Position.Y -= (entityHitbox.Bottom - targetEntityHitbox.Top);
                                    collidableEntity.GetComponent<Rigidbody>().Velocity.Y = 0;

                                }
                                else if (prevEntityHitbox.Top >= targetEntityHitbox.Bottom)
                                {
                                    // bottom
                                    collidableEntity.GetComponent<Transform>().Position.Y -= (entityHitbox.Top - targetEntityHitbox.Bottom);
                                    collidableEntity.GetComponent<Rigidbody>().Velocity.Y = 0;
                                }
                                else if (prevEntityHitbox.Right <= targetEntityHitbox.Left)
                                {
                                    // left
                                    collidableEntity.GetComponent<Transform>().Position.X -= (entityHitbox.Right - targetEntityHitbox.Left);
                                    collidableEntity.GetComponent<Rigidbody>().Velocity.X = 0;
                                }
                                else if (prevEntityHitbox.Left >= targetEntityHitbox.Right)
                                {
                                    // right
                                    collidableEntity.GetComponent<Transform>().Position.X -= (entityHitbox.Left - targetEntityHitbox.Right);
                                    collidableEntity.GetComponent<Rigidbody>().Velocity.X = 0;
                                }


                            }



                        }
                        else
                        {
                            //Debug.WriteLine("Collision not detected!");
                        }
                    }
                }
            }

            /*
            var intersectingRectangles = collidableEntities.Where
                    (rect => collidableEntities
                    .Where(r => r != rect)
                    .Any(r => rect.GetComponent<Collider>().BoundingBox.Intersects(r.GetComponent<Collider>().BoundingBox)));*/

        }

        // private because you'd add to velocity and then reset it to 0(?)
        private void AddForce(Entity entity, Vector2 force, GameTime gameTime)
        {
            entity.GetComponent<Transform>().Position += force * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
