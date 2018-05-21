using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCBusiness.Model;
using WComm;

namespace VCBusiness
{
    public class TecnifibreOrder : Order
    {
        protected override ReturnValue customerData(TOrder order, EntityList orderline)
        {
            ReturnValue _result = new ReturnValue();

            TOrderTF _tOrder = new TOrderTF();
            _result = _tOrder.getOrderById(order.OrderId);
            if (_result.Success == false)
            {
                return _result;
            }
            _tOrder = _result.Object as TOrderTF;


            if (_tOrder.UsingAccountNumber == false)
            {
                order.ShippingAccountNumber = "";
            }
            else
            {
                order.ShippingAccountNumber = _tOrder.ShippingAccountNumber;
            }



            return _result;
        }
    }
}
