using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryEngine.Components;

namespace LibraryEngine.Systems
{
    public class PhysicsSystem : ISystem
    {
        public float Gravity;

        public PhysicsSystem(float gravity)
        {
            this.Gravity = gravity;
        }

        public void Update(Scene scene, GameTime gameTime)
        {

            // grab all entities with a rigidbody 
            // calculate where the entities with a rigidbody would be
            var rigidbodyEntities = scene.Entities
               .Where(e => e.HasComponent<Rigidbody>())
               .Where(e => e.HasComponent<Transform>());

            

            var updatedTransforms = new Dictionary<Guid, Transform>();
            foreach (var entity in rigidbodyEntities)
            {
                updatedTransforms[entity.id] = entity.GetComponent<Transform>().Copy();
                var rigidbody = entity.GetComponent<Rigidbody>();
                updatedTransforms[entity.id].Position =
                    ApplyMovement(entity, updatedTransforms[entity.id].Position, gameTime);

            }

            // grab all entities with transform, collider
            // grab all entities from above set that also have a rigidbody
            var collidableEntities = scene.Entities
               .Where(e => e.HasComponent<Collider>())
               .Where(e => e.HasComponent<Transform>());
            var collidableRigidbodyEntities = collidableEntities.Where(e => e.HasComponent<Rigidbody>());

            // for loop over the entities with a transform, rigidbody
            // and collider, and resolve collisions

            foreach (var self in collidableRigidbodyEntities)
            {
                var transform = self.GetComponent<Transform>();
                var newTransform = updatedTransforms[self.id];
                var collider = self.GetComponent<Collider>();
                var oldColliderInWorldSpace = new Rectangle(
                    (int)transform.Position.X + collider.BoundingBox.X - collider.BoundingBox.Width / 2,
                    (int)transform.Position.Y + collider.BoundingBox.Y - collider.BoundingBox.Height / 2,
                    collider.BoundingBox.Width,
                    collider.BoundingBox.Height
                );

                var newColliderInWorldSpace = new Rectangle(
                    (int)newTransform.Position.X + collider.BoundingBox.X - collider.BoundingBox.Width / 2,
                    (int)newTransform.Position.Y + collider.BoundingBox.Y - collider.BoundingBox.Height / 2,
                    collider.BoundingBox.Width,
                    collider.BoundingBox.Height
                );

                var collidableEntitiesByDistance = collidableEntities
                    .OrderBy
                    (e => (newTransform.Position - e.GetComponent<Transform>().Position).LengthSquared()).ToList();
                
                foreach (var other in collidableEntitiesByDistance)
                {
                    // don't collide with yourself
                    if (self.id == other.id)
                    {
                        continue;
                    } 

                    var otherTransform = other.GetComponent<Transform>();
                    var otherCollider = other.GetComponent<Collider>();
                    var otherColliderInWorldSpace = new Rectangle(
                        (int)otherTransform.Position.X + otherCollider.BoundingBox.X - otherCollider.BoundingBox.Width / 2,
                        (int)otherTransform.Position.Y + otherCollider.BoundingBox.Y - otherCollider.BoundingBox.Height / 2,
                        otherCollider.BoundingBox.Width,
                        otherCollider.BoundingBox.Height
                    );

                    // don't process collisions that aren't happening
                    if (!newColliderInWorldSpace.Intersects(otherColliderInWorldSpace))
                    {
                        continue;
                    }

                    // resolve collisions
                    if (oldColliderInWorldSpace.Bottom <= otherColliderInWorldSpace.Top)
                    {

                        // top
                        updatedTransforms[self.id].Position.Y -= (newColliderInWorldSpace.Bottom - otherColliderInWorldSpace.Top);
                        self.GetComponent<Rigidbody>().Velocity.Y = 0;
                        //self.GetComponent<Rigidbody>().Acceleration = Vector2.Zero;

                    }
                    else if (oldColliderInWorldSpace.Top >= otherColliderInWorldSpace.Bottom)
                    {
                        // bottom
                        updatedTransforms[self.id].Position.Y -= (newColliderInWorldSpace.Top - otherColliderInWorldSpace.Bottom);
                        self.GetComponent<Rigidbody>().Velocity.Y = 0;
                    }
                    else if (oldColliderInWorldSpace.Right <= otherColliderInWorldSpace.Left)
                    {
                        // left
                        updatedTransforms[self.id].Position.X -= (newColliderInWorldSpace.Right - otherColliderInWorldSpace.Left);
                        self.GetComponent<Rigidbody>().Velocity.X = 0;
                    }
                    else if (oldColliderInWorldSpace.Left >= otherColliderInWorldSpace.Right)
                    {
                        // right
                        updatedTransforms[self.id].Position.X -= (newColliderInWorldSpace.Left - otherColliderInWorldSpace.Right);
                        self.GetComponent<Rigidbody>().Velocity.X = 0;
                    }


                }

            }

            // apply movement
            foreach(var entity in rigidbodyEntities)
            {
                // apply gravity as well?
                entity.AddComponent(updatedTransforms[entity.id]);
                var rigidbody = entity.GetComponent<Rigidbody>();

            }
        }

        
        private Vector2 ApplyMovement(Entity entity, Vector2 position, GameTime gameTime)
        {
            var rigidbody = entity.GetComponent<Rigidbody>();
            // rigidbody.Acceleration = rigidbody.Force / rigidbody.Mass;
            

            rigidbody.Velocity += (rigidbody.Acceleration + (rigidbody.ApplyGravity ? new Vector2(0, Gravity) : Vector2.Zero)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            return position += rigidbody.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
