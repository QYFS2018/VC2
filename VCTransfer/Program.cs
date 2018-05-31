using System;
using System.Collections.Generic;
using System.Text;
using WComm;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Configuration;
using DotNetOpenMail;
using System.Globalization;
using System.Net;
using VCBusiness.VeraCoreOMS;

namespace VCTransfer
{
    static class Program
    {

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            ReturnValue _result = new ReturnValue();



            //VCBusiness.VeraCore VeraCore = new VCBusiness.VeraCore("TECN_ORD","gxo?5RRZ");
            //_result = VeraCore.PostProduct("TECNIF", "Test001", "Test001");

            string sourceKey = Encrypt.EncryptData("junjun", "2020-6-20");


            sourceKey = Encrypt.DecryptData("junjun", "oWS2Dpkw7LXDByVYTyUIAw==");
            //VCBusiness.VeraCore VeraCore = new VCBusiness.VeraCore();
            //_result = VeraCore.GetInventory("00999","00999-3");
            //_result = VeraCore.PostProduct("00999", "00999-31","aaaa009");
           // _result = VeraCore.GetOrderShipmentInfo("28000019");

            //VCBusiness.Product Product = new VCBusiness.Product();
            //_result = Product.UpdateInventoryStatus();
           

            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());

                return;
            }
            else
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["Hold"].ToString() == "Y")
                {
                    return;
                }

                string ownerCode = args[0].ToString().Trim();
                string action = args[1].ToString().Trim();

                VCBusiness.Process Process = new VCBusiness.Process();
                _result = Process.Run(ownerCode,action, "");

            }

           
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception _ex = e.ExceptionObject as Exception;

            ReturnValue _result = new ReturnValue();
            _result.Success = false;
            _result.ErrMessage = _ex.ToString();
            //_result.EffectRows = 1;

            VCBusiness.Common.Log("UnhandledException---ER \r\n" + _ex.ToString());

            VCBusiness.Common.ProcessError(_result, true);
        }
    }
}
