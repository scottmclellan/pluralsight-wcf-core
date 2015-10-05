using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GeoLib.Core;

namespace GeoLib.Data
{
    public class StateRepository : DataRepositoryBase<State, GeoLibDbContext>, IStateRepository
    {

        #region Common Where Predicates

        public static Expression<Func<State, bool>> IsPrimary(bool isPrimary)
        {
            return x => x.IsPrimaryState == isPrimary;
        }

        #endregion

        protected override DbSet<State> DbSet(GeoLibDbContext entityContext)
        {
            return entityContext.StateSet;
        }

        protected override Expression<Func<State, bool>> IdentifierPredicate(GeoLibDbContext entityContext, int id)
        {
            return (e => e.StateId == id);
        }      

        public State Get(string abbrev)
        {
            using (GeoLibDbContext entityContext = new GeoLibDbContext())
            {
                return entityContext.StateSet.FirstOrDefault(e => e.Abbreviation.ToUpper() == abbrev.ToUpper());
            }
        }

        public IQueryable<State> Get(bool primaryOnly)
        {
            return GetQuery(x => x.IsPrimaryState == primaryOnly);
        }
    }
}
