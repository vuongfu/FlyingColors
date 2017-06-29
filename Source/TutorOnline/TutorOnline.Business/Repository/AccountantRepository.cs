using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorOnline.Business;
using System.Web;
using TutorOnline.DataAccess;
using System.Data.Entity;

namespace TutorOnline.Business.Repository
{
    //public class AccountantRepository : BaseRepository
    //{
    //    public IEnumerable<Transaction> GetAllTrans()
    //    {
    //        var trans = _dbContext.Transactions.Include(x => x.User);
    //        return trans;
    //    }
    //    public IEnumerable<Role> GetAllRole()
    //    {
    //        return _dbContext.Roles;
    //    }

    //    public Transaction Find(int? id)
    //    {
    //        Transaction user = _dbContext.Transactions.Find(id);
    //        return user;
    //    }

    //    public void Add( Transaction trans)
    //    {
    //        _dbContext.Transactions.Add(trans);
    //        _dbContext.SaveChanges();
    //    }

    //    public void Edit(Transaction trans)
    //    {
    //        _dbContext.Entry(trans).State = EntityState.Modified;
    //        _dbContext.SaveChanges();
    //    }

    //    public void Delete(int id)
    //    {
    //        Transaction trans = _dbContext.Transactions.Find(id);
    //        _dbContext.Transactions.Remove(trans);
    //        _dbContext.SaveChanges();
    //    }

    //    public void Dispose()
    //    {
    //        _dbContext.Dispose();
    //    }
    //}
}
