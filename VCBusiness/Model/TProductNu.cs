using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("TProductNu")]
    public class TProductNu : TProduct 
     {
         public TProductNu()
          {
              
          }


         private string _name;
         private string _shortDesc;
         private string _longDesc;

         [BindingField("Name", true)]
         public string Name
         {
             set
             {
                 _name = value;
             }
             get
             {
                 return _name;
             }
         }
         [BindingField("Name", true)]
         public string ShortDesc
         {
             set
             {
                 _shortDesc = value;
             }
             get
             {
                 return _shortDesc;
             }
         }
         [BindingField("Name", true)]
         public string LongDesc
         {
             set
             {
                 _longDesc = value;
             }
             get
             {
                 return _longDesc;
             }
         }

     }
}