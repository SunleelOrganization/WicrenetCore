using Microsoft.EntityFrameworkCore;
using ShareYunSourse.EFCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShareYunSourse.Application
{
    /// <summary>
    /// 泛型仓储类，实现泛型仓储接口。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EfRepositoryBase<T> : IRepository<T> where T : Entity  //约束在最后面。
    {
        private readonly YunSourseContext _context;
        private DbSet<T> _entities;
        /// <summary>
        /// 
        /// </summary>
        public EfRepositoryBase(YunSourseContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return Table;
        }
        public List<T> GetAllList()
        {
            return GetAll().ToList();
        }
        public List<T> GetAllList(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
        public async Task<List<T>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }
        public DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = _context.Set<T>();
                }
                return _entities;
            }
        }

        /// <summary>
        /// 实现泛型接口中的IQueryable<T>类型的 Table属性
        /// 标记为virtual是为了可以重写它
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }

        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public int InsertAndGetId(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }
                else
                {
                    Entities.Add(model);
                    _context.SaveChanges();
                    return model.Id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public void Insert(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }
                else
                {
                    this.Entities.Add(model);
                    //  this._context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertAsync(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }
                else
                {
                    await Entities.AddAsync(model);
                    // await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(T model)
        {
            try
            {
                //model为空，抛空异常
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }
                else
                {
                    //直接保存了
                    //  this._context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(T model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.Remove(model);
                // this._context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertRange(T[] model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.AddRange(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void InsertRange(IEnumerable<T> model)
        {
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("entity");
                }
                this.Entities.AddRange(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
