using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Data.Repositories
{
    public static class Extensions
    {
        public static IQueryable<State> IsPrimary(this IQueryable<State> states, bool isPrimaryState)
        {
            return states.Where(x => x.IsPrimaryState == isPrimaryState);
        }
    }
}
