using FriendOrganizer.DataAccess;
using FriendOrganizer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private FriendOrganizerDbContext context;

        public FriendRepository(FriendOrganizerDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Friend> GetAll()
        {
            return context.Friend.ToList();   
        }

        public async Task<List<Friend>> GetAllAsync()
        {
            return await context.Friend.AsNoTracking().ToListAsync();   
        }

        public async Task<Friend> GetByIdAsync(int friendId)
        {
            return await context.Friend.SingleAsync(f => f.Id == friendId);   
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
