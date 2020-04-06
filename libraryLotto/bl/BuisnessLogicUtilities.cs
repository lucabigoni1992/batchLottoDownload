using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static libraryLotto.dlm.KendoDataLogicMapping;

namespace libraryLotto.bl
{
    public static class BuisnessLogicUtilities
    {        //provare a dusare
        //var param = typeof(QueryDescriptorSort).GetProperty(par.Field);
        //        if (par.Dir == "asc")
        //            source.OrderBy(x => param.GetValue(x, null));


        //makes expression for specific prop
        private static Expression<Func<TSource, object>> GetExpression<TSource>(QueryDescriptorSort property)
        {
            var param = Expression.Parameter(typeof(TSource), property.Field+"param");
            Expression conversion = Expression.Convert(Expression.Property(param, property.Field), typeof(object));
            return Expression.Lambda<Func<TSource, object>>(conversion, param);
        }

        //makes deleget for specific prop
        private static Func<TSource, object> GetFunc<TSource>(QueryDescriptorSort propertyName)
        {
            return GetExpression<TSource>(propertyName).Compile();  //only need compiled expression
        }

        //OrderBy overload
        public static IEnumerable<Struct_Joing_AllTable> OrderBy<Struct_Joing_AllTable>(this IEnumerable<Struct_Joing_AllTable> source, QueryDescriptor kendoQuery)
        {
            ICollection<QueryDescriptorSort> propertyName = kendoQuery.Sort;
            foreach (QueryDescriptorSort par in propertyName) 
                if(par.Dir=="asc")
                    source= source.OrderBy(GetFunc<Struct_Joing_AllTable>(par));
                else
                    source=source.OrderByDescending(GetFunc<Struct_Joing_AllTable>(par));            
            return source;
        }
        public static IEnumerable<Struct_Joing_AllTable> Range<Struct_Joing_AllTable>(this IEnumerable<Struct_Joing_AllTable> source, QueryDescriptor kendoQuery)
        {
            int skip = kendoQuery.Skip;
            int take = kendoQuery.Take;
            if (source.Count() < skip)
                return null;
            return source.Skip(skip).Take(take);
        }
        internal static IEnumerable<Struct_Joing_AllTable> Where<Struct_Joing_AllTable>(this IEnumerable<Struct_Joing_AllTable> enumerable, QueryDescriptor kendoQuery)
        {
            foreach (QueryDescriptorGroup where in kendoQuery.Group)
            {
                var param = Expression.Parameter(typeof(Struct_Joing_AllTable), where.Field);
                Expression<Func<Struct_Joing_AllTable, bool>> exeWhere = null;
                if (where.Operator == "eq")
                {
                    exeWhere = Expression.Lambda<Func<Struct_Joing_AllTable, bool>>(
                         Expression.Equal(
                             Expression.Property(param, where.Field),
                            Expression.Constant(where.Value)
                        ),
                        param
                    );
                }
                enumerable = enumerable.Where(exeWhere.Compile());
            }
            return enumerable;
        }
    }
}
