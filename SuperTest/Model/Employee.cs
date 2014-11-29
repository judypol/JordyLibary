using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary.ORM.Mappings;
using JordyLibary.ORM;

namespace Model
{
	[Table("Employee")]
    public class Employee:DataObject
    {
		private int? mEmployeeID;
		public static FieldInfo<int?> employeeID=new FieldInfo<int?> ("Employee","EmployeeID");
		private string mFirstName;
		public static FieldInfo<string> firstName=new FieldInfo<string> ("Employee","FirstName");
		private string mLastName;
		public static FieldInfo<string> lastName=new FieldInfo<string> ("Employee","LastName");

		///
		///
		///
		[ID()]
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
		///
		///
		///
		[Column()]
		public virtual string FirstName
		{
			get
			{
				return mFirstName;
			}
			set
			{
				mFirstName=value;
				EntityState.FieldChange("FirstName");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string LastName
		{
			get
			{
				return mLastName;
			}
			set
			{
				mLastName=value;
				EntityState.FieldChange("LastName");
			}
		}
    }
}