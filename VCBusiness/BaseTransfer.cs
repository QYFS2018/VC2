using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WComm;

namespace VCBusiness
{
    public class BaseTransfer
    {
        private Owner _owner;
        public Owner Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
                VeraCore = new VeraCore(value.OwnerInfo["VeraCoreUserName"].ToString(), value.OwnerInfo["VeraCorePassword"].ToString());
            }
        }

        public VCBusiness.VeraCore VeraCore;

        protected int successfulRecord = 0;
        protected int failedRecord = 0;
        protected string errorNotes = "";



    }

    public class BaseOrder : BaseTransfer
    {
        public virtual ReturnValue Download()
        {
            return new ReturnValue();
        }

        public virtual ReturnValue ImportDMOrderDetail(int orderID)
        {
            return new ReturnValue();
        }

        public virtual ReturnValue UpdateShipment()
        {
            return new ReturnValue();
        }
    }

    public class BaseProduct : BaseTransfer
    {
        public virtual ReturnValue ProductDownload()
        {
            return new ReturnValue();
        }

        public virtual ReturnValue UpdateInventoryStatus()
        {
            return new ReturnValue();
        }
    }
}
