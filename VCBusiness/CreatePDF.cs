using System;
using System.Collections.Generic;
using System.Text;
using WComm;
using System.Web;
using DotNetOpenMail;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using VCBusiness.Model;


namespace VCBusiness
{
    public class TecnifibreInvoicePDF
    {
        public int PrintInvoiceSize
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationSettings.AppSettings["PrintInvoiceSize"].ToString());
            }
        }

        public ReturnValue PrintInvoice(int invoiceId)
        {

            #region Get Order and Customer Info

            ReturnValue _result = new ReturnValue();

            TOrderTF _TOrder = new TOrderTF();
            TCustomer _TCustomer = new TCustomer();
            TUser _TUser = new TUser();
            TInvoice _TInvoice = new TInvoice();
            TPaymentTerms _TPaymentTerms = new TPaymentTerms();

            _result = _TInvoice.getInvoiceById(invoiceId);
            if (!_result.Success)
            {

                return _result;
            }
            _TInvoice = _result.Object as TInvoice;

            if (string.IsNullOrEmpty(_TPaymentTerms.Description) == true)
            {
                _TPaymentTerms.Description = "Credit Card";
            }

            _result = _TOrder.getOrderById(_TInvoice.OrderId);
            if (!_result.Success)
            {

                return _result;
            }
            _TOrder = _result.Object as TOrderTF;

            _result = _TCustomer.getCustomerById(_TOrder.PWPCustomerId);
            if (!_result.Success)
            {

                return _result;
            }
            _TCustomer = _result.Object as TCustomer;

            _result = _TUser.getUserById(_TCustomer.SalesRepId);
            if (!_result.Success)
            {

                return _result;
            }
            _TUser = _result.Object as TUser;

            _result = _TPaymentTerms.getPaymentTermsById(_TCustomer.PaymentTermsId);
            if (!_result.Success)
            {

                return _result;
            }
            _TPaymentTerms = _result.Object as TPaymentTerms;
            #endregion

            #region Get Address Info

            TAddress _TAddressBillTo = new TAddress();
            _result = _TAddressBillTo.getAddressById(_TCustomer.BillToAddressId);
            if (!_result.Success)
            {

                return _result;
            }
            _TAddressBillTo = _result.Object as TAddress;



            TAddress _TAddressShipTo = new TAddress();
            _result = _TAddressShipTo.getAddressById(_TOrder.ShipToAddressId);
            if (!_result.Success)
            {

                return _result;
            }
            _TAddressShipTo = _result.Object as TAddress;

            #endregion

            #region Get OrderLineItem, Invoice

            _result = _result = new TInvoice_Line_Item().getInvoice_Line_ItemListByInvoiceId(_TInvoice.InvoiceId);
            if (!_result.Success)
            {

                return _result;
            }

            EntityList oliList = _result.ObjectList;
            oliList.Sort("OrderLineItemId", true);




            #endregion

            MemoryStream m = new MemoryStream();

            string _path = System.Configuration.ConfigurationSettings.AppSettings["InvoiceImagePath"].ToString();

            try
            {
                #region Print Order and Customer Info

                Document document = new Document(PageSize.A4, -10, 10, 50, 60);
                PdfWriter writer = PdfWriter.GetInstance(document, m);

                document.Open();
                Font font = new Font(Font.FontFamily.UNDEFINED, this.PrintInvoiceSize);
                Font font7 = new Font(Font.FontFamily.UNDEFINED, 6);
                Font fontBold = new Font(Font.FontFamily.UNDEFINED, this.PrintInvoiceSize, Font.BOLD);
                Font fontBold14 = new Font(Font.FontFamily.UNDEFINED, 12, Font.BOLD);



                PdfPTable _PdfPTable = new PdfPTable(2);
                iTextSharp.text.Image _Image = iTextSharp.text.Image.GetInstance(new Uri(_path + "\\Images\\tf-logo-pdf.JPG"));
                _Image.ScalePercent(11.0f);
                PdfPCell _PdfPCell = new PdfPCell(_Image);
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Customer Invoice", fontBold14));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPTable.AddCell(_PdfPCell);


                Phrase _Phrase = new Phrase();
                _Phrase.Add(new Phrase("                 Waterford at Blue Lagoon\r\n\r\n", font));
                _Phrase.Add(new Phrase("                 5775 Blue Lagoon Drive, Suite 110 \r\n\r\n", font));
                _Phrase.Add(new Phrase("                 Miami, Florida 33126  \r\n\r\n", font));
                _Phrase.Add(new Phrase("                 Please make Checks/Money orders payable to:\r\n\r\n", fontBold));
                _Phrase.Add(new Phrase("                 Tecnifibre USA, INC\r\n", font));
                _PdfPCell = new PdfPCell(_Phrase);
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("   ", font));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                document.Add(_PdfPTable);




                _PdfPTable = new PdfPTable(new float[] { 47f, 12, 12f, 9f, 11f, 11f });
                _Phrase = new Phrase();
                _Phrase.Add(new Paragraph("\r\n\r\nBill to Address:\r\n\r\n", fontBold14));
                _Phrase.Add(new Paragraph(
                    _TAddressBillTo.FirstName + " " + _TAddressBillTo.LastName + "\r\n\r\n" +
                    _TAddressBillTo.Address1 + "\r\n\r\n" +
                    _TAddressBillTo.City + ", " + _TAddressBillTo.StateCode + ", " + _TAddressBillTo.PostalCode
                    , font));

                _PdfPCell = new PdfPCell(_Phrase);
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPCell.Rowspan = 8;
                _PdfPCell.VerticalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_MIDDLE;
                _PdfPTable.AddCell(_PdfPCell);


                fontBold.Size = this.PrintInvoiceSize;
                _PdfPCell = new PdfPCell(new Paragraph("Customer Acct", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Sales Rep", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Order #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Order Date", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("PO #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                font.Size = this.PrintInvoiceSize;
                _PdfPCell = new PdfPCell(new Paragraph(_TCustomer.AltCustNum, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TUser.FirstName + " " + _TUser.LastName, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TOrder.AltOrderNum, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TOrder.OrderDate.Year == 1 ? "" : _TOrder.OrderDate.ToString("MM/dd/yy"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TOrder.PONumber, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Invoice #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Invoice Date", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Due Date", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Terms", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Ship Date", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.InvoiceId.ToString(), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.InvoiceDate.Year == 1 ? "" : _TInvoice.InvoiceDate.ToString("MM/dd/yy"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.DueDate.Year == 1 ? "" : _TInvoice.DueDate.ToString("MM/dd/yy"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TPaymentTerms.Description, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.ShippedDate.Year == 1 ? "" : _TInvoice.ShippedDate.ToString("MM/dd/yy"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("    ", font));
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 5;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Invoice Balance (If Paid within " + _TPaymentTerms.DiscountIfPaidInDays.ToString() + " Days)", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 3;
                _PdfPTable.AddCell(_PdfPCell);

                decimal balanceInDays = Convert.ToDecimal(_TInvoice.InvoiceAmount - _TInvoice.PaiedAmount);
                if (_TInvoice.DueDate > DateTime.Now)
                {
                    balanceInDays = balanceInDays - Convert.ToDecimal(_TInvoice.InvoiceAmount * _TPaymentTerms.Discount / 100);
                }

                if (balanceInDays < 0)
                {
                    balanceInDays = 0;
                }
                _PdfPCell = new PdfPCell(new Paragraph(balanceInDays.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 2;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("    ", font));
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 5;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Invoice Balance (IF Paid after " + _TPaymentTerms.DiscountIfPaidInDays.ToString() + " Days)", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 3;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph((_TInvoice.InvoiceAmount - _TInvoice.PaiedAmount).ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 2;
                _PdfPTable.AddCell(_PdfPCell);

                document.Add(_PdfPTable);


                _PdfPTable = new PdfPTable(1);
                _PdfPCell = new PdfPCell(new Paragraph("    ", new Font(Font.FontFamily.UNDEFINED, 1)));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                fontBold.Size = 7;
                _PdfPCell = new PdfPCell(new Paragraph("                                                                                                                  PLEASE DETACH AND RETURN TOP PORTION WITH YOUR PAYMENT", fontBold));
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthLeft = 0.0f;
                _PdfPCell.BorderWidthRight = 0.0f;
                _PdfPCell.BorderWidthBottom = 0.0f;
                _PdfPCell.BorderColorTop = new BaseColor(System.Drawing.Color.SteelBlue);
                _PdfPTable.AddCell(_PdfPCell);

                document.Add(_PdfPTable);




                fontBold.Size = this.PrintInvoiceSize;
                _PdfPTable = new PdfPTable(1);
                _PdfPCell = new PdfPCell(new Paragraph("    ", font));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                _Phrase = new Phrase();
                _Phrase.Add(new Paragraph("Ship to Address:\r\n", fontBold));
                _Phrase.Add(new Paragraph(
                    _TAddressShipTo.FirstName + " " + _TAddressShipTo.LastName + "\r\n" +
                    _TAddressShipTo.Address1 + "\r\n" +
                    _TAddressShipTo.City + ", " + _TAddressShipTo.StateCode + ", " + _TAddressShipTo.PostalCode
                    , font));
                _PdfPCell = new PdfPCell(_Phrase);
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("    ", font));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                document.Add(_PdfPTable);


                #endregion

                #region Print Order Line Item and Order Total

                _PdfPTable = new PdfPTable(new float[] { 10f, 25f, 9f, 28f, 20f, 15f, 21f, 18f, 17f, 20f });

                _PdfPCell = new PdfPCell(new Paragraph("", fontBold));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPCell.Colspan = 3;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Customer Account", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Sales Rep", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Order #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Invoice #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Invoice Date", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("PO #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Terms", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("", fontBold));
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TCustomer.AltCustNum, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TUser.FirstName + " " + _TUser.LastName, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TOrder.AltOrderNum, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.InvoiceId.ToString(), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.InvoiceDate.Year == 1 ? "" : _TInvoice.InvoiceDate.ToString("MM/dd/yy"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TOrder.PONumber, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TPaymentTerms.Description, font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Line #", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Stock Number", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Description", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.Colspan = 3;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Qty", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Standard Price", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Discount", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Sale Price", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("Total Cost", fontBold));
                _PdfPCell.BackgroundColor = new BaseColor(System.Drawing.Color.LightBlue);
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthTop = 0.5f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                StringBuilder sb = new StringBuilder();

                int index = 0;
                int count = oliList.Count;

                font.Size = this.PrintInvoiceSize;

                foreach (TInvoice_Line_Item oliItem in oliList)
                {
                    _PdfPCell = new PdfPCell(new Paragraph((++index).ToString(), font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                    _PdfPCell.BorderWidthLeft = 0.5f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.PartNumber, font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.ProductName, font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPCell.Colspan = 3;
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.Quantity.ToString(), font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_CENTER;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.StandardPrice.ToString("c"), font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.DiscountAmount.ToString("c"), font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.WholeSalePrice.ToString("c"), font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);

                    _PdfPCell = new PdfPCell(new Paragraph(oliItem.TotalCost.ToString("c"), font));
                    _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                    _PdfPCell.BorderWidthLeft = 0f;
                    _PdfPCell.BorderWidthRight = 0.5f;
                    _PdfPCell.BorderWidthTop = 0f;
                    _PdfPCell.BorderWidthBottom = (index == count ? 0.5f : 0.5f);
                    _PdfPTable.AddCell(_PdfPCell);
                }


                //font.Size = 7;
                _Phrase = new Phrase();
                _Phrase.Add(new Phrase("\r\n\r\nA 1.5% interest will be assessed on all unpaid balances 60 days past due.", font7));
                _Phrase.Add(new Phrase("\r\n\r\nFor billing or order inquiries, please call : 888-301-7878", font7));
                _Phrase.Add(new Phrase("\r\n\r\nThank you for your business!\r\n", font7));

                _PdfPCell = new PdfPCell(_Phrase);
                _PdfPCell.Colspan = 6;
                _PdfPCell.Rowspan = 5;
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPTable.AddCell(_PdfPCell);


                font.Size = this.PrintInvoiceSize;
                _PdfPCell = new PdfPCell(new Paragraph("Sub Total", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.Subtotal.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Tax", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPCell.Colspan = 3;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.Tax.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Shipping", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.Shipping.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);






                //font = new Font(Font.FontFamily.UNDEFINED, 9, Font.BOLD);
                _PdfPCell = new PdfPCell(new Paragraph("Invoice Total", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.InvoiceAmount.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                //font = new Font(Font.FontFamily.UNDEFINED, 9);
                _PdfPCell = new PdfPCell(new Paragraph("Payment Applies/Credit memo", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(_TInvoice.PaiedAmount.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(_Image);
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPCell.Colspan = 6;
                _PdfPCell.Rowspan = 3;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Invoice Balance (If Paid within " + _TPaymentTerms.DiscountIfPaidInDays.ToString() + " Days)", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph(balanceInDays.ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);


                _PdfPCell = new PdfPCell(new Paragraph("Invoice Balance (IF Paid after " + _TPaymentTerms.DiscountIfPaidInDays.ToString() + " Days)", font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_LEFT;
                _PdfPCell.Colspan = 3;
                _PdfPCell.BorderWidthLeft = 0.5f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph((_TInvoice.InvoiceAmount - _TInvoice.PaiedAmount).ToString("C"), font));
                _PdfPCell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_RIGHT;
                _PdfPCell.BorderWidthLeft = 0f;
                _PdfPCell.BorderWidthRight = 0.5f;
                _PdfPCell.BorderWidthTop = 0f;
                _PdfPCell.BorderWidthBottom = 0.5f;
                _PdfPTable.AddCell(_PdfPCell);

                _PdfPCell = new PdfPCell(new Paragraph("    ", font));
                _PdfPCell.BorderWidth = 0.0f;
                _PdfPCell.Colspan = 4;
                _PdfPTable.AddCell(_PdfPCell);


                document.Add(_PdfPTable);

                #endregion
                document.Close();
            }
            catch (DocumentException ex)
            {
                _result = new ReturnValue();
                _result.Success = false;
                _result.ErrMessage = ex.ToString();


                return _result;
            }



            try
            {
                byte[] bytes = m.GetBuffer();
                string filename = "Invoice/TFInvoice_" + invoiceId.ToString() + ".pdf";
                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();
            }




            return _result;

        }

    }
}
