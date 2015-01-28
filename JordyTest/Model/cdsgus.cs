using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JordyLibary.ORM.Mappings;
using JordyLibary.ORM;

namespace Model
{
	[Table("cdsgus")]
    public class cdsgus:DataObject
    {
		private string mName;
		public static FieldInfo<string> name=new FieldInfo<string> ("cdsgus","Name");
		private string mCardNo;
		public static FieldInfo<string> cardNo=new FieldInfo<string> ("cdsgus","CardNo");
		private string mDescriot;
		public static FieldInfo<string> descriot=new FieldInfo<string> ("cdsgus","Descriot");
		private string mCtfTp;
		public static FieldInfo<string> ctfTp=new FieldInfo<string> ("cdsgus","CtfTp");
		private string mCtfId;
		public static FieldInfo<string> ctfId=new FieldInfo<string> ("cdsgus","CtfId");
		private string mGender;
		public static FieldInfo<string> gender=new FieldInfo<string> ("cdsgus","Gender");
		private string mBirthday;
		public static FieldInfo<string> birthday=new FieldInfo<string> ("cdsgus","Birthday");
		private string mAddress;
		public static FieldInfo<string> address=new FieldInfo<string> ("cdsgus","Address");
		private string mZip;
		public static FieldInfo<string> zip=new FieldInfo<string> ("cdsgus","Zip");
		private string mDirty;
		public static FieldInfo<string> dirty=new FieldInfo<string> ("cdsgus","Dirty");
		private string mDistrict1;
		public static FieldInfo<string> district1=new FieldInfo<string> ("cdsgus","District1");
		private string mDistrict2;
		public static FieldInfo<string> district2=new FieldInfo<string> ("cdsgus","District2");
		private string mDistrict3;
		public static FieldInfo<string> district3=new FieldInfo<string> ("cdsgus","District3");
		private string mDistrict4;
		public static FieldInfo<string> district4=new FieldInfo<string> ("cdsgus","District4");
		private string mDistrict5;
		public static FieldInfo<string> district5=new FieldInfo<string> ("cdsgus","District5");
		private string mDistrict6;
		public static FieldInfo<string> district6=new FieldInfo<string> ("cdsgus","District6");
		private string mFirstNm;
		public static FieldInfo<string> firstNm=new FieldInfo<string> ("cdsgus","FirstNm");
		private string mLastNm;
		public static FieldInfo<string> lastNm=new FieldInfo<string> ("cdsgus","LastNm");
		private string mDuty;
		public static FieldInfo<string> duty=new FieldInfo<string> ("cdsgus","Duty");
		private string mMobile;
		public static FieldInfo<string> mobile=new FieldInfo<string> ("cdsgus","Mobile");
		private string mTel;
		public static FieldInfo<string> tel=new FieldInfo<string> ("cdsgus","Tel");
		private string mFax;
		public static FieldInfo<string> fax=new FieldInfo<string> ("cdsgus","Fax");
		private string mEMail;
		public static FieldInfo<string> eMail=new FieldInfo<string> ("cdsgus","EMail");
		private string mNation;
		public static FieldInfo<string> nation=new FieldInfo<string> ("cdsgus","Nation");
		private string mTaste;
		public static FieldInfo<string> taste=new FieldInfo<string> ("cdsgus","Taste");
		private string mEducation;
		public static FieldInfo<string> education=new FieldInfo<string> ("cdsgus","Education");
		private string mCompany;
		public static FieldInfo<string> company=new FieldInfo<string> ("cdsgus","Company");
		private string mCTel;
		public static FieldInfo<string> cTel=new FieldInfo<string> ("cdsgus","CTel");
		private string mCAddress;
		public static FieldInfo<string> cAddress=new FieldInfo<string> ("cdsgus","CAddress");
		private string mCZip;
		public static FieldInfo<string> cZip=new FieldInfo<string> ("cdsgus","CZip");
		private string mFamily;
		public static FieldInfo<string> family=new FieldInfo<string> ("cdsgus","Family");
		private string mVersion;
		public static FieldInfo<string> version=new FieldInfo<string> ("cdsgus","Version");
		private int? mid;
		public static FieldInfo<int?> id=new FieldInfo<int?> ("cdsgus","Id");

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
		public virtual string CardNo
		{
			get
			{
				return mCardNo;
			}
			set
			{
				mCardNo=value;
				EntityState.FieldChange("CardNo");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Descriot
		{
			get
			{
				return mDescriot;
			}
			set
			{
				mDescriot=value;
				EntityState.FieldChange("Descriot");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string CtfTp
		{
			get
			{
				return mCtfTp;
			}
			set
			{
				mCtfTp=value;
				EntityState.FieldChange("CtfTp");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string CtfId
		{
			get
			{
				return mCtfId;
			}
			set
			{
				mCtfId=value;
				EntityState.FieldChange("CtfId");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Gender
		{
			get
			{
				return mGender;
			}
			set
			{
				mGender=value;
				EntityState.FieldChange("Gender");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Birthday
		{
			get
			{
				return mBirthday;
			}
			set
			{
				mBirthday=value;
				EntityState.FieldChange("Birthday");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Address
		{
			get
			{
				return mAddress;
			}
			set
			{
				mAddress=value;
				EntityState.FieldChange("Address");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Zip
		{
			get
			{
				return mZip;
			}
			set
			{
				mZip=value;
				EntityState.FieldChange("Zip");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Dirty
		{
			get
			{
				return mDirty;
			}
			set
			{
				mDirty=value;
				EntityState.FieldChange("Dirty");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string District1
		{
			get
			{
				return mDistrict1;
			}
			set
			{
				mDistrict1=value;
				EntityState.FieldChange("District1");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string District2
		{
			get
			{
				return mDistrict2;
			}
			set
			{
				mDistrict2=value;
				EntityState.FieldChange("District2");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string District3
		{
			get
			{
				return mDistrict3;
			}
			set
			{
				mDistrict3=value;
				EntityState.FieldChange("District3");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string District4
		{
			get
			{
				return mDistrict4;
			}
			set
			{
				mDistrict4=value;
				EntityState.FieldChange("District4");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string District5
		{
			get
			{
				return mDistrict5;
			}
			set
			{
				mDistrict5=value;
				EntityState.FieldChange("District5");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string District6
		{
			get
			{
				return mDistrict6;
			}
			set
			{
				mDistrict6=value;
				EntityState.FieldChange("District6");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string FirstNm
		{
			get
			{
				return mFirstNm;
			}
			set
			{
				mFirstNm=value;
				EntityState.FieldChange("FirstNm");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string LastNm
		{
			get
			{
				return mLastNm;
			}
			set
			{
				mLastNm=value;
				EntityState.FieldChange("LastNm");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Duty
		{
			get
			{
				return mDuty;
			}
			set
			{
				mDuty=value;
				EntityState.FieldChange("Duty");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Mobile
		{
			get
			{
				return mMobile;
			}
			set
			{
				mMobile=value;
				EntityState.FieldChange("Mobile");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Tel
		{
			get
			{
				return mTel;
			}
			set
			{
				mTel=value;
				EntityState.FieldChange("Tel");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Fax
		{
			get
			{
				return mFax;
			}
			set
			{
				mFax=value;
				EntityState.FieldChange("Fax");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string EMail
		{
			get
			{
				return mEMail;
			}
			set
			{
				mEMail=value;
				EntityState.FieldChange("EMail");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Nation
		{
			get
			{
				return mNation;
			}
			set
			{
				mNation=value;
				EntityState.FieldChange("Nation");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Taste
		{
			get
			{
				return mTaste;
			}
			set
			{
				mTaste=value;
				EntityState.FieldChange("Taste");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Education
		{
			get
			{
				return mEducation;
			}
			set
			{
				mEducation=value;
				EntityState.FieldChange("Education");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Company
		{
			get
			{
				return mCompany;
			}
			set
			{
				mCompany=value;
				EntityState.FieldChange("Company");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string CTel
		{
			get
			{
				return mCTel;
			}
			set
			{
				mCTel=value;
				EntityState.FieldChange("CTel");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string CAddress
		{
			get
			{
				return mCAddress;
			}
			set
			{
				mCAddress=value;
				EntityState.FieldChange("CAddress");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string CZip
		{
			get
			{
				return mCZip;
			}
			set
			{
				mCZip=value;
				EntityState.FieldChange("CZip");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Family
		{
			get
			{
				return mFamily;
			}
			set
			{
				mFamily=value;
				EntityState.FieldChange("Family");
			}
		}
		///
		///
		///
		[Column()]
		public virtual string Version
		{
			get
			{
				return mVersion;
			}
			set
			{
				mVersion=value;
				EntityState.FieldChange("Version");
			}
		}
		///
		///
		///
		[ID()]
		public virtual int? Id
		{
			get
			{
				return mid;
			}
			set
			{
				mid=value;
				EntityState.FieldChange("Id");
			}
		}
    }
}