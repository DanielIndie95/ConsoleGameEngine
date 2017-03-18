using ConsoleGameEngine.Engine.Models;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGameEngine.Engine
{
    public abstract class GameWorld : IGameWorld
    {
        public int Layers => _entities.Max(en => en.Layer);

        private List<Entity> _entities;

        public GameWorld()
        {
            _entities = new List<Entity>();

        }

        public Entity Add(Entity entity)
        {
            _entities.Add(entity);
            entity.TargetRender = GameEngine.Screen;
            entity.Added();
            return entity;
        }

        public Entity Add(Graphics graphics, int layer = 0, int x = 0, int y = 0)
        {
            Entity graphicEntity = new Entity(x, y, graphics)
            {
                Layer = layer
            };
            return Add(graphicEntity);
        }

        public void SendToFront(Entity entity)
        {
            entity.Layer = 0;
        }

        public void SendToBack(Entity entity)
        {
            entity.Layer = Layers;
        }

        public IEnumerable<Entity> GetEntities()
        {
            return _entities;
        }

        public IEnumerable<TEntityType> GetEntities<TEntityType>()
            where TEntityType : Entity
        {
            foreach (var entity in _entities)
            {
                if (entity is TEntityType)
                    yield return entity as TEntityType;
            }
        }

        public IEnumerable<Entity> GetEntities(string type)
        {
            foreach (var entity in _entities)
            {
                if (entity.Type == type)
                    yield return entity;
            }
        }

        public virtual void Begin()
        {
        }

        public virtual void Draw()
        {

            foreach (List<Entity> layer in EntitiesByLayer)
            {
                foreach (Entity entity in layer)
                {
                    entity.Draw();
                }
            }
        }

        public virtual void Update(GameInput input)
        {
            foreach (List<Entity> layer in EntitiesByLayer)
            {
                foreach (Entity entity in layer)
                {
                    if (entity.Active)
                        entity.Update(input);
                }
            }
        }

        private List<Entity>[] EntitiesByLayer => _entities.GroupBy(en => en.Layer).OrderByDescending(en => en.Key).Select(gr => gr.ToList()).ToArray();
    }
}
