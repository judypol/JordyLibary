using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary.ORM.Mappings;
using JordyLibary.ORM;

namespace Model
{
	[Table("Customer")]
    public class Customer:DataObject
    {
		private int? mCustomerID;
		public static FieldInfo<int?> customerID=new FieldInfo<int?> ("Customer","CustomerID");
		private string mCompanyName;
		public static FieldInfo<string> companyName=new FieldInfo<string> ("Customer","CompanyName");

		///
		///
		///
		[ID()]
		public virtual int? CustomerID
		{
			get
			{
				return mCustomerID;
			}
			set
			{
				mCustomerID=value;
				EntityState.FieldChange("CustomerID");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string CompanyName
		{
			get
			{
				return mCompanyName;
			}
			set
			{
				mCompanyName=value;
				EntityState.FieldChange("CompanyName");
			}
		}
    }
}