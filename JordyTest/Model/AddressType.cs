using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary.ORM.Mappings;
using JordyLibary.ORM;

namespace Model
{
	[Table("Person.AddressType")]
    public class AddressType:DataObject
    {
		private int? mAddressTypeID;
		public static FieldInfo<int?> addressTypeID=new FieldInfo<int?> ("AddressType","AddressTypeID");
		private string mName;
		public static FieldInfo<string> name=new FieldInfo<string> ("AddressType","Name");
		private Guid mrowguid;
		public static FieldInfo<Guid> rowguid=new FieldInfo<Guid> ("AddressType","Rowguid");
		private DateTime? mModifiedDate;
		public static FieldInfo<DateTime?> modifiedDate=new FieldInfo<DateTime?> ("AddressType","ModifiedDate");

		///
		///
		///
		[ID()]
		public virtual int? AddressTypeID
		{
			get
			{
				return mAddressTypeID;
			}
			set
			{
				mAddressTypeID=value;
				EntityState.FieldChange("AddressTypeID");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName=value;
				EntityState.FieldChange("Name");
			}
		}
		///
		///
		///
		[Column()]
		public virtual Guid Rowguid
		{
			get
			{
				return mrowguid;
			}
			set
			{
				mrowguid=value;
				EntityState.FieldChange("Rowguid");
			}
		}
		///
		///
		///
		[Column()]
		public virtual DateTime? ModifiedDate
		{
			get
			{
				return mModifiedDate;
			}
			set
			{
				mModifiedDate=value;
				EntityState.FieldChange("ModifiedDate");
			}
		}
    }
}