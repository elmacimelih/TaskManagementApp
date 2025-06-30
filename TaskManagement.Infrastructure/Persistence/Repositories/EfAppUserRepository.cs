using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class EfAppUserRepository : IAppUserRepository
    {

        private readonly AppDbContext _db;

        public EfAppUserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<IReadOnlyList<AppUser>> ListAllAsync()
        {
            return await _db.Users.ToListAsync();
        }
    }
}
