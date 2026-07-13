using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using SimpleBol.Models.MongoDb;

public class DirectPrintBillOfLadingAsSquares : PrintDocument
{

    public bool DuplexPrinting;

    public BILLOFLADINGS Bol { get; set; } = null!;
    private SHIPPER BolShipper => Bol.Shipper!;
    private CUSTOMERS BolCustomer => Bol.ShipToCustomer!;
    private SHIPPINGLOCATIONS BolCustomerShipAddress => Bol.ShipToLocation!;
    private List<PALLETS> PALLETSs => Bol.Pallets ?? [];
    private BILLTOACCOUNTS BolBill3RdParty => Bol.BillToAccount!;
    public List<Tuple<string, string>> BolAppointmentContacts { get; set; }
    public int CurrentPage { get; set; }

    private int _currentBillOfLadingIndex = 0;

    // Create the Duplex placeholders
    private bool _addBlankPage = false;
    private int _totalPrintedPages = 0;

    // Create the Page StartPoint
    private RectangleF _printArea;
    private PointF _pageStartPoint = new PointF(0.0f, 0.0f);

    // Create the fonts we need to print with
    private readonly Font _fontTitleBold = new Font("Arial", 16, FontStyle.Bold);
    private readonly Font _fontTitleNormal = new Font("Arial", 16, FontStyle.Regular);
    private readonly Font _fontLargeBold = new Font("Arial", 14, FontStyle.Bold);
    private readonly Font _fontLargeNormal = new Font("Arial", 14, FontStyle.Regular);
    private readonly Font _fontMedium12Bold = new Font("Arial", 12, FontStyle.Bold);
    private readonly Font _fontMedium12Normal = new Font("Arial", 12);
    private readonly Font _fontMedium10Bold = new Font("Arial", 10, FontStyle.Bold);
    private readonly Font _fontMedium10Normal = new Font("Arial", 10);
    private readonly Font _fontPalletBold = new Font("Arial", 9, FontStyle.Bold);
    private readonly Font _fontPalletNormal = new Font("Arial", 9);
    private readonly Font _fontSmallBold = new Font("Arial", 8, FontStyle.Bold);
    private readonly Font _fontSmallBoldUnderline = new Font("Arial", 8, FontStyle.Bold | FontStyle.Underline);
    private readonly Font _fontSmallNormal = new Font("Arial", 8);
    private readonly Font _fontSmallItalic = new Font("Arial", 8, FontStyle.Italic);
    private readonly Font _fontTinyBold = new Font("Arial", 6, FontStyle.Bold);
    private readonly Font _fontTinyNormal = new Font("Arial", 6);

    // Brushes
    private readonly SolidBrush _blackBrush = new SolidBrush(Color.Black);
    private readonly SolidBrush _whiteBrush = new SolidBrush(Color.White);
    private readonly SolidBrush _redBrush = new SolidBrush(Color.Red);
    private readonly SolidBrush _blueBrush = new SolidBrush(Color.Blue);
    private readonly SolidBrush _greenBrush = new SolidBrush(Color.Green);
    private readonly SolidBrush _lightGreyBrush = new SolidBrush(Color.LightGray);

    // Pens
    private readonly Pen _blackPen = new Pen(Color.Black, 2);
    private readonly Pen _blackThinPen = new Pen(Color.Black, 1);
    private readonly Pen _whitePen = new Pen(Color.White, 2);
    private readonly Pen _redPen = new Pen(Color.Red, 2);
    private readonly Pen _bluePen = new Pen(Color.Blue, 2);
    private readonly Pen _greyPen = new Pen(Color.Gray, 2);

    /// <summary>
    /// Called by the Handler in the Report Dialog, to Print the report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
    {

        if (e.Graphics is not null)
        {

            // Define the printable area
            _printArea = e.PageSettings.PrintableArea;

            // Check if we need to print a blank page
            if (_addBlankPage)
            {
                PrintBlankPage(e);
                _addBlankPage = false; // Reset the flag after printing the blank page
                e.HasMorePages = true;
                return;
            }

            CurrentPage = _currentBillOfLadingIndex / 4 + 1;
            int totalPages = Math.Max(1, (int)Math.Ceiling(PALLETSs.Count / 4.0));

            // Draw the main rectangle around the page with 1/4" margins
            var pageStartPoint = new PointF(_printArea.Width - _printArea.Width + 25, _printArea.Height - _printArea.Height + 25);
            e.Graphics.DrawRectangle(_blackPen, pageStartPoint.X, pageStartPoint.Y, _printArea.Width - 50, _printArea.Height - 50);

            // Draw the first line in the rectangle
            e.Graphics.DrawLine(_blackPen, pageStartPoint.X, pageStartPoint.Y + 50, _printArea.Width - 25, _printArea.Height - _printArea.Height + 75);

            // Draw the text within the box above
            SizeF bolTextSize = e.Graphics.MeasureString("BILL OF LADING", _fontTitleBold);
            float bolTextX = _printArea.Width - 25.0f - bolTextSize.Width - 1.5f;
            float bolTextY = (pageStartPoint.Y + bolTextSize.Height) / 2;
            e.Graphics.DrawString("BILL OF LADING", _fontTitleBold, _blackBrush, bolTextX, bolTextY);

            if (Bol is not null)
            {

                // Draw the Bol Create Date inside the BOL TextBox
                string bolDate = Bol.CreatedOnUtc.ToLocalTime().ToString("MM/dd/yyyy");
                SizeF bolDateTextSize = e.Graphics.MeasureString(bolDate, _fontMedium10Normal);
                float bolDateTextX = _printArea.Width - _printArea.Width + 25 + 12.5f;
                float bolDateTextY = pageStartPoint.Y + bolTextY + 4.0f;
                e.Graphics.DrawString("Date: " + bolDate, _fontMedium10Bold, _blackBrush, bolDateTextX, bolDateTextY);

                // Draw the Bol Page Number
                string pageNumberText = $"Page: {CurrentPage} of {totalPages}";
                SizeF bolPageNumberTextSize = e.Graphics.MeasureString(pageNumberText, _fontMedium10Normal);
                float bolPageNumberTextX = _printArea.Width - 25.0f - bolPageNumberTextSize.Width - 10.5f;
                float bolPageNumberTextY = pageStartPoint.Y + bolTextY + 4.0f;
                e.Graphics.DrawString(pageNumberText, _fontMedium10Bold, _blackBrush, bolPageNumberTextX, bolPageNumberTextY);


                if (BolShipper.CompanyName is not null)
                {

                    // Draw the Company Name within the BOL TextBox
                    SizeF bolCompanyNameTextSize = e.Graphics.MeasureString("TroxellUSA", _fontTitleBold);
                    string bolCompanyNameText = "TroxellUSA";
                    float bolCompanyNameTextX = pageStartPoint.X + 10.0f;
                    float bolCompanyNameTextY = (pageStartPoint.Y + bolCompanyNameTextSize.Height) / 2.0f;
                    e.Graphics.DrawString("TroxellUSA", _fontTitleBold, _blackBrush, bolCompanyNameTextX, bolCompanyNameTextY);

                }

                // /////////////////////////////////////////////////////////////////////////////
                // Start of Ship From Address

                // Create the Ship From Header, black rectangle with white text
                var shipFromStartPoint = new PointF(pageStartPoint.X, pageStartPoint.Y + 50.0f);
                SizeF shipFromTextSize = e.Graphics.MeasureString("SHIP FROM", _fontSmallBold);
                float shipFromRectangleWidth = (_printArea.Width - 50.0f) / 2.0f;
                float shipFromRectangleHeight = 12.5f;

                // Draw the black rectangle
                e.Graphics.DrawRectangle(_blackPen, shipFromStartPoint.X, shipFromStartPoint.Y, shipFromRectangleWidth, shipFromRectangleHeight);
                e.Graphics.FillRectangle(_blackBrush, shipFromStartPoint.X, shipFromStartPoint.Y, shipFromRectangleWidth, shipFromRectangleHeight);

                // Draw the text "SHIP FROM"
                float shipFromTextX = shipFromStartPoint.X + 12.5f;
                float shipFromTextY = shipFromStartPoint.Y + (shipFromRectangleHeight - shipFromTextSize.Height) / 2.0f;
                e.Graphics.DrawString("SHIP FROM", _fontSmallBold, _whiteBrush, shipFromTextX, shipFromTextY);

                // Define address information
                var shipFromAddressLines = new List<string>();

                if (!string.IsNullOrEmpty("TroxellUSA"))
                {
                    shipFromAddressLines.Add("TroxellUSA");
                }

                if (!string.IsNullOrEmpty("18392 Enterprise Lane STE 3"))
                {
                    shipFromAddressLines.Add("18392 Enterprise Lane STE 3");
                }

                if (!string.IsNullOrEmpty(""))
                {
                    shipFromAddressLines.Add("");
                }

                // Build a single line here, City, Region, Postal Code
                string shipFromCityRegionPostal = "";

                if (!string.IsNullOrEmpty("Huntington Beach"))
                {
                    shipFromCityRegionPostal += "Huntington Beach";
                }

                if (!string.IsNullOrEmpty("CA"))
                {
                    shipFromCityRegionPostal += " " + "CA";
                }

                if (!string.IsNullOrEmpty("92648"))
                {
                    shipFromCityRegionPostal += " " + "92648";
                }

                if (!string.IsNullOrEmpty("US"))
                {
                    shipFromCityRegionPostal += " " + "US";
                }

                shipFromAddressLines.Add(shipFromCityRegionPostal);

                if (!string.IsNullOrEmpty("Ray Huerta") && !string.IsNullOrEmpty("(714) 847-0880"))
                {
                    shipFromAddressLines.Add("Contact: " + "Ray" + ": " + "(714) 847-0880");
                }

                // Calculate position for drawing address lines
                float shipFromAddressStartX = shipFromStartPoint.X;
                float shipFromAddressStartY = shipFromStartPoint.Y + shipFromRectangleHeight + 10.0f; // Adjust vertical position

                foreach (string line in shipFromAddressLines)
                {
                    SizeF lineSize = e.Graphics.MeasureString(line, _fontMedium10Normal);
                    float lineX = shipFromAddressStartX + 12.5f; // Indent the address
                    float lineY = shipFromAddressStartY;

                    e.Graphics.DrawString(line, _fontMedium10Normal, _blackBrush, lineX, lineY);

                    // Increment the Y position for the next line
                    shipFromAddressStartY += lineSize.Height + 1.5f; // Add a small vertical spacing
                }

                // End of Ship From Address
                // //////////////////////////////////////////////////////////////////////////////

                // //////////////////////////////////////////////////////////////////////////////
                // Start of Ship To Address

                if (BolCustomerShipAddress is not null)
                {

                    // Create the Ship To Header, black rectangle with white text
                    var shipToStartPoint = new PointF(pageStartPoint.X, pageStartPoint.Y + 190.0f);
                    SizeF shipToTextSize = e.Graphics.MeasureString("SHIP TO", _fontSmallBold);
                    float shipToRectangleWidth = (_printArea.Width - 50.0f) / 2.0f;
                    float shipToRectangleHeight = 12.5f;

                    e.Graphics.DrawRectangle(_blackPen, shipToStartPoint.X, shipToStartPoint.Y, shipToRectangleWidth, shipToRectangleHeight);
                    e.Graphics.FillRectangle(_blackBrush, shipToStartPoint.X, shipToStartPoint.Y, shipToRectangleWidth, shipToRectangleHeight);

                    // Draw the text Ship To
                    float shipToTextX = shipToStartPoint.X + 12.5f;
                    float shipToTextY = shipToStartPoint.Y + (shipToRectangleHeight - shipToTextSize.Height) / 2.0f;
                    e.Graphics.DrawString("SHIP TO", _fontSmallBold, _whiteBrush, shipToTextX, shipToTextY);

                    // Define address information
                    var shipToAddressLines = new List<string>();

                    if (!string.IsNullOrEmpty(BolCustomer.CompanyName))
                    {
                        shipToAddressLines.Add(BolCustomer.CompanyName);
                    }

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.Address1))
                    {
                        shipToAddressLines.Add(BolCustomerShipAddress.Address1);
                    }

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.Address2))
                    {
                        shipToAddressLines.Add(BolCustomerShipAddress.Address2);
                    }

                    // Build a single line here, City, Region, Postal Code
                    string shipToCityRegionPostal = "";

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.City))
                    {
                        shipToCityRegionPostal += BolCustomerShipAddress.City;
                    }

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.RegionAbbr))
                    {
                        shipToCityRegionPostal += " " + BolCustomerShipAddress.RegionAbbr;
                    }

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.PostalCode))
                    {
                        shipToCityRegionPostal += " " + BolCustomerShipAddress.PostalCode;
                    }

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.CountryAbbr))
                    {
                        shipToCityRegionPostal += " " + BolCustomerShipAddress.CountryAbbr;
                    }

                    shipToAddressLines.Add(shipToCityRegionPostal);

                    if (!string.IsNullOrEmpty(BolCustomerShipAddress.ContactName) && !string.IsNullOrEmpty(BolCustomerShipAddress.ContactPhone))
                    {
                        shipToAddressLines.Add("Contact: " + BolCustomerShipAddress.ContactName + ": " + BolCustomerShipAddress.ContactPhone);
                    }

                    // Calculate position for drawing address lines
                    float shipToAddressStartX = shipToStartPoint.X;
                    float shipToAddressStartY = shipToStartPoint.Y + shipToRectangleHeight + 10.0f; // Adjust vertical position

                    foreach (string line in shipToAddressLines)
                    {
                        SizeF lineSize = e.Graphics.MeasureString(line, _fontMedium10Normal);
                        float lineX = shipToAddressStartX + 12.5f; // Indent the address
                        float lineY = shipToAddressStartY;

                        e.Graphics.DrawString(line, _fontMedium10Normal, _blackBrush, lineX, lineY);

                        // Increment the Y position for the next line
                        shipToAddressStartY += lineSize.Height + 1.5f; // Add a small vertical spacing
                    }

                    // Bring a Liftgate If Needed
                    if (Bol is not null)
                    {

                        var liftgateLabelSize = new SizeF(0.0f, 0.0f);

                        if (Bol.ShipToLocation?.LiftGateRequired == true)
                        {
                            liftgateLabelSize = e.Graphics.MeasureString("Liftgate Required: X", _fontMedium10Bold);
                            float liftgateLabelX = shipToStartPoint.X + 12.5f;
                            float liftgateLabelY = shipToStartPoint.Y + shipToRectangleHeight + 103.0f;
                            e.Graphics.DrawString("Liftgate Required: X", _fontMedium10Bold, _redBrush, liftgateLabelX, liftgateLabelY);
                        }

                        if (Bol.ShipToLocation?.AppointmentRequired == true)
                        {

                            SizeF appointmentLabelSize = e.Graphics.MeasureString("Appointment Required: X", _fontMedium10Bold);
                            float appointmentLabelX = shipToStartPoint.X + (liftgateLabelSize.Width == 0.0f ? 12.5f : liftgateLabelSize.Width + 36.5f);
                            float appointmentLabelY = shipToStartPoint.Y + shipToRectangleHeight + 103.0f;
                            e.Graphics.DrawString("Appointment Required: X", _fontMedium10Bold, _redBrush, appointmentLabelX, appointmentLabelY);

                            // Add the contact name and phone or email address
                            if (!string.IsNullOrEmpty(Bol.ShipToLocation?.ContactName) && !string.IsNullOrEmpty(Bol.ShipToLocation?.ContactPhone))
                            {
                                SizeF appointmentContactNameLabelSize = e.Graphics.MeasureString("Appt Contact: " + Bol.ShipToLocation?.ContactName + " at " + Bol.ShipToLocation?.ContactPhone, _fontSmallBold);
                                float appointmentContactNameLabelX = shipToStartPoint.X + 12.5f;
                                float appointmentContactNameLabelY = shipToStartPoint.Y + shipToRectangleHeight + 121.0f;
                                e.Graphics.DrawString("Appt Contact: " + Bol.ShipToLocation?.ContactName + " at " + Bol.ShipToLocation?.ContactPhone, _fontSmallBold, _blackBrush, appointmentContactNameLabelX, appointmentContactNameLabelY);
                            }

                        }

                    }

                }

                // End of Ship To Address
                // ///////////////////////////////////////////////////////////////////////////

                // Draw the top middle line that divides the left side ShipTo and ShipFrom from the right side of BOL information
                var topCenterVerticalLineStartPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 50.0f);
                var topCenterVerticalLineStopPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 325.0f);
                e.Graphics.DrawLine(_blackPen, topCenterVerticalLineStartPoint, topCenterVerticalLineStopPoint);

                // We are now below the top of the main rectangle, working the right side column

                // ///////////////////////////////////////////////////////////////////////////
                // Start of BOL Information

                // Bol Number
                SizeF bolNumberLabelSize = e.Graphics.MeasureString("Bill of Lading Number: ", _fontMedium10Bold);
                float bolNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                float bolNumberLabelY = pageStartPoint.Y + 58.0f;
                e.Graphics.DrawString("Bill of Lading Number: ", _fontMedium10Bold, _blackBrush, bolNumberLabelX, bolNumberLabelY);

                // Human-facing BOL number (BolId is the MongoDB record identifier).
                if (!string.IsNullOrEmpty(Bol.BolNumber))
                {
                    SizeF bolNumberTextSize = e.Graphics.MeasureString(Bol.BolNumber, _fontMedium12Bold);
                    float bolNumberTextX = bolNumberLabelX + bolNumberLabelSize.Width + 3.125f;
                    float bolNumberTextY = pageStartPoint.Y + 58.0f;
                    e.Graphics.DrawString(Bol.BolNumber, _fontMedium12Normal, _blackBrush, bolNumberTextX, bolNumberTextY);
                }

                // Ship From PO Number
                SizeF bolPoNumberLabelSize = e.Graphics.MeasureString("TroxellUSA" + " PO Id: ", _fontMedium10Bold);
                float bolPoNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                float bolPoNumberLabelY = pageStartPoint.Y + 58.0f + bolPoNumberLabelSize.Height + 1.5f;
                e.Graphics.DrawString("TroxellUSA" + " PO Id: ", _fontMedium10Bold, _blackBrush, bolPoNumberLabelX, bolPoNumberLabelY);

                // PoNumber
                if (!string.IsNullOrEmpty(Bol.ShipperReferenceNumber))
                {
                    SizeF bolPoNumberTextSize = e.Graphics.MeasureString(Bol.ShipperReferenceNumber, _fontMedium12Bold);
                    float bolPoNumberTextX = bolPoNumberLabelX + bolPoNumberLabelSize.Width + 3.125f;
                    float bolPoNumberTextY = pageStartPoint.Y + 56.0f + bolPoNumberLabelSize.Height + 1.5f;
                    e.Graphics.DrawString(Bol.ShipperReferenceNumber, _fontMedium12Normal, _blackBrush, bolPoNumberTextX, bolPoNumberTextY);
                }

                // Measure the size of the "Order #: " label with the company name
                SizeF bolOrderNumberLabelSize = e.Graphics.MeasureString("TroxellUSA" + " Order #: ", _fontMedium10Bold);

                // Set the position for the label
                float bolOrderNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                float bolOrderNumberLabelY = pageStartPoint.Y + 78.0f + bolOrderNumberLabelSize.Height + 1.5f;

                // Draw the label for the order number
                e.Graphics.DrawString("TroxellUSA" + " Order #: ", _fontMedium10Bold, _blackBrush, bolOrderNumberLabelX, bolOrderNumberLabelY);

                // Check if the OrderNumber is not null
                if (!string.IsNullOrEmpty(Bol.OrderNumber))
                {
                    // Measure the size of the OrderNumber text
                    SizeF bolOrderNumberTextSize = e.Graphics.MeasureString(Bol.OrderNumber, _fontMedium12Normal);

                    // Set the position for the OrderNumber text
                    float bolOrderNumberTextX = bolOrderNumberLabelX + bolOrderNumberLabelSize.Width + 3.125f;
                    float bolOrderNumberTextY = pageStartPoint.Y + 76.0f + bolOrderNumberLabelSize.Height + 1.5f;

                    // Draw the OrderNumber text
                    e.Graphics.DrawString(Bol.OrderNumber, _fontMedium12Normal, _blackBrush, bolOrderNumberTextX, bolOrderNumberTextY);
                }

                // Draw a line to finish the top BOL Reference Container, on the right side
                var bolReferenceContainerStartPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 122.0f);
                var bolReferenceContainerStopPoint = new PointF(_printArea.Width - 50.0f / 2.0f, pageStartPoint.Y + 122.0f);
                e.Graphics.DrawLine(_blackPen, bolReferenceContainerStartPoint, bolReferenceContainerStopPoint);

                // End of BOL Information
                // //////////////////////////////////////////////////////////////////////////////

                // //////////////////////////////////////////////////////////////////////////////
                // Start a new Container for the Selected Shipper or Carrier

                if (BolShipper is not null)
                {
                    SizeF carrierNameLabelSize = e.Graphics.MeasureString("Carrier Name: ", _fontMedium10Bold);
                    float carrierNameLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                    float carrierNameLabelY = pageStartPoint.Y + 109.0f + carrierNameLabelSize.Height + 1.5f;
                    e.Graphics.DrawString("Carrier Name: ", _fontMedium10Bold, _blackBrush, carrierNameLabelX, carrierNameLabelY);

                    if (BolShipper.CompanyName is not null)
                    {
                        SizeF carrierNameTextSize = e.Graphics.MeasureString(BolShipper.CompanyName, _fontMedium12Normal);
                        float carrierNameTextX = carrierNameLabelX + carrierNameLabelSize.Width + 3.125f;
                        float carrierNameTextY = pageStartPoint.Y + 107.0f + carrierNameLabelSize.Height + 1.5f;
                        e.Graphics.DrawString(BolShipper.CompanyName, _fontMedium12Normal, _blackBrush, carrierNameTextX, carrierNameTextY);
                    }
                }

                // Shipper City, State
                if (Bol.ShipperQuoteNumber is not null)
                {
                    SizeF carrierQuoteNumberLabelSize = e.Graphics.MeasureString("Location: ", _fontMedium10Bold);
                    float carrierQuoteNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                    float carrierQuoteNumberLabelY = pageStartPoint.Y + 128.0f + carrierQuoteNumberLabelSize.Height + 1.5f;
                    e.Graphics.DrawString("Location: ", _fontMedium10Bold, _blackBrush, carrierQuoteNumberLabelX, carrierQuoteNumberLabelY);

                    SizeF carrierQuoteNumberTextSize = e.Graphics.MeasureString(BolShipper.City + " " + BolShipper.RegionShortName, _fontMedium12Normal);
                    float carrierQuoteNumberTextX = carrierQuoteNumberLabelX + carrierQuoteNumberLabelSize.Width + 3.125f;
                    float carrierQuoteNumberTextY = pageStartPoint.Y + 123.0f + carrierQuoteNumberTextSize.Height + 1.5f;
                    e.Graphics.DrawString(BolShipper.City + " " + BolShipper.RegionShortName, _fontMedium12Normal, _blackBrush, carrierQuoteNumberTextX, carrierQuoteNumberTextY);
                }

                // Shipper Phone
                SizeF carrierQuotePriceLabelSize = e.Graphics.MeasureString("Phone: ", _fontMedium10Bold);
                float carrierQuotePriceLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                float carrierQuotePriceLabelY = pageStartPoint.Y + 147.0f + carrierQuotePriceLabelSize.Height + 1.5f;
                e.Graphics.DrawString("Phone: ", _fontMedium10Bold, _blackBrush, carrierQuotePriceLabelX, carrierQuotePriceLabelY);

                SizeF carrierQuotePriceTextSize = e.Graphics.MeasureString(BolShipper.Phone2, _fontMedium10Normal);
                float carrierQuotePriceTextX = carrierQuotePriceLabelX + carrierQuotePriceLabelSize.Width + 3.125f;
                float carrierQuotePriceTextY = pageStartPoint.Y + 145.0f + carrierQuotePriceTextSize.Height + 1.5f;
                e.Graphics.DrawString(BolShipper.Phone2, _fontMedium12Normal, _blackBrush, carrierQuotePriceTextX, carrierQuotePriceTextY);

                // End of Carrier Selected
                // /////////////////////////////////////////////////////////////////////////////

                // /////////////////////////////////////////////////////////////////////////////
                // Third Party Freight Charges
                // Create the 3rd Part Freight Charge Header, black rectangle with white text
                // This line matches Ship To at 190F Y Axis

                var freightChargeStartPoint = new PointF(topCenterVerticalLineStartPoint.X, pageStartPoint.Y + 190.0f);
                SizeF freightChargeTextSize = e.Graphics.MeasureString("THIRD PARTY/FREIGHT CHARGES BILL TO", _fontSmallBold);
                float freightChargeRectangleWidth = (_printArea.Width - 50.0f) / 2.0f;
                float freightChargeRectangleHeight = 12.5f;

                e.Graphics.DrawRectangle(_blackPen, freightChargeStartPoint.X, freightChargeStartPoint.Y, freightChargeRectangleWidth, freightChargeRectangleHeight);
                e.Graphics.FillRectangle(_blackBrush, freightChargeStartPoint.X, freightChargeStartPoint.Y, freightChargeRectangleWidth, freightChargeRectangleHeight);

                // Draw the text "THIRD PARTY/FREIGHT CHARGES BILL TO"
                float freightChargeTextX = freightChargeStartPoint.X + 12.5f;
                float freightChargeTextY = freightChargeStartPoint.Y + (freightChargeRectangleHeight - freightChargeTextSize.Height) / 2.0f;
                e.Graphics.DrawString("THIRD PARTY/FREIGHT CHARGES BILL TO", _fontSmallBold, _whiteBrush, freightChargeTextX, freightChargeTextY);


                // Third Party Billing Information
                if (BolBill3RdParty is not null)
                {

                    // Define address information
                    var billTo3rdPartyAddressLines = new List<string>();

                    if (!string.IsNullOrEmpty(BolBill3RdParty.CompanyName))
                    {
                        billTo3rdPartyAddressLines.Add(BolBill3RdParty.CompanyName);
                    }

                    if (!string.IsNullOrEmpty(BolBill3RdParty.Address1))
                    {
                        billTo3rdPartyAddressLines.Add(BolBill3RdParty.Address1);
                    }

                    // Build a single line here, City, Region, Postal Code
                    string billTo3rdPartyCityRegionPostal = "";

                    if (!string.IsNullOrEmpty(BolBill3RdParty.City))
                    {
                        billTo3rdPartyCityRegionPostal += " " + BolBill3RdParty.City;
                    }

                    if (!string.IsNullOrEmpty(BolBill3RdParty.RegionLongName))
                    {
                        billTo3rdPartyCityRegionPostal += " " + BolBill3RdParty.RegionAbbr;
                    }

                    if (!string.IsNullOrEmpty(BolBill3RdParty.PostalCode))
                    {
                        billTo3rdPartyCityRegionPostal += " " + BolBill3RdParty.PostalCode;
                    }

                    if (!string.IsNullOrEmpty(BolBill3RdParty.CountryLongName))
                    {
                        billTo3rdPartyCityRegionPostal += " " + BolBill3RdParty.CountryAbbr;
                    }

                    billTo3rdPartyAddressLines.Add(billTo3rdPartyCityRegionPostal);

                    // Calculate position for drawing address lines
                    float billTo3rdPartyAddressStartX = freightChargeStartPoint.X;
                    float billTo3rdPartyAddressStartY = freightChargeStartPoint.Y + freightChargeRectangleHeight + 6.0f; // Adjust vertical position

                    foreach (string line in billTo3rdPartyAddressLines)
                    {
                        SizeF lineSize = e.Graphics.MeasureString(line, _fontMedium10Normal);
                        float lineX = billTo3rdPartyAddressStartX + 12.5f; // Indent the address
                        float lineY = billTo3rdPartyAddressStartY;

                        e.Graphics.DrawString(line, _fontMedium10Normal, _blackBrush, lineX, lineY);

                        // Increment the Y position for the next line
                        billTo3rdPartyAddressStartY += lineSize.Height + 1.0f; // Add a small vertical spacing
                    }

                }

                // Draw a line to finish the center of the Bill To Account Container, on the right side
                var freightChargeTermsContainerStartPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 270.0f);
                var freightChargeTermsContainerStopPoint = new PointF(_printArea.Width - 50.0f / 2.0f, pageStartPoint.Y + 270.0f);
                e.Graphics.DrawLine(_blackPen, freightChargeTermsContainerStartPoint, freightChargeTermsContainerStopPoint);

                // Freight Charge Terms Label
                SizeF freightChargeTermsLabelSize = e.Graphics.MeasureString("Freight charges are prepaid unless marked otherwise", _fontSmallBold);
                float freightChargeTermsLabelX = topCenterVerticalLineStartPoint.X + 12.5f;
                float freightChargeTermsLabelY = pageStartPoint.Y + 260.0f + freightChargeTermsLabelSize.Height + 1.5f;
                e.Graphics.DrawString("Freight charges are prepaid unless marked otherwise", _fontSmallBold, _blackBrush, freightChargeTermsLabelX, freightChargeTermsLabelY);

                if (Bol.FreightPrePaid == true)
                {

                    // Freight Charge Terms Method
                    SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ______X______   Collect ___________   3rd Party _________", _fontSmallBold);
                    float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5f;
                    float freightChargeTermsTextY = pageStartPoint.Y + 290.0f + freightChargeTermsLabelSize.Height + 1.5f;
                    e.Graphics.DrawString("Prepaid _____X_____   Collect ___________   3rd Party _________", _fontSmallBold, _blackBrush, freightChargeTermsTextX, freightChargeTermsTextY);
                }

                else if (Bol.COD == true && Bol.CodAmount > 0.0m)
                {

                    // Freight Charge Terms Method
                    SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ___________   Collect _____X_____   3rd Party _________", _fontSmallBold);
                    float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5f;
                    float freightChargeTermsTextY = pageStartPoint.Y + 290.0f + freightChargeTermsLabelSize.Height + 1.5f;
                    e.Graphics.DrawString("Prepaid ___________   Collect _____X_____   3rd Party _________", _fontSmallBold, _redBrush, freightChargeTermsTextX, freightChargeTermsTextY);
                }

                else if (Bol.ThirdPartyBilling == true && string.IsNullOrEmpty(Bol.BillToId))
                {

                    // Freight Charge Terms Method
                    SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ___________   Collect ___________   3rd Party ____X____", _fontSmallBold);
                    float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5f;
                    float freightChargeTermsTextY = pageStartPoint.Y + 290.0f + freightChargeTermsLabelSize.Height + 1.5f;
                    e.Graphics.DrawString("Prepaid ___________   Collect _____________   3rd Party ____X____", _fontSmallBold, _blackBrush, freightChargeTermsTextX, freightChargeTermsTextY);
                }

                else
                {

                    // Freight Charge Terms Method
                    SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ___________   Collect ___________   3rd Party _________", _fontSmallBold);
                    float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5f;
                    float freightChargeTermsTextY = pageStartPoint.Y + 290.0f + freightChargeTermsLabelSize.Height + 1.5f;
                    e.Graphics.DrawString("Prepaid _________   Collect ___________   3rd Party ___________", _fontSmallBold, _blackBrush, freightChargeTermsTextX, freightChargeTermsTextY);

                }

                // End of 3rd Party Billing
                // //////////////////////////////////////////////////////////////////////////////

                // //////////////////////////////////////////////////////////////////////////////
                // We are now 325 units below the top of the rectangle, draw a horizontal line
                // //////////////////////////////////////////////////////////////////////////////

                // Packages, Pallets, Containers
                // Create the BOL Data Header, black rectangle with white text
                // This line matches Ship To at 325F Y Axis
                var shipmentDetailsStartPoint = new PointF(pageStartPoint.X, pageStartPoint.Y + 325.0f);
                SizeF shipmentDetailsTextSize = e.Graphics.MeasureString("SHIPMENT DETAILS", _fontSmallBold);
                float shipmentDetailsRectangleWidth = _printArea.Width - 50.0f;
                float shipmentDetailsRectangleHeight = 12.5f;

                e.Graphics.DrawRectangle(_blackPen, shipmentDetailsStartPoint.X, shipmentDetailsStartPoint.Y, shipmentDetailsRectangleWidth, shipmentDetailsRectangleHeight);
                e.Graphics.FillRectangle(_blackBrush, shipmentDetailsStartPoint.X, shipmentDetailsStartPoint.Y, shipmentDetailsRectangleWidth, shipmentDetailsRectangleHeight);

                // Draw the text Shipment Details Text
                float shipmentDetailsTextX = shipmentDetailsStartPoint.X + 12.5f;
                float shipmentDetailsTextY = shipmentDetailsStartPoint.Y + (shipmentDetailsRectangleHeight - shipmentDetailsTextSize.Height) / 2.0f;
                e.Graphics.DrawString("SHIPMENT DETAILS", _fontSmallBold, _whiteBrush, shipmentDetailsTextX, shipmentDetailsTextY);

                // //////////////////////////////////////////////////////////////////////////////
                // Shipment Details - Pallet, Package Details
                // //////////////////////////////////////////////////////////////////////////////

                // Draw a line to finish the center of the Shipping Details Container
                var endOfShipmentDetailsContainerStartPoint = new PointF(_printArea.Width - _printArea.Width + 25.0f, pageStartPoint.Y + 750.0f);
                var endOfShipmentDetailsContainerStopPoint = new PointF(_printArea.Width - 25.0f, pageStartPoint.Y + 750.0f);
                e.Graphics.DrawLine(_blackPen, endOfShipmentDetailsContainerStartPoint, endOfShipmentDetailsContainerStopPoint);

                // Moved the Pallet, Package generator to a separate function, which I will call at the end of this page generation or rendering
                // 03/06/2024 jKirkerx

                // End of Shipment Details
                // //////////////////////////////////////////////////////////////////////////////

                // //////////////////////////////////////////////////////////////////////////////
                // Shipment Value declaration and COD Payment Terms
                // //////////////////////////////////////////////////////////////////////////////

                // Shipment Value Declaration Text String
                SizeF shipValueDeclarationTextSize = e.Graphics.MeasureString("Where the rate is dependent on value, shippers are required to state specifically in writing the" + Constants.vbCrLf + "agreed or declared value of the property as follows:" + Constants.vbCrLf + "The agreed or declared value of the property is specifically stated by the shipper to be not" + Constants.vbCrLf + "exceeding", _fontTinyNormal);
                float shipValueDeclarationTextX = pageStartPoint.X + 6.125f;
                float shipValueDeclarationTextY = pageStartPoint.Y + 715.0f + shipValueDeclarationTextSize.Height + 1.5f;
                e.Graphics.DrawString("Where the rate is dependent on value, shippers are required to state specifically in writing the" + Constants.vbCrLf + "agreed or declared value of the property as follows:" + Constants.vbCrLf + "The agreed or declared value of the property is specifically stated by the shipper to be not" + Constants.vbCrLf + "exceeding", _fontTinyNormal, _blackBrush, shipValueDeclarationTextX, shipValueDeclarationTextY);

                // Draw a vertical line to create two containers, Limited Liability and Shipper Signature
                var shipValueDeclarationVerticalLineContainerStartPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 750.0f);
                var shipValueDeclarationVerticalLineContainerStopPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 815.0f);
                e.Graphics.DrawLine(_blackPen, shipValueDeclarationVerticalLineContainerStartPoint, shipValueDeclarationVerticalLineContainerStopPoint);

                // Draw an X if the shipment is COD
                if (Bol.COD == true && Bol.CodAmount > 0.0m)
                {
                    // Write the COD Payment Amount
                    SizeF codAmountDeclarationTextSize = e.Graphics.MeasureString("COD AMOUNT: " + Bol.CodAmount.ToString("c"), _fontMedium10Bold);
                    float codAmountDeclarationTextX = _printArea.Width / 2.0f + 6.125f;
                    float codAmountDeclarationTextY = pageStartPoint.Y + 740.0f + codAmountDeclarationTextSize.Height + 1.5f;
                    e.Graphics.DrawString("COD AMOUNT: " + Bol.CodAmount.ToString("c"), _fontMedium10Bold, _redBrush, codAmountDeclarationTextX, codAmountDeclarationTextY);
                }
                else
                {
                    // Write the blank COD Payment Amount
                    SizeF codAmountDeclarationTextSize = e.Graphics.MeasureString("COD AMOUNT: $__________________________________", _fontMedium10Bold);
                    float codAmountDeclarationTextX = _printArea.Width / 2.0f + 6.125f;
                    float codAmountDeclarationTextY = pageStartPoint.Y + 740.0f + codAmountDeclarationTextSize.Height + 1.5f;
                    e.Graphics.DrawString("COD AMOUNT: $__________________________________", _fontMedium10Bold, _blackBrush, codAmountDeclarationTextX, codAmountDeclarationTextY);
                }

                // Write the COD Fee Terms Label
                SizeF codAmountFeeTermsTextSize = e.Graphics.MeasureString("Fee Terms:", _fontMedium10Bold);
                float codAmountFeeTermsTextX = _printArea.Width / 2.0f + 6.125f;
                float codAmountFeeTermsTextY = pageStartPoint.Y + 760.0f + codAmountFeeTermsTextSize.Height + 1.5f;
                e.Graphics.DrawString("Fee Terms:", _fontMedium10Bold, _blackBrush, codAmountFeeTermsTextX, codAmountFeeTermsTextY);

                // Write the COD Fee Term Choice Collect
                SizeF codAmountFeeTermsCollectTextSize = e.Graphics.MeasureString("Collect:", _fontMedium10Bold);
                float codAmountFeeTermsCollectTextX = _printArea.Width / 2.0f + codAmountFeeTermsTextSize.Width + 31.0f;
                float codAmountFeeTermsCollectTextY = pageStartPoint.Y + 760.0f + codAmountFeeTermsCollectTextSize.Height + 1.5f;
                e.Graphics.DrawString("Collect", _fontMedium10Bold, _blackBrush, codAmountFeeTermsCollectTextX, codAmountFeeTermsCollectTextY);

                // Draw the Fee Terms COD Checkbox
                var codAmountFeeTermsCollectCheckboxStartPoint = new PointF(codAmountFeeTermsCollectTextX + codAmountFeeTermsCollectTextSize.Width + 6.125f, pageStartPoint.Y + 760.0f + codAmountFeeTermsCollectTextSize.Height + 2.5f);
                e.Graphics.DrawRectangle(_blackPen, codAmountFeeTermsCollectCheckboxStartPoint.X, codAmountFeeTermsCollectCheckboxStartPoint.Y, 12, 12);

                // Draw an X if the shipment is COD
                if (Bol.COD == true && Bol.CodAmount > 0.0m)
                {
                    SizeF codAmountFeeTermsCollectXTextSize = e.Graphics.MeasureString("X", _fontMedium10Bold);
                    float codAmountFeeTermsCollectXTextX = codAmountFeeTermsCollectTextX + codAmountFeeTermsCollectTextSize.Width + 6.0f;
                    float codAmountFeeTermsCollectXTextY = pageStartPoint.Y + 760.0f + codAmountFeeTermsCollectXTextSize.Height + 1.0f;
                    e.Graphics.DrawString("X", _fontMedium10Bold, _redBrush, codAmountFeeTermsCollectXTextX, codAmountFeeTermsCollectXTextY);
                }

                // Write the COD Fee Term Choice Prepaid
                SizeF codAmountFeeTermsPrepaidTextSize = e.Graphics.MeasureString("Prepaid", _fontMedium10Bold);
                float codAmountFeeTermsPrepaidTextX = _printArea.Width / 2.0f + codAmountFeeTermsTextSize.Width + 120.0f;
                float codAmountFeeTermsPrepaidTextY = pageStartPoint.Y + 760.0f + codAmountFeeTermsPrepaidTextSize.Height + 1.5f;
                e.Graphics.DrawString("Prepaid", _fontMedium10Bold, _blackBrush, codAmountFeeTermsPrepaidTextX, codAmountFeeTermsPrepaidTextY);

                // Draw the Fee Terms Freight Prepaid Checkbox
                var codAmountFeeTermsPrepaidCheckboxStartPoint = new PointF(codAmountFeeTermsPrepaidTextX + codAmountFeeTermsPrepaidTextSize.Width + 6.125f, pageStartPoint.Y + 760.0f + codAmountFeeTermsCollectTextSize.Height + 2.5f);
                e.Graphics.DrawRectangle(_blackPen, codAmountFeeTermsPrepaidCheckboxStartPoint.X, codAmountFeeTermsPrepaidCheckboxStartPoint.Y, 12, 12);

                if (Bol.FreightPrePaid)
                {
                    // Draw an X indicating the Shipment is Freight PrePaid
                    SizeF codAmountFeeTermsPrepaidXTextSize = e.Graphics.MeasureString("X", _fontMedium10Bold);
                    float codAmountFeeTermsPrepaidXTextX = codAmountFeeTermsPrepaidTextX + codAmountFeeTermsPrepaidTextSize.Width + 6.125f;
                    float codAmountFeeTermsPrepaidXTextY = pageStartPoint.Y + 760.0f + codAmountFeeTermsPrepaidXTextSize.Height + 1.5f;
                    e.Graphics.DrawString("X", _fontMedium10Bold, _blackBrush, codAmountFeeTermsPrepaidXTextX, codAmountFeeTermsPrepaidXTextY);
                }

                // Write the Customer Check Acceptable
                SizeF customerCheckAcceptableTextSize = e.Graphics.MeasureString("Customer check acceptable:", _fontSmallNormal);
                float customerCheckAcceptableTextX = _printArea.Width / 2.0f + 6.125f;
                float customerCheckAcceptableTextY = pageStartPoint.Y + 785.0f + customerCheckAcceptableTextSize.Height + 1.5f;
                e.Graphics.DrawString("Customer check acceptable:", _fontSmallNormal, _blackBrush, customerCheckAcceptableTextX, customerCheckAcceptableTextY);

                // Draw the Customer Check Acceptable Checkbox
                var customerCheckAcceptableCheckboxStartPoint = new PointF(customerCheckAcceptableTextX + customerCheckAcceptableTextSize.Width + 6.125f, customerCheckAcceptableTextY + 2.0f);
                e.Graphics.DrawRectangle(_blackPen, customerCheckAcceptableCheckboxStartPoint.X, customerCheckAcceptableCheckboxStartPoint.Y, 8, 8);

                // /////////////////////////////////////////////////////////////////////////////
                // /// Shipment Liability Limitation
                // /////////////////////////////////////////////////////////////////////////////

                // Draw a line to start the shipment liability limitation and Shipper Signature
                var shipmentLiabilityContainerTitleStartPoint = new PointF(_printArea.Width - _printArea.Width + 25.0f, pageStartPoint.Y + 816.0f);
                var shipmentLiabilityContainerTitleStopPoint = new PointF(_printArea.Width - 25.0f, pageStartPoint.Y + 816.0f);
                e.Graphics.DrawLine(_blackPen, shipmentLiabilityContainerTitleStartPoint, shipmentLiabilityContainerTitleStopPoint);

                // Liability Text String
                SizeF shipperSignatureLiabilityClauseTextSize = e.Graphics.MeasureString("Liability Limitation for loss or damage in this shipment may be applicable. See 49 U.S.C. 14706(c)(1)(A) and (B).", _fontMedium10Bold);
                float shipperSignatureLiabilityClauseTextX = pageStartPoint.X + 6.125f;
                float shipperSignatureLiabilityClauseTextY = pageStartPoint.Y + 803.0f + shipperSignatureLiabilityClauseTextSize.Height + 1.5f;
                e.Graphics.DrawString("Liability Limitation for loss or damage in this shipment may be applicable. See 49 U.S.C. 14706(c)(1)(A) and (B).", _fontMedium10Bold, _redBrush, shipperSignatureLiabilityClauseTextX, shipperSignatureLiabilityClauseTextY);

                // Draw a line to start the shipment liability limitation and Shipper Signature
                var shipmentLiabilityContainerStartPoint = new PointF(_printArea.Width - _printArea.Width + 25.0f, pageStartPoint.Y + 844.0f);
                var shipmentLiabilityContainerStopPoint = new PointF(_printArea.Width - 25.0f, pageStartPoint.Y + 844.0f);
                e.Graphics.DrawLine(_blackPen, shipmentLiabilityContainerStartPoint, shipmentLiabilityContainerStopPoint);

                // Write the legal text for limited liability
                SizeF legalLiabilityTextSize = e.Graphics.MeasureString("RECEIVED, subject to individually determined rates or contracts that have been agreed upon in" + Constants.vbCrLf + "writing between the carrier and shipper, if applicable, otherwise to the rates, classifications" + Constants.vbCrLf + "and rules that have been established by the carrier and are available to the shipper, on request," + Constants.vbCrLf + "and to all applicable state and federal regulations.", _fontTinyNormal);
                float legalLiabilityTextX = pageStartPoint.X + 6.125f;
                float legalLiabilityTextY = pageStartPoint.Y + 808.0f + legalLiabilityTextSize.Height + 1.5f;
                e.Graphics.DrawString("RECEIVED, subject to individually determined rates or contracts that have been agreed upon in" + Constants.vbCrLf + "writing between the carrier and shipper, if applicable, otherwise to the rates, classifications" + Constants.vbCrLf + "and rules that have been established by the carrier and are available to the shipper, on request," + Constants.vbCrLf + "and to all applicable state and federal regulations.", _fontTinyNormal, _blackBrush, legalLiabilityTextX, legalLiabilityTextY);

                // Draw a vertical line to create two containers, Limited Liability and Shipper Signature
                var shipmentLiabilityCodVerticalLineContainerStartPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 844.0f);
                var shipmentLiabilityCodVerticalLineContainerStopPoint = new PointF(_printArea.Width / 2.0f, pageStartPoint.Y + 910.0f);
                e.Graphics.DrawLine(_blackPen, shipmentLiabilityCodVerticalLineContainerStartPoint, shipmentLiabilityCodVerticalLineContainerStopPoint);

                // Write the Shipper signature for not delivering unless payment is made
                SizeF carrierCodAgreementTextSize = e.Graphics.MeasureString("The carrier shall not make delivery of this shipment without payment of freight" + Constants.vbCrLf + "and all other lawful charges.", _fontTinyNormal);
                float carrierCodAgreementTextX = _printArea.Width / 2.0f + 6.125f;
                float carrierCodAgreementTextY = pageStartPoint.Y + 828.0f + carrierCodAgreementTextSize.Height + 1.5f;
                e.Graphics.DrawString("The carrier shall not make delivery of this shipment without payment of freight" + Constants.vbCrLf + "and all other lawful charges.", _fontTinyNormal, _blackBrush, carrierCodAgreementTextX, carrierCodAgreementTextY);

                // Write the Shipper signature for not delivering unless payment is made
                SizeF carrierCodAgreementSignatureTextSize = e.Graphics.MeasureString("Shipper Signature _________________________________________", _fontSmallBold);
                float carrierCodAgreementSignatureTextX = _printArea.Width / 2.0f + 6.125f;
                float carrierCodAgreementSignatureTextY = pageStartPoint.Y + 868.0f + carrierCodAgreementTextSize.Height + 1.5f;
                e.Graphics.DrawString("Shipper Signature _________________________________________", _fontSmallBold, _blackBrush, carrierCodAgreementSignatureTextX, carrierCodAgreementSignatureTextY);

                // /// End of Shipment Liability Limitations
                // ///////////////////////////////////////////////////////////////////////////////////


                // ////////////////////////////////////////////////////////////////////////////////////
                // /// Shipment Terms and Signatures
                // ////////////////////////////////////////////////////////////////////////////////////

                // Draw a line to start the Shipper Signature Container
                var startOfShipperSignatureContainerStartPoint = new PointF(_printArea.Width - _printArea.Width + 25.0f, pageStartPoint.Y + 910.0f);
                var startOfShipperSignatureContainerStopPoint = new PointF(_printArea.Width - 25.0f, pageStartPoint.Y + 910.0f);
                e.Graphics.DrawLine(_blackPen, startOfShipperSignatureContainerStartPoint, startOfShipperSignatureContainerStopPoint);

                // Shipper Signature
                SizeF shipperSignatureLabelSize = e.Graphics.MeasureString("SHIPPER SIGNATURE / DATE", _fontSmallBold);
                float shipperSignatureLabelX = pageStartPoint.X + 12.5f;
                float shipperSignatureLabelY = pageStartPoint.Y + 898.0f + shipperSignatureLabelSize.Height + 1.5f;
                e.Graphics.DrawString("SHIPPER SIGNATURE / DATE", _fontSmallBold, _blackBrush, shipperSignatureLabelX, shipperSignatureLabelY);

                // Shipper Signature Legal Terms
                SizeF shipperSignatureLegalTermsLabelSize = e.Graphics.MeasureString("This is to certify that the above named materials are properly" + Constants.vbCrLf + "classified, packaged, marked and labeled, and are in proper" + Constants.vbCrLf + "condition for transportation according to the applicable" + Constants.vbCrLf + "regulations of the DOT.", _fontTinyNormal);
                float shipperSignatureLegalTermsLabelX = pageStartPoint.X + 6.125f;
                float shipperSignatureLegalTermsLabelY = pageStartPoint.Y + 889.0f + shipperSignatureLegalTermsLabelSize.Height + 1.5f;
                e.Graphics.DrawString("This is to certify that the above named materials are properly" + Constants.vbCrLf + "classified, packaged, marked and labeled, and are in proper" + Constants.vbCrLf + "condition for transportation according to the applicable" + Constants.vbCrLf + "regulations of the DOT.", _fontTinyNormal, _blackBrush, shipperSignatureLegalTermsLabelX, shipperSignatureLegalTermsLabelY);
                // ////////////////////////////////////////////////////////////////////////////////////
                // /// End of Shipper Signature Container
                // ////////////////////////////////////////////////////////////////////////////////////

                // ////////////////////////////////////////////////////////////////////////////////////
                // Draw a vertical line in the Freight Loaded and Counted Container 1/3 across the page width
                var shipperSignatureVerticalLineContainerStartPoint = new PointF(_printArea.Width / 3.0f, pageStartPoint.Y + 910.0f);
                var shipperSignatureVerticalLineContainerStopPoint = new PointF(_printArea.Width / 3.0f, _printArea.Height - 25.0f);
                e.Graphics.DrawLine(_blackPen, shipperSignatureVerticalLineContainerStartPoint, shipperSignatureVerticalLineContainerStopPoint);

                // ////////////////////////////////////////////////////////////////////////////////////
                // /// End of Freight Loaded and Counted Container
                // ////////////////////////////////////////////////////////////////////////////////////

                // Trailer Loaded //////////                   
                SizeF trailerLoadedLabelSize = e.Graphics.MeasureString("TRAILER LOADED", _fontSmallBold);
                float trailerLoadedLabelX = _printArea.Width / 3.0f + 6.125f;
                float trailerLoadedLabelY = pageStartPoint.Y + 898.0f + trailerLoadedLabelSize.Height + 1.5f;
                e.Graphics.DrawString("TRAILER LOADED", _fontSmallBold, _blackBrush, trailerLoadedLabelX, trailerLoadedLabelY);

                // Draw 2 rectangles as checkboxes

                // Draw the Shipper Loaded Checkbox
                var shipperLoadedCheckboxStartPoint = new PointF(_printArea.Width / 3.0f + 8.125f, pageStartPoint.Y + 920.0f + trailerLoadedLabelSize.Height + 1.5f);
                e.Graphics.DrawRectangle(_blackPen, shipperLoadedCheckboxStartPoint.X, shipperLoadedCheckboxStartPoint.Y, 12.0f, 12.0f);

                // Draw the Carrier Loaded Checkbox
                var driverLoadedCheckboxStartPoint = new PointF(_printArea.Width / 3.0f + 8.125f, pageStartPoint.Y + 945.0f + trailerLoadedLabelSize.Height + 1.5f);
                e.Graphics.DrawRectangle(_blackPen, driverLoadedCheckboxStartPoint.X, driverLoadedCheckboxStartPoint.Y, 12.0f, 12.0f);

                // Draw the Text for the Checkboxes

                // Shipper Loaded Text
                SizeF shipperLoadedTextSize = e.Graphics.MeasureString("By Shipper", _fontSmallNormal);
                float shipperLoadedTextX = _printArea.Width / 3.0f + 24.25f;
                float shipperLoadedTextY = pageStartPoint.Y + 919.0f + shipperLoadedTextSize.Height + 1.5f;
                e.Graphics.DrawString("By Shipper", _fontSmallNormal, _blackBrush, shipperLoadedTextX, shipperLoadedTextY);

                // Driver Loaded Text
                SizeF driverLoadedTextSize = e.Graphics.MeasureString("By Driver", _fontSmallNormal);
                float driverLoadedTextX = _printArea.Width / 3.0f + 24.25f;
                float driverLoadedTextY = pageStartPoint.Y + 944.0f + driverLoadedTextSize.Height + 1.5f;
                e.Graphics.DrawString("By Driver", _fontSmallNormal, _blackBrush, driverLoadedTextX, driverLoadedTextY);

                // ////////////////////////////////////////////////////////////////////////////////////
                // Freight Counted //////////                    
                SizeF freightCountedLabelSize = e.Graphics.MeasureString("FREIGHT COUNTED", _fontSmallBold);
                float freightCountedLabelX = _printArea.Width / 2.0f + 6.125f;
                float freightCountedLabelY = pageStartPoint.Y + 898.0f + freightCountedLabelSize.Height + 1.5f;
                e.Graphics.DrawString("FREIGHT COUNTED", _fontSmallBold, _blackBrush, freightCountedLabelX, freightCountedLabelY);

                // Draw 3 Checkboxes

                // Draw the Shipper Counted Checkbox
                var shipperCountedCheckbox1StartPoint = new PointF(_printArea.Width / 2.0f + 8.125f, pageStartPoint.Y + 920.0f + trailerLoadedLabelSize.Height + 1.5f);
                e.Graphics.DrawRectangle(_blackPen, shipperCountedCheckbox1StartPoint.X, shipperCountedCheckbox1StartPoint.Y, 12, 12);

                // Draw the Driver Counted Checkbox (By Pallets)
                var driverPalletsCountedCheckboxStartPoint = new PointF(_printArea.Width / 2.0f + 8.125f, pageStartPoint.Y + 945.0f + trailerLoadedLabelSize.Height + 1.5f);
                e.Graphics.DrawRectangle(_blackPen, driverPalletsCountedCheckboxStartPoint.X, driverPalletsCountedCheckboxStartPoint.Y, 12, 12);

                // Draw the Driver Counted Checkbox (By Pieces)
                var driverPiecesCountedCheckboxStartPoint = new PointF(_printArea.Width / 2.0f + 8.125f, pageStartPoint.Y + 970.0f + trailerLoadedLabelSize.Height + 1.5f);
                e.Graphics.DrawRectangle(_blackPen, driverPiecesCountedCheckboxStartPoint.X, driverPiecesCountedCheckboxStartPoint.Y, 12, 12);

                // Shipper Counted Text
                SizeF shipperCountedTextSize = e.Graphics.MeasureString("By Shipper", _fontSmallNormal);
                float shipperCountedTextX = _printArea.Width / 2.0f + 24.25f;
                float shipperCountedTextY = pageStartPoint.Y + 919.0f + shipperLoadedTextSize.Height + 1.5f;
                e.Graphics.DrawString("By Shipper", _fontSmallNormal, _blackBrush, shipperCountedTextX, shipperCountedTextY);

                // Driver Counted By Pallets Loaded Text
                SizeF driverCountedPalletsTextSize = e.Graphics.MeasureString("By Driver/By Pallet", _fontSmallNormal);
                float driverCountedPalletsTextX = _printArea.Width / 2.0f + 24.25f;
                float driverCountedPalletsTextY = pageStartPoint.Y + 944.0f + driverCountedPalletsTextSize.Height + 1.5f;
                e.Graphics.DrawString("By Driver/By Pallet", _fontSmallNormal, _blackBrush, driverCountedPalletsTextX, driverCountedPalletsTextY);

                // Driver Counted By Pieces Loaded Text
                SizeF driverCountedPackagesTextSize = e.Graphics.MeasureString("By Driver/By Pieces", _fontSmallNormal);
                float driverCountedPackagesTextX = _printArea.Width / 2.0f + 24.25f;
                float driverCountedPackagesTextY = pageStartPoint.Y + 969.0f + driverCountedPackagesTextSize.Height + 1.5f;
                e.Graphics.DrawString("By Driver/By Pieces", _fontSmallNormal, _blackBrush, driverCountedPackagesTextX, driverCountedPackagesTextY);

                // End of Freight Counted
                // /////////////////////////////////////////////////////////////////////////////////////

                // ////////////////////////////////////////////////////////////////////////////////////
                // Draw a vertical line in the Carrier Signature Container 2/3 across the page width
                var carrierSignatureVerticalLineContainerStartPoint = new PointF(_printArea.Width / 3.0f + _printArea.Width / 3.0f, pageStartPoint.Y + 910.0f);
                var carrierSignatureVerticalLineContainerStopPoint = new PointF(_printArea.Width / 3.0f + _printArea.Width / 3.0f, _printArea.Height - 25.0f);
                e.Graphics.DrawLine(_blackPen, carrierSignatureVerticalLineContainerStartPoint, carrierSignatureVerticalLineContainerStopPoint);

                // Carrier Signature                    
                SizeF carrierSignatureLabelSize = e.Graphics.MeasureString("CARRIER SIGNATURE / PICKUP DATE", _fontSmallBold);
                float carrierSignatureLabelX = _printArea.Width / 3.0f + _printArea.Width / 3.0f + 12.5f;
                float carrierSignatureLabelY = pageStartPoint.Y + 898.0f + carrierSignatureLabelSize.Height + 1.5f;
                e.Graphics.DrawString("CARRIER SIGNATURE / PICKUP DATE", _fontSmallBold, _blackBrush, carrierSignatureLabelX, carrierSignatureLabelY);

                // Shipper Signature Legal Terms                    
                SizeF carrierSignatureLegalTermsLabelSize = e.Graphics.MeasureString("Carrier acknowledges receipt of packages and required placards," + Constants.vbCrLf + "certifies emergency response information was made available" + Constants.vbCrLf + "and/or carrier has the DOT emergency response guidebook nor" + Constants.vbCrLf + "equivalent documentation in the vehicle. Property described" + Constants.vbCrLf + "above is received in good order, except as noted.", _fontTinyNormal);
                float carrierSignatureLegalTermsLabelX = _printArea.Width / 3.0f + _printArea.Width / 3.0f + 6.125f;
                float carrierSignatureLegalTermsLabelY = pageStartPoint.Y + 880.0f + carrierSignatureLegalTermsLabelSize.Height + 1.5f;
                e.Graphics.DrawString("Carrier acknowledges receipt of packages and required placards," + Constants.vbCrLf + "certifies emergency response information was made available" + Constants.vbCrLf + "and/or carrier has the DOT emergency response guidebook nor" + Constants.vbCrLf + "equivalent documentation in the vehicle. Property described" + Constants.vbCrLf + "above is received in good order, except as noted.", _fontTinyNormal, _blackBrush, carrierSignatureLegalTermsLabelX, carrierSignatureLegalTermsLabelY);

                // ////////////////////////////////////////////////////////////////////////////////////
                // End of Carrier Signature Container
                // ////////////////////////////////////////////////////////////////////////////////////

                // ////////////////////////////////////////////////////////////////////////////////////
                // Shipment Details - Containers, Pallets, Packages
                // ////////////////////////////////////////////////////////////////////////////////////

                // Uncomment this when it's written
                PrintDocument_PhysicalPalletSquares(e);


                // End of If BolRequest Null Check

            }

        }

    }

    private static FREIGHTCLASSCODES? GetHighestFreightClass(IEnumerable<PALLETS> pallets)
    {
        return pallets
            .Select(pallet => pallet.ClassCode)
            .Where(classCode => classCode is not null)
            .OrderByDescending(classCode => classCode!.CodeNumber ?? double.MinValue)
            .ThenByDescending(
                classCode => classCode!.Name ?? string.Empty,
                StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }

    /// <summary>
    /// Prints one details square for each physical pallet/load, four per page.
    /// </summary>
    private void PrintDocument_PhysicalPalletSquares(PrintPageEventArgs e)
    {
        RectangleF printArea = e.PageSettings.PrintableArea;
        _pageStartPoint.X = 24.0f;
        _pageStartPoint.Y = 24.0f;

        List<PALLETS> pagePallets = PALLETSs
            .Skip(_currentBillOfLadingIndex)
            .Take(4)
            .ToList();

        for (int index = 0; index < pagePallets.Count; index++)
        {
            PALLETS pallet = pagePallets[index];
            pallet.ShipmentType = Bol.ShipmentType;

            bool rightColumn = index >= 2;
            bool bottomRow = index % 2 == 1;
            float keyX = rightColumn ? printArea.Width / 2.0f : _pageStartPoint.X + 12.5f;
            float valueX = rightColumn ? printArea.Width / 2.0f + 200.0f : _pageStartPoint.X + 200.0f;
            float y = _pageStartPoint.Y + 350.0f + (bottomRow ? 168.0f : 0.0f);

            PrintDocument_PrintPalletItem(e, [pallet], pallet, keyX, valueX, y);
        }

        if (Bol.PrintHoursOfOperation && pagePallets.Count < 4)
        {
            float hoursY = _pageStartPoint.Y + 350.0f + (pagePallets.Count % 2 == 1 ? 168.0f : 0.0f);
            float hoursKeyX = pagePallets.Count >= 2 ? printArea.Width / 2.0f : _pageStartPoint.X + 12.5f;
            float hoursValueX = pagePallets.Count >= 2 ? printArea.Width / 2.0f + 200.0f : _pageStartPoint.X + 200.0f;
            PrintDocument_HoursOfOperation(e, Bol, hoursKeyX, hoursValueX, hoursY);
        }

        const float summaryX = 37.0f;
        const float summaryY = 703.0f;
        PrintDocument_ShipmentSummary(e, PALLETSs, summaryX, summaryY);

        _currentBillOfLadingIndex += pagePallets.Count;
        e.HasMorePages = _currentBillOfLadingIndex < PALLETSs.Count;
    }

    private static string GetDimensionUnit(string? volumeType)
    {
        return volumeType?.Trim().ToUpperInvariant() switch
        {
            "CM" or "CUBIC METER" or "CUBIC METERS" => "CM",
            _ => "IN"
        };
    }

    /// <summary>
    /// Print the BOL Pallets within the document
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PrintDocument_ShipmentDetails(object sender, PrintPageEventArgs e)
    {
        // //////////////////////////////////////////////////////////////////////////////
        // Program the shipment detail start points
        // 

        // Set the Pallet, Package Container Object Start Point
        // This will represent temp index 0, the first pallet or package
        // 

        // Define the printable area.
        RectangleF printArea = e.PageSettings.PrintableArea;

        _pageStartPoint.X = 24.0f;
        _pageStartPoint.Y = 24.0f;

        float objectContainerStartPointXKey = _pageStartPoint.X + 12.5f;
        float objectContainerStartPointXvalue = _pageStartPoint.X + 200.0f;
        float objectContainerStartPointY = _pageStartPoint.Y + 350.0f;

        // Reset the TempStartIndex
        int tempStartIndex = 0;

        // We can print 4 objects per page
        // /////////////////////////////////////////////////////////////////////////////
        // Pallets, MiscItems and Misc over-sized Items

        if (PALLETSs is not null && PALLETSs.Any())
        {

            List<PALLETS> pallets = PALLETSs.Where(p => Operators.ConditionalCompareObjectEqual(p.PalletType, 0, false)).ToList();
            List<PALLETS> miscPieces = PALLETSs.Where(p => Operators.ConditionalCompareObjectEqual(p.PalletType, 1, false)).ToList();
            List<PALLETS> miscOverSizedPieces = PALLETSs.Where(p => Operators.ConditionalCompareObjectEqual(p.PalletType, 2, false)).ToList();

            if (pallets.Any())
            {

                PALLETS @init = new PALLETS();
                var pallet = (@init.Description = pallets.FirstOrDefault()?.Description, @init.BoxCount = pallets.Sum(cc => cc.BoxCount), @init.UnitCount = pallets.Sum(uc => uc.UnitCount), @init.Weight = pallets.Sum(w => w.Weight), @init.Volume = pallets.Sum(v => v.Volume), @init.ClassCode = GetHighestFreightClass(pallets), @init.ShipmentType = Bol.ShipmentType, @init.PalletType = pallets.FirstOrDefault()!.PalletType, @init.VolumeType = pallets.FirstOrDefault()?.VolumeType, @init.WeightType = pallets.FirstOrDefault()?.WeightType, @init.CurrencyCode = pallets.FirstOrDefault()?.CurrencyCode, @init).@init;

                // Use this unified function to print the Pallet Block
                PrintDocument_PrintPalletItem(e, pallets, pallet, objectContainerStartPointXKey, objectContainerStartPointXvalue, objectContainerStartPointY);
                tempStartIndex += 1;

                // Create code to set the new print position for tempIndex 1 to 3
                switch (tempStartIndex)
                {
                    case 0:
                        {
                            // use the defined above the loop
                            // Pallets are First
                            objectContainerStartPointY = _pageStartPoint.Y + 350.0f;
                            break;
                        }
                    case 1:
                        {
                            // 2nd Item, Misc Pieces bottom left
                            objectContainerStartPointY += 168.0f;
                            break;
                        }
                    case 2:
                        {
                            // 3rd Item, Misc over-sized Pieces Top Right
                            objectContainerStartPointY = _pageStartPoint.Y + 350.0f;
                            objectContainerStartPointXKey = printArea.Width / 2;
                            objectContainerStartPointXvalue = printArea.Width / 2 + 200.0f;
                            break;
                        }
                    case 3:
                        {
                            // 4th Item, Not Assigned Bottom Right
                            objectContainerStartPointY += 168.0f;

                            // Time to start a new page, if we have more Pallets or Package
                            // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                            tempStartIndex = -1;
                            break;
                        }
                }

            }

            // Misc Pieces
            if (miscPieces.Any())
            {

                PALLETS @init1 = new PALLETS();
                var miscPiece = (@init1.Description = miscPieces.FirstOrDefault()?.Description, @init1.BoxCount = miscPieces.Sum(cc => cc.BoxCount), @init1.UnitCount = miscPieces.Sum(uc => uc.UnitCount), @init1.Weight = miscPieces.Sum(w => w.Weight), @init1.Volume = miscPieces.Sum(v => v.Volume), @init1.ClassCode = GetHighestFreightClass(miscPieces), @init1.ShipmentType = Bol.ShipmentType, @init1.PalletType = miscPieces.FirstOrDefault()!.PalletType, @init1.VolumeType = miscPieces.FirstOrDefault()?.VolumeType, @init1.WeightType = miscPieces.FirstOrDefault()?.WeightType, @init1.CurrencyCode = miscPieces.FirstOrDefault()?.CurrencyCode, @init1).@init1;

                // Use this unified function to print the Pallet Block
                PrintDocument_PrintPalletItem(e, miscPieces, miscPiece, objectContainerStartPointXKey, objectContainerStartPointXvalue, objectContainerStartPointY);
                tempStartIndex += 1;

                // Create code to set the new print position for tempIndex 1 to 3
                switch (tempStartIndex)
                {
                    case 0:
                        {
                            // use the defined above the loop
                            // Pallets are First
                            objectContainerStartPointY = _pageStartPoint.Y + 350.0f;
                            break;
                        }
                    case 1:
                        {
                            // 2nd Item, Misc Pieces bottom left
                            objectContainerStartPointY += 168.0f;
                            break;
                        }
                    case 2:
                        {
                            // 3rd Item, Misc over-sized Pieces Top Right
                            objectContainerStartPointY = _pageStartPoint.Y + 350.0f;
                            objectContainerStartPointXKey = printArea.Width / 2;
                            objectContainerStartPointXvalue = printArea.Width / 2 + 200.0f;
                            break;
                        }
                    case 3:
                        {
                            // 4th Item, Not Assigned Bottom Right
                            objectContainerStartPointY += 168.0f;

                            // Time to start a new page, if we have more Pallets or Package
                            // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                            tempStartIndex = -1;
                            break;
                        }
                }

            }

            if (miscOverSizedPieces.Any())
            {

                PALLETS @init2 = new PALLETS();
                var miscOverSizedPiece = (@init2.Description = miscOverSizedPieces.FirstOrDefault()?.Description, @init2.BoxCount = miscOverSizedPieces.Sum(cc => cc.BoxCount), @init2.UnitCount = miscOverSizedPieces.Sum(uc => uc.UnitCount), @init2.Weight = miscOverSizedPieces.Sum(w => w.Weight), @init2.Volume = miscOverSizedPieces.Sum(v => v.Volume), @init2.ClassCode = GetHighestFreightClass(miscOverSizedPieces), @init2.ShipmentType = Bol.ShipmentType, @init2.PalletType = miscOverSizedPieces.FirstOrDefault()!.PalletType, @init2.VolumeType = miscOverSizedPieces.FirstOrDefault()?.VolumeType, @init2.WeightType = miscOverSizedPieces.FirstOrDefault()?.WeightType, @init2.CurrencyCode = miscOverSizedPieces.FirstOrDefault()?.CurrencyCode, @init2).@init2;

                // Use this unified function to print the Pallet Block
                PrintDocument_PrintPalletItem(e, miscOverSizedPieces, miscOverSizedPiece, objectContainerStartPointXKey, objectContainerStartPointXvalue, objectContainerStartPointY);
                tempStartIndex += 1;

                // Create code to set the new print position for tempIndex 1 to 3
                switch (tempStartIndex)
                {
                    case 0:
                        {
                            // use the defined above the loop
                            // Pallets are First
                            objectContainerStartPointY = _pageStartPoint.Y + 350.0f;
                            break;
                        }
                    case 1:
                        {
                            // 2nd Item, Misc Pieces bottom left
                            objectContainerStartPointY += 168.0f;
                            break;
                        }
                    case 2:
                        {
                            // 3rd Item, Misc over-sized Pieces Top Right
                            objectContainerStartPointY = _pageStartPoint.Y + 350.0f;
                            objectContainerStartPointXKey = printArea.Width / 2;
                            objectContainerStartPointXvalue = printArea.Width / 2 + 200.0f;
                            break;
                        }
                    case 3:
                        {
                            // 4th Item, Not Assigned Bottom Right
                            objectContainerStartPointY += 168.0f;

                            // Time to start a new page, if we have more Pallets or Package
                            // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                            tempStartIndex = -1;
                            break;
                        }
                }

            }

            // Print Customer Hours
            if (Bol.PrintHoursOfOperation == true)
            {
                PrintDocument_HoursOfOperation(e, Bol, objectContainerStartPointXKey, objectContainerStartPointXvalue, objectContainerStartPointY);
            }

            // Print Pallet Summary
            // Calculate the new X and Y for the Summary
            const float startPointX = 37.0f;
            const float startPointY = 703.0f;
            PrintDocument_ShipmentSummary(e, PALLETSs, startPointX, startPointY);
        }

    }

    /// <summary>
    /// Unified Code to Print Pallet, Misc Items and Over-Sized Items
    /// </summary>
    /// <param name="e"></param>
    /// <param name="pallets"></param>
    /// <param name="pallet"></param>
    /// <param name="objectContainerStartPointXKey"></param>
    /// <param name="objectContainerStartPointXvalue"></param>
    /// <param name="objectContainerStartPointY"></param>
    private void PrintDocument_PrintPalletItem(PrintPageEventArgs e, List<PALLETS> pallets, PALLETS pallet, float objectContainerStartPointXKey, float objectContainerStartPointXvalue, float objectContainerStartPointY)
    {

        string limitedDescription = Conversions.ToString(pallet.Description.Length > 21 ? pallet.Description.Substring(0, 21) : pallet.Description);
        string palletTypeString = "Pallet(s)";
        switch (pallet.PalletType)
        {
            case 0:
                {
                    palletTypeString = "Pallet(s)";
                    break;
                }
            case 1:
                {
                    palletTypeString = "Misc Piece(s)";
                    break;
                }
            case 2:
                {
                    palletTypeString = "Over Sized";
                    break;
                }
        }

        // eGraphicsPallets
        e.Graphics.DrawString(palletTypeString + " Desc: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY);
        e.Graphics.DrawString(limitedDescription, _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY);

        e.Graphics.DrawString("Total Cartons: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 15.0f);
        e.Graphics.DrawString(pallet.BoxCount.ToString(), _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 15.0f);

        e.Graphics.DrawString("Total Units: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 30.0f);
        e.Graphics.DrawString(pallet.UnitCount.ToString(), _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 30.0f);

        e.Graphics.DrawString("Number of " + palletTypeString, _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 45.0f);
        e.Graphics.DrawString(pallets.Count.ToString(), _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 45.0f);

        // Draw the line under "Number of Pallets" section
        e.Graphics.DrawLine(_blackThinPen, objectContainerStartPointXKey, objectContainerStartPointY + 64.0f, objectContainerStartPointXvalue + 165.0f, objectContainerStartPointY + 64.0f);

        var dimensionUnit = GetDimensionUnit(pallet.VolumeType);

        // Print the pallet dimensions if the user chooses to use them
        if (pallet.Length > 0.00d & pallet.Width > 0 & pallet.Height > 0)
        {
            e.Graphics.DrawString("Dimensions: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 67.0f);
            e.Graphics.DrawString(pallet.Length.ToString() + " x " + pallet.Width.ToString() + " x " + pallet.Height.ToString() + " " + dimensionUnit, _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 67.0f);
        }

        if (pallet.Volume > 0.00d)
        {

            var displayVolume = pallet.Volume;
            var displayVolumeType = dimensionUnit == "CM" ? "cubic meter" : "cubic feet";
            switch (dimensionUnit)
            {
                case "IN":
                    {
                        if (displayVolume != 0)
                        {
                            displayVolume /= 1728; // Convert cubic inches to cubic feet
                        }
                        displayVolume = (float)Math.Round(displayVolume, 2);
                        break;
                    }

                case "CM":
                    {
                        if (displayVolume != 0)
                        {
                            displayVolume /= 1000000; // Convert cubic centimeters to cubic meters
                        }
                        displayVolume = (float)Math.Round(displayVolume, 2);
                        break;
                    }

            }

            e.Graphics.DrawString("Total Volume: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 82.0f);
            e.Graphics.DrawString(displayVolume.ToString("F2") + " " + displayVolumeType, _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 82.0f);

        }

        e.Graphics.DrawString("Total Weight: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 97.0f);
        e.Graphics.DrawString(pallet.Weight.ToString() + " " + pallet.WeightType, _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 97.0f);


        // Draw the line under "Total Pallet Weight" section
        e.Graphics.DrawLine(_blackThinPen, objectContainerStartPointXKey, objectContainerStartPointY + 116.0f, objectContainerStartPointXvalue + 165.0f, objectContainerStartPointY + 116.0f);

        if (pallet.ClassCode is not null)
        {
            e.Graphics.DrawString("Max Freight Class: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 121.0f);
            e.Graphics.DrawString(pallet.ClassText ?? string.Empty, _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 121.0f);
        }

        e.Graphics.DrawString("Shipment Type: ", _fontPalletBold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 136.0f);
        e.Graphics.DrawString(pallet.ShipmentType, _fontPalletNormal, _blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 136.0f);

    }

    /// <summary>
    /// Print Customer Hours of Operation
    /// </summary>
    /// <param name="e"></param>
    /// <param name="bolRequest"></param>
    /// <param name="objectContainerStartPointXKey"></param>
    /// <param name="objectContainerStartPointY"></param>
    private void PrintDocument_HoursOfOperation(PrintPageEventArgs e, BILLOFLADINGS bol, float objectContainerStartPointXKey, float objectContainerStartPointXValue, float objectContainerStartPointY)
    {
        Dictionary<string, string> schedule = BuildHoursSchedule(bol);

        e.Graphics.DrawString("Ship to: Hours of Operation", _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY);
        objectContainerStartPointY = objectContainerStartPointY + 6.0f;

        // Example: Accessing parsed values
        foreach (KeyValuePair<string, string> day in schedule)
        {

            string[] hours = day.Value.Split('-');

            string open = "";
            string close = "";

            if (hours.Length == 2)
            {
                open = hours[0].Trim(); // Get opening time
                close = hours[1].Trim(); // Get closing time
            }
            else
            {
                open = day.Value; // Handle cases like "Closed"
                close = "";
            }

            switch (day.Key ?? "")
            {

                case "Mon":
                    {

                        string mondayStringOpen = "Closed";
                        string mondayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            mondayStringOpen = "Closed";
                            mondayStringClosed = "Closed";
                        }
                        else
                        {
                            mondayStringOpen = open;
                            mondayStringClosed = close;
                        }

                        // Measure the width of "Monday:"
                        string mondayText = "Monday: ";
                        SizeF mondayTextSize = e.Graphics.MeasureString(mondayText, _fontMedium10Bold);
                        float mondayTextWidth = mondayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF mondayStringOpenSize = e.Graphics.MeasureString(mondayStringOpen, _fontMedium10Normal);
                        float mondayStringOpenWidth = mondayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string mondayToText = "to ";
                        SizeF mondayToTextSize = e.Graphics.MeasureString(mondayToText, _fontMedium10Normal);
                        float mondayToTextWidth = mondayToTextSize.Width;

                        // Now, draw the "Monday:" text
                        e.Graphics.DrawString(mondayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 16.0f);

                        // Draw the mondayStringOpen with a fixed width (aligned with "Monday:")
                        e.Graphics.DrawString(mondayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 16.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(mondayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + mondayStringOpenWidth + 5.0f, objectContainerStartPointY + 16.0f);

                        // Draw the mondayStringClosed to the right of the "to"
                        e.Graphics.DrawString(mondayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + mondayStringOpenWidth + mondayToTextWidth + 5.0f, objectContainerStartPointY + 16.0f);
                        break;
                    }

                case "Tue":
                    {

                        string tuesdayStringOpen = "Closed";
                        string tuesdayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            tuesdayStringOpen = "Closed";
                            tuesdayStringClosed = "Closed";
                        }
                        else
                        {
                            tuesdayStringOpen = open;
                            tuesdayStringClosed = close;
                        }

                        // Measure the width of "Tuesday:"
                        string tuesdayText = "Tuesday: ";
                        SizeF tuesdayTextSize = e.Graphics.MeasureString(tuesdayText, _fontMedium10Bold);
                        float tuesdayTextWidth = tuesdayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF tuesdayStringOpenSize = e.Graphics.MeasureString(tuesdayStringOpen, _fontMedium10Normal);
                        float tuesdayStringOpenWidth = tuesdayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string tuesdayToText = "to ";
                        SizeF tuesdayToTextSize = e.Graphics.MeasureString(tuesdayToText, _fontMedium10Normal);
                        float tuesdayToTextWidth = tuesdayToTextSize.Width;

                        // Now, draw the "Tuesday:" text
                        e.Graphics.DrawString(tuesdayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 34.0f);

                        // Draw the tuesdayStringOpen with a fixed width (aligned with "Tuesday:")
                        e.Graphics.DrawString(tuesdayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 34.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(tuesdayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + tuesdayStringOpenWidth + 5.0f, objectContainerStartPointY + 34.0f);

                        // Draw the tuesdayStringClosed to the right of the "to"
                        e.Graphics.DrawString(tuesdayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + tuesdayStringOpenWidth + tuesdayToTextWidth + 5.0f, objectContainerStartPointY + 34.0f);
                        break;
                    }

                case "Wed":
                    {

                        string wednesdayStringOpen = "Closed";
                        string wednesdayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            wednesdayStringOpen = "Closed";
                            wednesdayStringClosed = "Closed";
                        }
                        else
                        {
                            wednesdayStringOpen = open;
                            wednesdayStringClosed = close;
                        }

                        // Measure the width of "Wednesday:"
                        string wednesdayText = "Wednesday: ";
                        SizeF wednesdayTextSize = e.Graphics.MeasureString(wednesdayText, _fontMedium10Bold);
                        float wednesdayTextWidth = wednesdayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF wednesdayStringOpenSize = e.Graphics.MeasureString(wednesdayStringOpen, _fontMedium10Normal);
                        float wednesdayStringOpenWidth = wednesdayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string wednesdayToText = "to ";
                        SizeF wednesdayToTextSize = e.Graphics.MeasureString(wednesdayToText, _fontMedium10Normal);
                        float wednesdayToTextWidth = wednesdayToTextSize.Width;

                        // Now, draw the "Wednesday:" text
                        e.Graphics.DrawString(wednesdayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 52.0f);

                        // Draw the wednesdayStringOpen with a fixed width (aligned with "Wednesday:")
                        e.Graphics.DrawString(wednesdayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 52.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(wednesdayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + wednesdayStringOpenWidth + 5.0f, objectContainerStartPointY + 52.0f);

                        // Draw the wednesdayStringClosed to the right of the "to"
                        e.Graphics.DrawString(wednesdayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + wednesdayStringOpenWidth + wednesdayToTextWidth + 5.0f, objectContainerStartPointY + 52.0f);
                        break;
                    }

                case "Thu":
                    {

                        string thursdayStringOpen = "Closed";
                        string thursdayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            thursdayStringOpen = "Closed";
                            thursdayStringClosed = "Closed";
                        }
                        else
                        {
                            thursdayStringOpen = open;
                            thursdayStringClosed = close;
                        }

                        // Measure the width of "Thursday:"
                        string thursdayText = "Thursday: ";
                        SizeF thursdayTextSize = e.Graphics.MeasureString(thursdayText, _fontMedium10Bold);
                        float thursdayTextWidth = thursdayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF thursdayStringOpenSize = e.Graphics.MeasureString(thursdayStringOpen, _fontMedium10Normal);
                        float thursdayStringOpenWidth = thursdayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string thursdayToText = "to ";
                        SizeF thursdayToTextSize = e.Graphics.MeasureString(thursdayToText, _fontMedium10Normal);
                        float thursdayToTextWidth = thursdayToTextSize.Width;

                        // Now, draw the "Thursday:" text
                        e.Graphics.DrawString(thursdayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 70.0f);

                        // Draw the thursdayStringOpen with a fixed width (aligned with "Thursday:")
                        e.Graphics.DrawString(thursdayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 70.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(thursdayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + thursdayStringOpenWidth + 5.0f, objectContainerStartPointY + 70.0f);

                        // Draw the thursdayStringClosed to the right of the "to"
                        e.Graphics.DrawString(thursdayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + thursdayStringOpenWidth + thursdayToTextWidth + 5.0f, objectContainerStartPointY + 70.0f);
                        break;
                    }

                case "Fri":
                    {

                        string fridayStringOpen = "Closed";
                        string fridayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            fridayStringOpen = "Closed";
                            fridayStringClosed = "Closed";
                        }
                        else
                        {
                            fridayStringOpen = open;
                            fridayStringClosed = close;
                        }

                        // Measure the width of "Friday:"
                        string fridayText = "Friday: ";
                        SizeF fridayTextSize = e.Graphics.MeasureString(fridayText, _fontMedium10Bold);
                        float fridayTextWidth = fridayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF fridayStringOpenSize = e.Graphics.MeasureString(fridayStringOpen, _fontMedium10Normal);
                        float fridayStringOpenWidth = fridayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string fridayToText = "to ";
                        SizeF fridayToTextSize = e.Graphics.MeasureString(fridayToText, _fontMedium10Normal);
                        float fridayToTextWidth = fridayToTextSize.Width;

                        // Now, draw the "Friday:" text
                        e.Graphics.DrawString(fridayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 88.0f);

                        // Draw the fridayStringOpen with a fixed width (aligned with "Friday:")
                        e.Graphics.DrawString(fridayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 88.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(fridayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + fridayStringOpenWidth + 5.0f, objectContainerStartPointY + 88.0f);

                        // Draw the fridayStringClosed to the right of the "to"
                        e.Graphics.DrawString(fridayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + fridayStringOpenWidth + fridayToTextWidth + 5.0f, objectContainerStartPointY + 88.0f);
                        break;
                    }

                case "Sat":
                    {

                        string saturdayStringOpen = "Closed";
                        string saturdayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            saturdayStringOpen = "Closed";
                            saturdayStringClosed = "Closed";
                        }
                        else
                        {
                            saturdayStringOpen = open;
                            saturdayStringClosed = close;
                        }

                        // Measure the width of "Saturday:"
                        string saturdayText = "Saturday: ";
                        SizeF saturdayTextSize = e.Graphics.MeasureString(saturdayText, _fontMedium10Bold);
                        float saturdayTextWidth = saturdayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF saturdayStringOpenSize = e.Graphics.MeasureString(saturdayStringOpen, _fontMedium10Normal);
                        float saturdayStringOpenWidth = saturdayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string saturdayToText = "to ";
                        SizeF saturdayToTextSize = e.Graphics.MeasureString(saturdayToText, _fontMedium10Normal);
                        float saturdayToTextWidth = saturdayToTextSize.Width;

                        // Now, draw the "Saturday:" text
                        e.Graphics.DrawString(saturdayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 106.0f);

                        // Draw the saturdayStringOpen with a fixed width (aligned with "Saturday:")
                        e.Graphics.DrawString(saturdayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 106.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(saturdayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + saturdayStringOpenWidth + 5.0f, objectContainerStartPointY + 106.0f);

                        // Draw the saturdayStringClosed to the right of the "to"
                        e.Graphics.DrawString(saturdayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + saturdayStringOpenWidth + saturdayToTextWidth + 5.0f, objectContainerStartPointY + 106.0f);
                        break;
                    }

                case "Sun":
                    {

                        string sundayStringOpen = "Closed";
                        string sundayStringClosed = "Closed";

                        // Check if open and close values are provided
                        if (open == "Closed")
                        {
                            sundayStringOpen = "Closed";
                            sundayStringClosed = "Closed";
                        }
                        else
                        {
                            sundayStringOpen = open;
                            sundayStringClosed = close;
                        }

                        // Measure the width of "Sunday:"
                        string sundayText = "Sunday: ";
                        SizeF sundayTextSize = e.Graphics.MeasureString(sundayText, _fontMedium10Bold);
                        float sundayTextWidth = sundayTextSize.Width;

                        // Measure the width of the opening time string
                        SizeF sundayStringOpenSize = e.Graphics.MeasureString(sundayStringOpen, _fontMedium10Normal);
                        float sundayStringOpenWidth = sundayStringOpenSize.Width;

                        // Measure the width of the "to" text string
                        string sundayToText = "to ";
                        SizeF sundayToTextSize = e.Graphics.MeasureString(sundayToText, _fontMedium10Normal);
                        float sundayToTextWidth = sundayToTextSize.Width;

                        // Now, draw the "Sunday:" text
                        e.Graphics.DrawString(sundayText, _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY + 124.0f);

                        // Draw the sundayStringOpen with a fixed width (aligned with "Sunday:")
                        e.Graphics.DrawString(sundayStringOpen, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue, objectContainerStartPointY + 124.0f);

                        // Draw the "to" text after opening time with fixed width alignment
                        e.Graphics.DrawString(sundayToText, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + sundayStringOpenWidth + 5.0f, objectContainerStartPointY + 124.0f);

                        // Draw the sundayStringClosed to the right of the "to"
                        e.Graphics.DrawString(sundayStringClosed, _fontMedium10Normal, _blackBrush, objectContainerStartPointXValue + sundayStringOpenWidth + sundayToTextWidth + 5.0f, objectContainerStartPointY + 124.0f);
                        break;
                    }

            }

        }

    }

    private static Dictionary<string, string> BuildHoursSchedule(BILLOFLADINGS bol)
    {
        return (bol.ShipToLocation?.HoursOfOperation?.Days ?? [])
            .OrderBy(day => day.Day)
            .ToDictionary(
                day => day.Day.ToString()[..3],
                day => day.IsClosed
                    ? "Closed"
                    : $"{FormatPrintTime(day.Open)} - {FormatPrintTime(day.Close)}");
    }

    private static string FormatPrintTime(string? time)
    {
        return DateTime.TryParseExact(time, "HH:mm", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out DateTime parsed)
            ? parsed.ToString("h:mm tt", CultureInfo.InvariantCulture)
            : time ?? string.Empty;
    }

    /// <summary>
    /// Unified Code to Print Pallet Summary
    /// </summary>
    /// <param name="e"></param>
    /// <param name="pallets"></param>
    /// <param name="startPointX"></param>
    /// <param name="startPointY"></param>
    private void PrintDocument_ShipmentSummary(PrintPageEventArgs e, List<PALLETS> pallets, float startPointX, float startPointY)
    {

        int palletCount = pallets.Where(pc => Operators.ConditionalCompareObjectEqual(pc.PalletType, 0, false)).Count();
        int miscCount = pallets.Where(ms => Operators.ConditionalCompareObjectEqual(ms.PalletType, 1, false)).Count();
        int overSizedCount = pallets.Where(os => Operators.ConditionalCompareObjectEqual(os.PalletType, 2, false)).Count();
        int totalCount = pallets.Count;
        decimal totalWeight = pallets.Sum(tw => tw.Weight);
        decimal totalVolumeCubicFeet = (decimal)pallets.Sum(tv => tv.Volume);
        string weightType = pallets.Select(wt => wt.WeightType).FirstOrDefault();
        string volumeType = pallets.Select(wt => wt.VolumeType).FirstOrDefault();

        switch (volumeType ?? "")
        {
            case "IN":
                {
                    volumeType = " CF"; // Cubic Feet
                    totalVolumeCubicFeet = totalVolumeCubicFeet / 1728m;
                    break;
                }
            case "CM":
                {
                    volumeType = "M³"; // Cubic Meters
                    totalVolumeCubicFeet = totalVolumeCubicFeet / 1000m;
                    break;
                }
        }

        // eGraphicsPallets
        e.Graphics.DrawString("Shipment Summary", _fontMedium12Bold, _blackBrush, startPointX, startPointY);

        float spacing = 12.0f; // Extra spacing between items

        // Define texts
        const string palletText = "Pallet(s):";
        const string miscText = "Misc Item(s):";
        const string overSizedText = "Oversized Item(s):";
        const string totalText = "Total Unit(s):";
        const string totalWeightText = "Total Weight:";
        const string totalVolumeText = "Total Volume:";

        // Measure text widths
        SizeF palletTextSize = e.Graphics.MeasureString(palletText, _fontMedium12Bold);
        float palletTextWidth = palletTextSize.Width;

        SizeF miscTextSize = e.Graphics.MeasureString(miscText, _fontMedium12Bold);
        float miscTextWidth = miscTextSize.Width;

        SizeF overSizedTextSize = e.Graphics.MeasureString(overSizedText, _fontMedium12Bold);
        float overSizedTextWidth = overSizedTextSize.Width;

        SizeF totalTextSize = e.Graphics.MeasureString(totalText, _fontMedium12Bold);
        float totalTextWidth = totalTextSize.Width;

        SizeF totalWeightTextSize = e.Graphics.MeasureString(totalWeightText, _fontMedium12Bold);
        float totalWeightTextWidth = totalWeightTextSize.Width;

        SizeF totalVolumeTextSize = e.Graphics.MeasureString(totalVolumeText, _fontMedium12Bold);
        float totalVolumeTextWidth = totalVolumeTextSize.Width;

        // Start positions
        float currentX = startPointX;
        float currentY = startPointY + 23.0f; // Adjust vertical alignment

        // //////////////////////////////////////////////////////////////////////////////////////////
        // Pallet Counts

        // Draw "Pallet(s):"
        e.Graphics.DrawString(palletText, _fontMedium12Bold, _greenBrush, currentX, currentY);
        e.Graphics.DrawString(palletCount.ToString(), _fontMedium12Normal, _greenBrush, currentX + palletTextWidth, currentY);

        // Move to next position
        currentX += palletTextWidth + spacing; // Adding extra spacing

        // Draw "Misc Item(s):"
        e.Graphics.DrawString(miscText, _fontMedium12Bold, _greenBrush, currentX, currentY);
        e.Graphics.DrawString(miscCount.ToString(), _fontMedium12Normal, _greenBrush, currentX + miscTextWidth, currentY);

        // Move to next position
        currentX += miscTextWidth + spacing + 5.0f; // Adding extra spacing

        // Draw "Oversized Item(s):"
        e.Graphics.DrawString(overSizedText, _fontMedium12Bold, _greenBrush, currentX, currentY);
        e.Graphics.DrawString(overSizedCount.ToString(), _fontMedium12Normal, _greenBrush, currentX + overSizedTextWidth, currentY);

        // Move to next position for "Total Items Picked Up:"
        currentX += overSizedTextWidth + spacing + 5.0f; // Adding extra spacing

        // Draw "Total Items Picked Up:"
        e.Graphics.DrawString(totalText, _fontMedium12Bold, _greenBrush, currentX, currentY);
        e.Graphics.DrawString(totalCount.ToString(), _fontMedium12Normal, _greenBrush, currentX + totalTextWidth, currentY);

        // //////////////////////////////////////////////////////////////////////////////////////////
        // Next Line

        // Adjust Vertical Alignment
        currentX = startPointX;
        currentY += 24.0f;

        // Draw "Total Weight:"
        e.Graphics.DrawString(totalWeightText, _fontMedium12Bold, _blackBrush, currentX, currentY);
        e.Graphics.DrawString(totalWeight.ToString() + " " + weightType, _fontMedium12Normal, _blackBrush, currentX + totalWeightTextWidth, currentY);

        // Move to next position for "Total Volume"
        currentX += overSizedTextWidth + spacing + 52.0f; // Adding extra spacing

        // Draw "Total Volume:"
        e.Graphics.DrawString(totalVolumeText, _fontMedium12Bold, _blackBrush, currentX, currentY);
        e.Graphics.DrawString(totalVolumeCubicFeet.ToString("F") + " " + volumeType, _fontMedium12Normal, _blackBrush, currentX + totalVolumeTextWidth, currentY);


    }

    /// <summary>
    /// Prints the report content in code
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PrintReport(object sender, PrintPageEventArgs e)
    {

        if (e.Graphics is not null)
        {


        }

    }

    /// <summary>
    /// Prints a blank page for duplex printing.
    /// </summary>
    /// <param name="e"></param>
    private void PrintBlankPage(PrintPageEventArgs e)
    {

        // Simply do nothing, the framework will handle adding a blank page
        // Optionally, you can draw some text indicating this is an intentionally blank page

        string messageText = "This page intentionally left blank";
        SizeF messageTextSize = e.Graphics.MeasureString(messageText, _fontMedium10Bold);

        // Calculate the X coordinate to horizontally center the text
        var messageTextX = (_printArea.Width - messageTextSize.Width) / 2;

        // Calculate the Y coordinate to vertically center the text
        var messageTextY = (_printArea.Height - messageTextSize.Height) / 2;

        e.Graphics.DrawString(messageText, _fontMedium10Bold, _lightGreyBrush, messageTextX, messageTextY);

    }

    /// <summary>
    /// Receives a call to end Printing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void PrintDocument_EndPrint(object sender, PrintEventArgs e)
    {

        CurrentPage = 0;
        _currentBillOfLadingIndex = 0; // Reset the sales rep index for future print jobs

    }

    // Override the OnPrintPage method to call your custom PrintPage event handler
    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        base.OnPrintPage(e);
        PrintDocument_PrintPage(this, e);
    }

}
