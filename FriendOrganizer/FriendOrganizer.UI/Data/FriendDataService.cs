using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data
{
    public class FriendDataService : IFriendDataService
    {
        private Func<FriendOrganizerDbContext> contextCreator;

        public FriendDataService(Func<FriendOrganizerDbContext> contextCreator)
        {
            this.contextCreator = contextCreator;
        }

        public IEnumerable<Friend> GetAll()
        {
            using(var ctx = contextCreator())
            {
                return ctx.Friend.AsNoTracking().ToList();
            }
        }

        public async Task<List<Friend>> GetAllAsync()
        {
            using(var ctx = contextCreator())
            {
                return await ctx.Friend.AsNoTracking().ToListAsync();
            }
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            using(var ctx = contextCreator())
            {
                return await ctx.Friend.AsNoTracking().SingleAsync(f => f.Id == friendId);
            }
        }

        public async Task SaveAsync(Friend friend)
        {
            using(var ctx = contextCreator())
            {
                ctx.Attach(friend);
                ctx.Entry(friend).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}
