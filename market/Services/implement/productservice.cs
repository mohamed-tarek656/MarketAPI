using market.Models;
using market.Services.internalinterface;
using Microsoft.EntityFrameworkCore;

namespace market.Services.implement
{
    public class productservice<t> : iproductservice<t> where t : class
    {
        private readonly marketdBContext _context;
        private readonly DbSet<t> _dbSet;
        public productservice(marketdBContext context)
        {
            this._context = context;
            _dbSet = _context.Set<t>();
        }
        public async Task<t> createasync(t entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> deleteasync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<t>> getallasync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<t?> getbyid(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> updateasync(int id, t entity)
        {
            //var exists = await _dbSet.FindAsync(id);
            //if (exists == null)
            //    return false;

            //_context.Entry(entity).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            //return true;
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
                return false;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
