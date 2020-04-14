using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace libExcel.Generic
{
    internal static class GenericExcelFunc
    {
        internal static CellValue ResolveCellDataValue(PropertyInfo type, Object text)
        {
            if (type.PropertyType.Name is "DateTime")
            {
                DateTime.TryParse(text.ToString(), out DateTime dt);
                if (dt == DateTime.MinValue)
                    return new CellValue("");
                else
                    return new CellValue(dt.ToString());//in futuro da sistemare  e tipizzare con formato standard
            }
            //if  (type.PropertyType is Int32 || type.PropertyType is Int64 || type.PropertyType is int)
            //    {
            //    int.TryParse(text.ToString(), out int dt);
            //    if (dt == -1)
            //        return new CellValue(text.ToString());
            //    else
            //        return new CellValue(dt);
            //}
            else
                return new CellValue((string)text.ToString());
            //else if (type.PropertyType is Int32 || type.PropertyType is Int64 || type.PropertyType is int || type.PropertyType is long)
            //{
            //    if ((long)text == 0)
            //        return new CellValue("");
            //    else
            //        return new CellValue((string)text.ToString());
            //}

        }
        internal static EnumValue<CellValues> ResolveCellDataType(PropertyInfo type)
        {
            if (type.PropertyType.Name is "DateTime")
                return CellValues.Date;
            else if (type.PropertyType.Name == "Int32" || type.PropertyType.Name == "Int64" || type.PropertyType.Name == "int" || type.PropertyType.Name == "long")
                return CellValues.Number;
            else if (type.PropertyType.Name == "bool")
                return CellValues.Boolean;
            else
                return CellValues.String;
        }
    }
}
