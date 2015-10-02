using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Data;
using GeoLib.Core;

namespace GeoLib.Services
{
    public class GeoManager : IGeoService
    {
        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;

            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);

            if (zipCodeEntity == null) return null;

            zipCodeData = new ZipCodeData()
            {
                City = zipCodeEntity.City,
                State = zipCodeEntity.State.Abbreviation,
                ZipCode = zip
            };

            return zipCodeData;
            
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {           
            IStateRepository stateRepository = new StateRepository();

            var stateEntities = stateRepository.GetQuery(StateRepository.IsPrimary(primaryOnly));

            if (stateEntities == null) return null;

            return stateEntities.Select(x => x.Name).ToList();

        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

            var zipCodeEntities = zipCodeRepository.GetByState(state);

            if (zipCodeEntities == null) return null;

            return zipCodeEntities.Select(x => new ZipCodeData() { City = x.City, State = x.State.Name, ZipCode = x.Zip }).ToList();
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            IZipCodeRepository zipCodeRepository = new ZipCodeRepository();

            var zipCode = zipCodeRepository.GetByZip(zip);

            var zipCodeEntities = zipCodeRepository.GetZipsForRange(zipCode, range);

            if(zipCodeEntities == null) return null;

            return zipCodeEntities.Select(x => new ZipCodeData() { City = x.City, State = x.State.Name, ZipCode = x.Zip }).ToList();
        }
    }
}
