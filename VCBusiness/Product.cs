using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCBusiness.Model;
using WComm;

namespace VCBusiness
{
    public class Product : BaseProduct
    {

        public override ReturnValue ProductDownload()
        {
            ReturnValue _result = new ReturnValue();

            #region get product list

            TProduct _tProduct = new TProduct();
            _result = _tProduct.getProductList();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "getProductList failed. \r\n " + _result.ErrMessage;

                Common.Log("getProductList---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            EntityList productList = _result.ObjectList;

            #endregion

            #region get inventory

            foreach (TProduct item in productList)
            {
                _result = VeraCore.PostProduct(VCBusiness.Common.OwnerCode, item.PartNumber, item.Name);
                if (_result.Success == false)
                {
                    if (_result.Code == -8)
                    {
                        Common.Log("Item : " + item.PartNumber + "---Already exist in VeraCore");
                        continue;
                    }

                    errorNotes = errorNotes + item.PartNumber.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                    failedRecord++;

                    Common.Log("Item : " + item.PartNumber + "  PostProduct---ER \r\n" + _result.ErrMessage);

                    continue;
                }
             

                successfulRecord++;
                Common.Log("Item : " + item.PartNumber + "---OK");
            }

            #endregion

            Common.SentAlterEmail(failedRecord, errorNotes);

            _result.Success = true;

            return _result;
        }

        public override ReturnValue UpdateInventoryStatus()
        {
            ReturnValue _result = new ReturnValue();

            Common.Connect();

            #region get product list

            TProduct _tProduct = new TProduct();
            _result = _tProduct.getProductList();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "getProductList failed. \r\n " + _result.ErrMessage;

                Common.Log("getProductList---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            EntityList productList = _result.ObjectList;

            #endregion

            #region get inventory 
            
            foreach (TProduct item in productList)
            {
               _result= VeraCore.GetInventory(VCBusiness.Common.OwnerCode, item);
               if (_result.Success == false)
               {
                   if (_result.ErrMessage.IndexOf("Can not retrieve any information") > -1)
                   {
                       _result.ErrMessage = "Can't find the product in VeraCore";
                   }
                   else
                   {
                       errorNotes = errorNotes + item.PartNumber.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                       failedRecord++;
                   }

                   Common.Log("Item : " + item.PartNumber + "  GetInventory---ER \r\n" + _result.ErrMessage);

                   continue;
               }


               

               _result = item.updateProductEstUnit(item.ProductId, item.EstUnits, item.PHOnHand);
               if (_result.Success == false)
               {
                   errorNotes = errorNotes + item.PartNumber.ToString() + "\r\n" + _result.ErrMessage + "\r\n";
                   failedRecord++;

                   Common.Log("Item : " + item.PartNumber + "  updateProductEstUnit---ER \r\n" + _result.ErrMessage);

                   continue;
               }

               successfulRecord++;
               Common.Log("Item : " + item.PartNumber + "---OK---" + item.EstUnits + "--" + item.PHOnHand);
            }

            #endregion

            #region UpdateBOOrder

            _result = _tProduct.UpdateBOOrder();
            if (_result.Success == false)
            {
                _result.Success = false;
                _result.ErrMessage = "UpdateBOOrder failed. \r\n " + _result.ErrMessage;

                Common.Log("UpdateBOOrder---ER \r\n" + _result.ErrMessage);

                return _result;
            }

            #endregion

            Common.SentAlterEmail(failedRecord, errorNotes);

            _result.Success = true;

            return _result;
        }

    }
}
