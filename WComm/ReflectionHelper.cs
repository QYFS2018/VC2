using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Data;
using System.Reflection.Emit;
using System.Web.UI;
using System.Text;

namespace WComm
{
  public class ReflectionHelper
  {
    private static readonly object _SyncLock = new object();

    public static ReflectionHelper GetInstance()
    {
      ReflectionHelper cachedHelper = null;
      lock (_SyncLock)
      {
        cachedHelper = HttpRuntime.Cache.Get("ReflectionHelper") as ReflectionHelper;
        if (cachedHelper == null)
        {
          cachedHelper = new ReflectionHelper();

          HttpRuntime.Cache.Add("ReflectionHelper",
              cachedHelper,
              null,
              Cache.NoAbsoluteExpiration,
              TimeSpan.FromMinutes(60),
              CacheItemPriority.Default,
              null);
        }
      }
      return cachedHelper;
    }

    private ReflectionHelper()
    {
    }

    Type stringType;
    public Type StringType
    {
      get
      {
        if (stringType == null)
          stringType = typeof(string);
        return stringType;
      }
    }
    Type guidType;
    public Type GuidType
    {
      get
      {
        if (guidType == null)
          guidType = typeof(Guid);
        return guidType;
      }
    }
    Type boolNullableType;
    public Type BoolNullableType
    {
      get
      {
        if (boolNullableType == null)
          boolNullableType = typeof(Nullable<bool>);
        return boolNullableType;
      }
    }
    Type boolType;
    public Type BoolType
    {
      get
      {
        if (boolType == null)
          boolType = typeof(bool);
        return boolType;
      }
    }
    Type nullableDateTimeType;
    public Type NullableDateTimeType
    {
      get
      {
        if (nullableDateTimeType == null)
          nullableDateTimeType = typeof(Nullable<DateTime>);
        return nullableDateTimeType;
      }
    }
    Type dateTimeType;
    public Type DateTimeType
    {
      get
      {
        if (dateTimeType == null)
          dateTimeType = typeof(DateTime);
        return dateTimeType;
      }
    }
    Type nullableIntType;
    public Type NullableIntType
    {
      get
      {
        if (nullableIntType == null)
          nullableIntType = typeof(Nullable<int>);
        return nullableIntType;
      }
    }
    Type intType;
    public Type IntType
    {
      get
      {
        if (intType == null)
          intType = typeof(int);
        return intType;
      }
    }
    Type nullableLongType;
    public Type NullableLongType
    {
      get
      {
        if (nullableLongType == null)
          nullableLongType = typeof(Nullable<long>);
        return nullableLongType;
      }
    }
    Type longType;
    public Type LongType
    {
      get
      {
        if (longType == null)
          longType = typeof(long);
        return longType;
      }
    }
    Type nullableDoubleType;
    public Type NullableDoubleType
    {
      get
      {
        if (nullableDoubleType == null)
          nullableDoubleType = typeof(Nullable<double>);
        return nullableDoubleType;
      }
    }
    Type doubleType;
    public Type DoubleType
    {
      get
      {
        if (doubleType == null)
          doubleType = typeof(double);
        return doubleType;
      }
    }
    Type decimalType;
    public Type DecimalType
    {
        get
        {
            if (decimalType == null)
                decimalType = typeof(decimal);
            return decimalType;
        }
    }
    Type nullabledecimalType;
    public Type NullableDecimalType
    {
        get
        {
            if (nullabledecimalType == null)
                nullabledecimalType = typeof(Nullable<decimal>);
            return nullabledecimalType;
        }
    }


    private Dictionary<Type, PropertyInfo[]> m_PropertiesForEntityCache = new Dictionary<Type, PropertyInfo[]>();
    public PropertyInfo[] GetPropertiesForEntity(Type entityType)
    {
      lock (_SyncLock)
      {
        if (!m_PropertiesForEntityCache.ContainsKey(entityType))
        {
          // m_PropertiesForEntityCache.Add(entityType, entityType.GetProperties());

          m_PropertiesForEntityCache[entityType] = entityType.GetProperties();
        }
      }
      return m_PropertiesForEntityCache[entityType];
    }

    private Dictionary<string, BindingFieldAttribute[]> m_BindingFieldAttributesCache = new Dictionary<string, BindingFieldAttribute[]>();
      public BindingFieldAttribute[] GetBindingFieldAtttributesOnProperty(Type entityType, PropertyInfo property)
      {

          StringBuilder sb = new StringBuilder();
          sb.Append(entityType.ToString());
          sb.Append(".");
          sb.Append(property.Name);


          string entityPropertyKey = sb.ToString();//edit by monom on 090611
          lock (_SyncLock)
          {
              if (!m_BindingFieldAttributesCache.ContainsKey(entityPropertyKey))
              {
                  // m_BindingFieldAttributesCache.Add(entityPropertyKey, (BindingFieldAttribute[])property.GetCustomAttributes(typeof(BindingFieldAttribute), false));

                  m_BindingFieldAttributesCache[entityPropertyKey] = (BindingFieldAttribute[])property.GetCustomAttributes(typeof(BindingFieldAttribute), false);
              }
          }
          return m_BindingFieldAttributesCache[entityPropertyKey];
      }

    private Dictionary<Type, BindingClassAttribute[]> m_BindingClassAttributesCache = new Dictionary<Type, BindingClassAttribute[]>();
    public BindingClassAttribute[] GetBindingClassAttributeOnEntity(Type entityType)
    {
      lock (_SyncLock)
      {
        if (!m_BindingClassAttributesCache.ContainsKey(entityType))
        {
          //m_BindingClassAttributesCache.Add(entityType, (BindingClassAttribute[])entityType.GetCustomAttributes(typeof(BindingClassAttribute), true));

          m_BindingClassAttributesCache[entityType] = (BindingClassAttribute[])entityType.GetCustomAttributes(typeof(BindingClassAttribute), true);
        }
      }
      return m_BindingClassAttributesCache[entityType];
    }


    private delegate object ObjectCreator();
    private Dictionary<Type, Func<object>> m_CreationDelegatesCache = new Dictionary<Type, Func<object>>();

    public Entity CreateNewEntityInstanceOf(Type entityType)
    {
      return (Entity)CreateNewInstanceOf(entityType);
    }

    public object CreateNewInstanceOf(Type type)
    {
      lock (_SyncLock)
      {
        if (!m_CreationDelegatesCache.ContainsKey(type))
        {
          ObjectCreator creatorDelegate = CreateConstructorDelegateFor(type);
          //m_CreationDelegatesCache.Add(type, new Func<object>(creatorDelegate));

          m_CreationDelegatesCache[type] = new Func<object>(creatorDelegate);
        }
      }
      Func<object> creator = m_CreationDelegatesCache[type];
      try
      {
        return creator();
      }
      catch
      {
        return Activator.CreateInstance(type);
      }

    }

    private ObjectCreator CreateConstructorDelegateFor(Type entityType)
    {
      ConstructorInfo ctor = entityType.GetConstructors()[0];

      DynamicMethod method = new DynamicMethod("CreateInstance", entityType, new Type[] { }, entityType);
      ILGenerator gen = method.GetILGenerator();
      gen.Emit(OpCodes.Newobj, ctor);
      gen.Emit(OpCodes.Ret);

      ObjectCreator creatorDelegate = (ObjectCreator)method.CreateDelegate(typeof(ObjectCreator));
      return creatorDelegate;
    }

    private Dictionary<string, Assembly> _AssemblyCache = new Dictionary<string, Assembly>();

    public object CreateObject(string typeName)
    {
      ClassInfo classInfo = ClassInfo.getClassInfo(typeName);
      Assembly assembly = GetAssemblyFromCache(classInfo.AssemblyFile, typeName, classInfo);
      Type type = assembly.GetType(classInfo.ClassType);
      return CreateNewInstanceOf(type);
    }

    private Assembly GetAssemblyFromCache(string assemblyFile, string typeName, ClassInfo _aInfo)
    {
      Assembly theAssembly = null;
      if (!_AssemblyCache.ContainsKey(assemblyFile))
      {
        if (String.IsNullOrEmpty(assemblyFile) == true)
        {
          theAssembly = Assembly.GetCallingAssembly();
        }
        else
        {
          string path = _aInfo.AssemblyFile;
          try
          {
            path = HttpContext.Current.Request.PhysicalApplicationPath + "bin\\" + path;
          }
          catch { }
          theAssembly = Assembly.LoadFrom(path);
          _AssemblyCache.Add(assemblyFile, theAssembly);
        }
      }
      return _AssemblyCache[assemblyFile];
    }
  }
}

// Claudio Lassala on 2009-04-16: the Func delegate has been introduced to .NET 3.5.
// Whenever the application is migrated to that version, this Func definition can be
// tossed out.
namespace System
{
  // Summary:
  //     Encapsulates a method that has no parameters and returns a value of the type
  //     specified by the TResult parameter.
  //
  // Type parameters:
  //   TResult:
  //     The type of the return value of the method that this delegate encapsulates.
  //
  // Returns:
  //     The return value of the method that this delegate encapsulates.
  public delegate TResult Func<TResult>();
}

