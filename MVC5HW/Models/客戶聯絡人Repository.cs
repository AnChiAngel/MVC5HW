using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5HW.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public IQueryable<客戶聯絡人> searchALL(string sortOrder, string searchString)
        {
            var 客where = this.All().Where(p => false == p.是否已刪除).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                客where = 客where.Where(s => s.職稱.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "職稱D":
                    客where = 客where.OrderByDescending(s => s.職稱);
                    break;
                case "姓名":
                    客where = 客where.OrderBy(s => s.姓名);
                    break;
                case "姓名D":
                    客where = 客where.OrderByDescending(s => s.姓名);
                    break;
                case "Email":
                    客where = 客where.OrderBy(s => s.Email);
                    break;
                case "EmailD":
                    客where = 客where.OrderByDescending(s => s.Email);
                    break;
                case "手機":
                    客where = 客where.OrderBy(s => s.手機);
                    break;
                case "手機D":
                    客where = 客where.OrderByDescending(s => s.手機);
                    break;
                case "電話":
                    客where = 客where.OrderBy(s => s.電話);
                    break;
                case "電話D":
                    客where = 客where.OrderByDescending(s => s.電話);
                    break;
                default:
                    客where = 客where.OrderBy(s => s.職稱);
                    break;
            }
            return 客where;
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public 客戶聯絡人 EmailFind(string Email)
        {
            return this.All().Where(p => p.Email == Email).FirstOrDefault();
        }

        //修正delete
        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
            this.UnitOfWork.Commit();
        }

    }

    public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}