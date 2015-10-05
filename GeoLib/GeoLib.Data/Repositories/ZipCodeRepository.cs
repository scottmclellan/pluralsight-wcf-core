using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GeoLib.Core;

namespace GeoLib.Data
{
    public class ZipCodeRepository : DataRepositoryBase<ZipCode, GeoLibDbContext>, IZipCodeRepository
    {

        protected override DbSet<ZipCode> DbSet(GeoLibDbContext entityContext)
        {
            return entityContext.ZipCodeSet;
        }

        protected override Expression<Func<ZipCode, bool>> IdentifierPredicate(GeoLibDbContext entityContext, int id)
        {
            return (e => e.ZipCodeId == id);
        }

        public override IEnumerable<ZipCode> Get()
        {
            using (GeoLibDbContext entityContext = new GeoLibDbContext())
            {
                return entityContext.ZipCodeSet
                    .Include(e => e.State).ToFullyLoaded();
            }
        }

        public ZipCode GetByZip(string zip)
        {
            using (GeoLibDbContext entityContext = new GeoLibDbContext())
            {
                return entityContext.ZipCodeSet
                    .Include(e => e.State)
                    .Where(e => e.Zip == zip)
                    .FirstOrDefault();
            }
        }

        public IEnumerable<ZipCode> GetByState(string stateCode)
        {
            return GetQuery(e => e.State.Abbreviation == stateCode, e => e.State).ToList();
        }

        public IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range)
        {
            double degrees = range / 69.047;

            return GetQuery(e => (e.Latitude <= zip.Latitude + degrees && e.Latitude >= zip.Latitude - degrees) &&
                            (e.Longitude <= zip.Longitude + degrees && e.Longitude >= zip.Longitude - degrees), e => e.State).ToList();

        }
    }
}
