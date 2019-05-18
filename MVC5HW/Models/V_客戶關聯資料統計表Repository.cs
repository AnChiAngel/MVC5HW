using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5HW.Models
{   
	public  class V_客戶關聯資料統計表Repository : EFRepository<V_客戶關聯資料統計表>, IV_客戶關聯資料統計表Repository
	{

	}

	public  interface IV_客戶關聯資料統計表Repository : IRepository<V_客戶關聯資料統計表>
	{

	}
}