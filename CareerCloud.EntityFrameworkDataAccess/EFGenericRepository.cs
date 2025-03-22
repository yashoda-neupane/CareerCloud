using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly CareerCloudContext _context;
        private readonly DbSet<T> _dbSet;
        public EFGenericRepository()
        {
            _context = new CareerCloudContext();
            _dbSet = _context.Set<T>();
        }

        

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (navigationProperties != null)
            {
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
            }
            return query.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet.Where(where);

            if (navigationProperties != null)
            {
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
            }
            return query.ToList();
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet.Where(where);

            if (navigationProperties != null)
            {
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
            }
            return query.SingleOrDefault();
        }

        public void Add(params T[] items)
        {
            ValidateItems(items);
            _dbSet.AddRange(items);
            SaveChanges();
        }

        public void Update(params T[] items)
        {
            ValidateItems(items);
            _dbSet.UpdateRange(items);
            SaveChanges();
        }

        public void Remove(params T[] items)
        {
            ValidateItems(items);
            _dbSet.RemoveRange(items);
            SaveChanges();
        }

        // Call a stored procedure
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(name));
            }

            var paramList = string.Join(", ", parameters.Select(p => $"@{p.Item1} = '{p.Item2}'"));
            var sqlQuery = $"EXEC {name} {paramList}";
            _context.Database.ExecuteSqlRaw(sqlQuery);
        }

        private void ValidateItems(T[] items)
        { 
            if (items == null || items.Length == 0)
            {
                throw new ArgumentException("No items to process.", nameof(items));
            }
        }

        private void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving changes.", ex);
            }
        }
    }
}
