using System;
using System.Collections;
using System.Text;
using System.Reflection;

namespace WComm
{
    internal sealed class ListComparer : System.Collections.Generic .IComparer<Entity>
    {
        string PropertyName;

        public ListComparer(string propertyName)
        {
            PropertyName = propertyName;
        }


        public int Compare(Entity x, Entity y)
        {
            PropertyInfo _property = x.GetType().GetProperty(PropertyName);

            if (_property.PropertyType == typeof(Nullable<Int16>))
            {
                short? first_s = _property.GetValue(x, null) as Nullable<Int16>;
                short? second_s = _property.GetValue(y, null) as Nullable<Int16>;
                return CompareNull<Int16>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Int16))
            {
                short first_s = Convert.ToInt16(_property.GetValue(x, null));
                short second_s = Convert.ToInt16(_property.GetValue(y, null));
                return CompareNull<Int16>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Nullable<Int32>))
            {
                int? first_s = _property.GetValue(x, null) as Nullable<Int32>;
                int? second_s = _property.GetValue(y, null) as Nullable<Int32>;
                return CompareNull<Int32>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Nullable<Decimal>))
            {
                Decimal? first_s = _property.GetValue(x, null) as Nullable<Decimal>;
                Decimal? second_s = _property.GetValue(y, null) as Nullable<Decimal>;
                return CompareNull<Decimal>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Decimal))
            {
                Decimal first_s = Convert.ToDecimal(_property.GetValue(x, null));
                Decimal second_s = Convert.ToDecimal(_property.GetValue(y, null));
                return CompareNull<Decimal>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Int32))
            {
                int first_s = Convert.ToInt32(_property.GetValue(x, null));
                int second_s = Convert.ToInt32(_property.GetValue(y, null));
                return CompareNull<Int32>(first_s, second_s);
            }

            if (_property.PropertyType == typeof(Nullable<Double>))
            {
                double? first_s = _property.GetValue(x, null) as Nullable<Double>;
                double? second_s = _property.GetValue(y, null) as Nullable<Double>;
                return CompareNull<Double>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Double))
            {
                double first_s = Convert.ToDouble(_property.GetValue(x, null));
                double second_s = Convert.ToDouble(_property.GetValue(y, null));
                return CompareNull<Double>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Nullable<DateTime>))
            {
                DateTime? first_s = _property.GetValue(x, null) as Nullable<DateTime>;
                DateTime? second_s = _property.GetValue(y, null) as Nullable<DateTime>;
                return CompareNull<DateTime>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(DateTime))
            {
                DateTime first_s = Convert.ToDateTime(_property.GetValue(x, null));
                DateTime second_s = Convert.ToDateTime(_property.GetValue(y, null));
                return CompareNull<DateTime>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Nullable<Boolean>))
            {
                bool? first_s = _property.GetValue(x, null) as Nullable<Boolean>;
                bool? second_s = _property.GetValue(y, null) as Nullable<Boolean>;
                return CompareNull<Boolean>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(Boolean))
            {
                bool first_s = Convert.ToBoolean(_property.GetValue(x, null));
                bool second_s = Convert.ToBoolean(_property.GetValue(y, null));
                return CompareNull<Boolean>(first_s, second_s);
            }
            if (_property.PropertyType == typeof(String))
            {
                string first_s = string.Empty;
                string second_s = string.Empty;

                if (_property.GetValue(x, null) != null)
                    first_s = _property.GetValue(x, null).ToString();
                if (_property.GetValue(y, null) != null)
                    second_s = _property.GetValue(y, null).ToString();
                return first_s.CompareTo(second_s);
            }
            return 0;
        }

        private int CompareNull<TCompareType>(Nullable<TCompareType> first_s, Nullable<TCompareType> second_s)
            where TCompareType : struct, IComparable
        {
            if (first_s == null && second_s != null)
            {
                return -1;
            }
            if (first_s != null && second_s == null)
            {
                return 1;
            }
            if (first_s == null && second_s == null)
            {
                return 0;
            }
            return first_s.Value.CompareTo(second_s.Value);
        }
    }
}
