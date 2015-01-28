using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OfficeOpenXml;    //EPPlus
using System.IO;
using System.Reflection;
using System.Collections.Specialized;
using JordyLibary.Core;

namespace JordyLibary.Core
{
    public class ExcelHelper
    {
        /// <summary>
        /// 将DataTable中的数据写入Excel表中
        /// </summary>
        /// <param name="sourceTable">DataTable</param>
        /// <param name="filename">Excel路径</param>
        /// <param name="sheetName">sheetName名</param>
        public static void DataTable2Excel(DataTable sourceTable,
            string filename,string sheetName="Sheet1")
        {
            FileInfo fi = new FileInfo(filename);
            if (fi.Exists)
            {
                fi.Delete();
                fi = new FileInfo(filename);
            }
            using(ExcelPackage excel=new ExcelPackage())
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets.Add(sheetName);
                DataTable2Excel(sourceTable, ws);

                excel.Compression = CompressionLevel.BestSpeed;
                excel.SaveAs(fi);
                
            }
        }
       /// <summary>
       /// 将DataTable数据写入Excel模板中
       /// </summary>
       /// <param name="sourceTable">DataTable</param>
       /// <param name="newFileName">新的Excel表路径</param>
       /// <param name="tempFileName">模板路径</param>
       /// <param name="sheetName">模板SheetName</param>
       /// <param name="isSave">是否保存模板，当需要在多个Sheet中保存数据时，
       /// 只需要在最后一个操作Sheet中将此值设为true，其他false</param>
        public static void DataTable2Template(DataTable sourceTable,string newFileName,
            string tempFileName,string sheetName,bool isSave=true)
        {
            FileInfo tempfi = new FileInfo(tempFileName);
            FileInfo fi = new FileInfo(newFileName);

            if (fi.Exists)
            {
                fi.Delete();
                fi = new FileInfo(newFileName);
            }

            using (ExcelPackage excel = new ExcelPackage(fi,tempfi))
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets[sheetName];
                DataTable2Excel(sourceTable, ws);

                excel.Compression = CompressionLevel.BestSpeed;
                
                
                if(isSave)
                    excel.SaveAs(fi);
            }
        }
        /// <summary>
        /// 将DataTable数据写入Excel表中
        /// </summary>
        /// <param name="sourceTable">数据源</param>
        /// <param name="sheet">ExcelSheet</param>
        /// <param name="startRowIndex">开始写入的Row Number</param>
        public static void DataTable2Excel(DataTable sourceTable,
            ExcelWorksheet sheet,int startRowIndex=1)
        {
            try
            {
                if (sourceTable == null || sourceTable.Rows.Count == 0)
                    return;
                if (sheet == null)
                    throw new ArgumentNullException("Sheet is not null");

                int row = startRowIndex;
                //#region 写表头信息
                //if (!headers)
                //{
                //    for (int i = 1; i <= sourceTable.Columns.Count; i++)
                //    {
                //        sheet.SetValue(row, i + 1, sourceTable.Columns[i].ColumnName);
                //    }
                //    row++;
                //}
                //#endregion
                
                foreach(DataRow dr in sourceTable.Rows)
                {
                    for(int i=1;i<=sourceTable.Columns.Count;i++)
                    {
                        sheet.SetValue(row, i + 1, dr[i]);
                    }
                    row++;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 将实体对象写入Excel
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="source">实体集合</param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="header"></param>
        public static void Entity2Excel<T>(List<T> source,string fileName,
            string sheetName,int header=1)
        {
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                fi.Delete();
                fi = new FileInfo(fileName);
            }
            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets.Add(sheetName);
                Entity2Excel<T>(source, ws, header);

                excel.Compression = CompressionLevel.BestSpeed;
                excel.SaveAs(fi);

            }
        }
        /// <summary>
        /// 将实体对象写入Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sheet"></param>
        /// <param name="startRowIndex"></param>
        public static void Entity2Excel<T>(List<T> source,
            ExcelWorksheet sheet,int startRowIndex)
        {
            Type type = typeof(T);
            PropertyInfo[] propertys= type.GetProperties();
            int rowIndex = startRowIndex;
            int colIndex = 1;
            Dictionary<string, PropertyHandler> propertyHandlers = new Dictionary<string, PropertyHandler>();
            foreach(PropertyInfo info in propertys)
            {
                string col = info.Name;
                if (info.PropertyType.IsGenericType)
                    continue;
                sheet.SetValue(startRowIndex, colIndex, col);
                colIndex++;
                propertyHandlers.Add(info.Name, new PropertyHandler(info));
            }
            rowIndex++;
            foreach(var t in source)
            {
                colIndex=1;
                foreach(var info in propertys)
                {
                    Type propertyType = info.PropertyType;
                    if (propertyType.IsGenericType)
                        continue;

                    //object obj = info.GetValue(t, null);
                    object obj = propertyHandlers[info.Name].Get(t);
                    string val = "";
                    if (obj != null&&obj!=DBNull.Value)
                        val=obj.ToString();
                    //string val =Uility.Convert2Type<string>(obj);// (obj, propertyType);
                    sheet.SetValue(rowIndex, colIndex, val);
                    colIndex++;
                }
                rowIndex++;
            }
        }
        /// <summary>
        /// 将Excel数据库导入DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static DataTable Excel2DataTable(string fileName,
            string sheetName,int header=1)
        {
            DataTable dt = new DataTable();
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
                throw new Exception("文件不存在！");

            using(ExcelPackage excel=new ExcelPackage(fi,true))
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets[sheetName];
                #region 创建表头
                for (int i=1;i<=ws.Dimension.Columns;i++)
                {
                    string colname = Uility.ChangeType<string>(ws.GetValue(header, i));
                    if (string.IsNullOrEmpty(colname))
                        colname = "Col"+i.ToString();
                    dt.Columns.Add(colname, typeof(string));
                }
                #endregion
                #region 向表中填充数据
                for (int i=header+1;i<=ws.Dimension.Rows;i++)
                {
                    for(int j=1;j<=ws.Dimension.Columns;j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr[j - 1] = ws.GetValue(i,j);
                        dt.Rows.Add(dr);
                    }
                }
                #endregion
                
            }
            return dt;
        }
        /// <summary>
        /// 将Excel表格中的数据导入到实体类中
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="fileName">Excel文件路径</param>
        /// <param name="sheetName">Excel表</param>
        /// <param name="header">表头行</param>
        /// <returns></returns>
        public static List<T> Excel2Entity<T>(string fileName,string 
            sheetName,int header=1)
        {
            List<T> entis = new List<T>();
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
                throw new Exception("文件不存在！");
            using (ExcelPackage excel = new ExcelPackage(fi, true))
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets[sheetName];
                #region 创建表头
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                Dictionary<string, PropertyHandler> propertyHandlers = new Dictionary<string, PropertyHandler>();
                #endregion

                Dictionary<string,int> proMapColIndex=new Dictionary<string,int>();
                for(int i=1;i<=ws.Dimension.Columns;i++)
                {
                    string colName=Uility.ChangeType<string>(ws.GetValue(header,i)).ToUpper();
                    proMapColIndex.Add(colName,i);
                }

                foreach(PropertyInfo pi in propertyInfos)
                {
                    propertyHandlers.Add(pi.Name, new PropertyHandler(pi));
                }
                #region 向表中填充数据
                Type type = typeof(T);
                for (int i = header + 1; i <= ws.Dimension.Rows; i++)
                {
                    T t=Activator.CreateInstance<T>();
                    
                    foreach(PropertyInfo property in propertyInfos)
                    {
                        Type propertyType = property.PropertyType;
                        if (propertyType.IsGenericType)
                            continue;
                        object obj=ws.GetValue(i,proMapColIndex[property.Name.ToUpper()]);
                        object val = Uility.ChangeType(obj, propertyType);
                        //property.SetValue(t,val,null);
                        propertyHandlers[property.Name].Set(t, val);
                    }
                    entis.Add(t);
                }
                #endregion

            }
            return entis;
        }
        /// <summary>
        /// 将Excel表格中的数据导入到实体类中
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="fileName">Excel文件路径</param>
        /// <param name="sheetName">Excel表格</param>
        /// <param name="headerMappings">中英文对照字典，key为表头</param>
        /// <param name="headerIndex">表头行</param>
        /// <returns></returns>
        public static List<T> Excel2Entity<T>(string fileName,string sheetName,
            Dictionary<string,string> headerMappings,int headerIndex=1)
        {
            List<T> entis = new List<T>();
            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
                throw new Exception("文件不存在！");

            using(ExcelPackage excel=new ExcelPackage(fi,true))
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets[sheetName];

                try
                {
                    #region 创建表头
                    PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                    Dictionary<string, PropertyHandler> propertyHandlers = new Dictionary<string, PropertyHandler>();
                    #endregion

                    Dictionary<string, int> proMapColIndex = new Dictionary<string, int>();
                    for (int i = 1; i <= ws.Dimension.Columns; i++)
                    {
                        string colChName = Uility.ChangeType<string>(ws.GetValue(headerIndex, i)).ToUpper().Trim();
                        string colEnName = headerMappings[colChName];       //找到中英文对照表
                        proMapColIndex.Add(colEnName.ToUpper(), i);
                    }

                    foreach (PropertyInfo pi in propertyInfos)
                    {
                        propertyHandlers.Add(pi.Name, new PropertyHandler(pi));
                    }
                    #region 向表中填充数据
                    Type type = typeof(T);
                    for (int i = headerIndex + 1; i <= ws.Dimension.Rows; i++)
                    {
                        T t = Activator.CreateInstance<T>();

                        foreach (PropertyInfo property in propertyInfos)
                        {
                            Type propertyType = property.PropertyType;
                            if (propertyType.IsGenericType||!proMapColIndex.Keys.Contains(property.Name.ToUpper()))
                                continue;

                            object obj = ws.GetValue(i, proMapColIndex[property.Name.ToUpper()]);
                            object val = Uility.ChangeType(obj, propertyType);
                            //property.SetValue(t,val,null);
                            propertyHandlers[property.Name].Set(t, val);
                        }
                        entis.Add(t);
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                #endregion
            }

            return entis;
        }
    }
}
