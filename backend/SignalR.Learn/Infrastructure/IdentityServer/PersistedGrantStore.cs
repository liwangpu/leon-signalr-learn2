using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Infrastructure.IdentityServer
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
      
        }

        public async Task RemoveAsync(string key)
        {
    
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
       
        }
    }
}
