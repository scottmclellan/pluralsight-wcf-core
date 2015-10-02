using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GeoLib.Core;
using System.Data.Entity;

namespace GeoLib.Data
{
    public interface IZipCodeRepository : IDataRepository<ZipCode>
    {
        ZipCode GetByZip(string zip);
        IQueryable<ZipCode> GetByState(string state);
        IQueryable<ZipCode> GetZipsForRange(ZipCode zip, int range);
        IQueryable<ZipCode> GetZipsQueryable(Expression<Func<ZipCode, bool>> wherePredicate);
    }
}
