using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitKidCateringApp.Extensions
{
    public abstract class Entity : IEntity
    {
        public long Id { get; set; }

        #region Equals()
        public override bool Equals(object entity)
        {
            return entity != null && entity is Entity && this == (Entity)entity;
        }
        #endregion

        #region GetHashCode()
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion

        #region operator ==
        public static bool operator ==(Entity entity1, Entity entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;
        }
        #endregion

        #region operator !=
        public static bool operator !=(Entity entity1, Entity entity2)
        {
            return (!(entity1 == entity2));
        }
        #endregion
    }

    public interface IEntity
    {
        long Id { get; set; }
    }
}
