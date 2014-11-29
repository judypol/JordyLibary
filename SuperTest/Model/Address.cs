using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary.ORM.Mappings;
using JordyLibary.ORM;

namespace Model
{
	[Table("Person.Address")]
    public class Address:DataObject
    {
		private int? mAddressID;
		public static FieldInfo<int?> addressID=new FieldInfo<int?> ("Address","AddressID");
		private string mAddressLine1;
		public static FieldInfo<string> addressLine1=new FieldInfo<string> ("Address","AddressLine1");
		private string mAddressLine2;
		public static FieldInfo<string> addressLine2=new FieldInfo<string> ("Address","AddressLine2");
		private string mCity;
		public static FieldInfo<string> city=new FieldInfo<string> ("Address","City");
		private int? mStateProvinceID;
		public static FieldInfo<int?> stateProvinceID=new FieldInfo<int?> ("Address","StateProvinceID");
		private string mPostalCode;
		public static FieldInfo<string> postalCode=new FieldInfo<string> ("Address","PostalCode");
		private string mSpatialLocation;
		public static FieldInfo<string> spatialLocation=new FieldInfo<string> ("Address","SpatialLocation");
		private Guid mrowguid;
		public static FieldInfo<Guid> rowguid=new FieldInfo<Guid> ("Address","Rowguid");
		private DateTime? mModifiedDate;
		public static FieldInfo<DateTime?> modifiedDate=new FieldInfo<DateTime?> ("Address","ModifiedDate");

		///
		///
		///
		[ID()]
		public virtual int? AddressID
		{
			get
			{
				return mAddressID;
			}
			set
			{
				mAddressID=value;
				EntityState.FieldChange("AddressID");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string AddressLine1
		{
			get
			{
				return mAddressLine1;
			}
			set
			{
				mAddressLine1=value;
				EntityState.FieldChange("AddressLine1");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string AddressLine2
		{
			get
			{
				return mAddressLine2;
			}
			set
			{
				mAddressLine2=value;
				EntityState.FieldChange("AddressLine2");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string City
		{
			get
			{
				return mCity;
			}
			set
			{
				mCity=value;
				EntityState.FieldChange("City");
			}
		}
		///
		///
		///
		[Column()]
		public virtual int? StateProvinceID
		{
			get
			{
				return mStateProvinceID;
			}
			set
			{
				mStateProvinceID=value;
				EntityState.FieldChange("StateProvinceID");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string PostalCode
		{
			get
			{
				return mPostalCode;
			}
			set
			{
				mPostalCode=value;
				EntityState.FieldChange("PostalCode");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string SpatialLocation
		{
			get
			{
				return mSpatialLocation;
			}
			set
			{
				mSpatialLocation=value;
				EntityState.FieldChange("SpatialLocation");
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