using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class LookupDataService : IFriendLookupDataService
    {
        private Func<FriendOrganizerDbContext> contexCreator;

        public LookupDataService(Func<FriendOrganizerDbContext> contexCreator)
        {
            this.contexCreator = contexCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetFriendLookupAsync()
        {
            using (var ctx = contexCreator())
            {
                 var result = await ctx.Friend.AsNoTracking().Select(f => new LookupItem
                 {
                     Id = f.Id,
                     DisplayMember = f.FirstName + " " + f.LastName
                 })
                .ToListAsync();
                
                return result;
            }
        }

        public IEnumerable<LookupItem> GetFriendLookup()
        {
            using (var ctx = contexCreator())
            {
                return ctx.Friend.AsNoTracking().Select(f => new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = f.FirstName + " " + f.LastName
                })
                .ToList();
            }
        }
    }
}
