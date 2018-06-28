using System;
using System.Data;
using WComm;

namespace VCBusiness.Model
{
     [Serializable]
     [BindingClass("Program_Email")]
    public class TProgram_EmailNu : TProgram_Email 
     {
         public TProgram_EmailNu()
          {
              
          }
          
        
          
          #region Basic Property



        [BindingField("FullText", true)]
        public override string FullText
          {
               set
               {
                    _fullText=value;
               }
               get
               {
                     return _fullText;
               }
           }
       
      
          [BindingField("Subject",true)]
        public override string Subject
          {
               set
               {
                    _subject=value;
               }
               get
               {
                     return _subject;
               }
           }
        
          [BindingField("FullHtml",true)]
        public override string FullHtml
          {
               set
               {
                    _fullHtml=value;
               }
               get
               {
                     return _fullHtml;
               }
           }
     
    
          #endregion 

          #region Extend Property

          #endregion 
          
          

    

       

     }
}