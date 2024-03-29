using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5HW.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public IQueryable<客戶銀行資訊> GetList(string keyword)
        {
            var data = this.All().Where(p => false == p.是否已刪除).AsQueryable();
            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.帳戶名稱.Contains(keyword));
            }
            return data;
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        //修正delete
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            this.UnitOfWork.Commit();
        }

    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}