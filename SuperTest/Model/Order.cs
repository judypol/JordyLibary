using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary.ORM.Mappings;
using JordyLibary.ORM;

namespace Model
{
	[Table("Order")]
    public class Order:DataObject
    {
		private int? mOrderID;
		public static FieldInfo<int?> orderID=new FieldInfo<int?> ("Order","OrderID");
		private int? mCustomerID;
		public static FieldInfo<int?> customerID=new FieldInfo<int?> ("Order","CustomerID");
		private int? mEmployeeID;
		public static FieldInfo<int?> employeeID=new FieldInfo<int?> ("Order","EmployeeID");

		///
		///
		///
		[ID()]
		public virtual int? OrderID
		{
			get
			{
				return mOrderID;
			}
			set
			{
				mOrderID=value;
				EntityState.FieldChange("OrderID");
			}
		}
		///
		///
		///
		[Column()]
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
		public virtual int? EmployeeID
		{
			get
			{
				return mEmployeeID;
			}
			set
			{
				mEmployeeID=value;
				EntityState.FieldChange("EmployeeID");
			}
		}
    }
}