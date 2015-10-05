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

        public GeoManager()
        {

        }

        #region Repositories

        private IZipCodeRepository _zipCodeRepository;
        private IZipCodeRepository ZipCodeRepositoryInstance
        {
            get
            {
               return _zipCodeRepository ?? (_zipCodeRepository = new ZipCodeRepository());
            }
        }

        private IStateRepository _stateRepositry;
        private IStateRepository StateRepositoryInstance
        {
            get
            {
                return _stateRepositry ?? (_stateRepositry = new StateRepository());
            }
        }

        #endregion       

        #region Constructors

        public GeoManager(IZipCodeRepository zipCodeRepository) : this(zipCodeRepository, null) { }

        public GeoManager(IStateRepository stateRepository) : this(null, stateRepository) { }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _zipCodeRepository = zipCodeRepository;
            _stateRepositry = stateRepository;
        }

        #endregion Constructors

        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;

            ZipCode zipCodeEntity = ZipCodeRepositoryInstance.GetByZip(zip);

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

            var stateEntities = StateRepositoryInstance.Get(primaryOnly) ;

            if (stateEntities == null) return null;

            return stateEntities.Select(x => x.Name);

        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            var zipCodeEntities = ZipCodeRepositoryInstance.GetByState(state);

            if (zipCodeEntities == null) return null;

            return zipCodeEntities.Select(x => new ZipCodeData() { City = x.City, State = x.State.Name, ZipCode = x.Zip });
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            var zipCode = ZipCodeRepositoryInstance.GetByZip(zip);

            var zipCodeEntities = ZipCodeRepositoryInstance.GetZipsForRange(zipCode, range);

            if(zipCodeEntities == null) return null;

            return zipCodeEntities.Select(x => new ZipCodeData() { City = x.City, State = x.State.Name, ZipCode = x.Zip });
        }
    }
}
