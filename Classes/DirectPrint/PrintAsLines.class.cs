using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using SimpleBol.Models.MongoDb;

public class DirectPrintBillOfLadingAsLines : PrintDocument
{

    public bool DuplexPrinting;

    public BILLOFLADINGS Bol { get; set; } = null!;
    private SHIPPER BolShipper => Bol.Shipper!;
    private CUSTOMERS BolCustomer => Bol.ShipToCustomer!;
    private SHIPPINGLOCATIONS BolCustomerShipAddress => Bol.ShipToLocation!;
    private List<PALLETS> PALLETSs => Bol.Pallets ?? [];
    private List<PACKAGES> PACKAGESs => Bol.Packages ?? [];
    private BILLTOACCOUNTS BolBill3RdParty => Bol.BillToAccount!;
    public List<Tuple<string, string>> BolAppointmentContacts { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }

    private int _currentBillOfLadingIndex = 0;
    private object _disposedValue = false;

    // Create the Duplex placeholders
    private bool _addBlankPage = false;
    private int _totalPrintedPages = 0;

    // Create the Page StartPoint
    private RectangleF _printArea;
    private PointF _pageStartPoint = new PointF(0.0f, 0.0f);
    private int _currentPalletIndex = 0;
    private int _currentPackageIndex = 0;
    private int _itemsPerPage = 15;

    // Track whether Hours of Operation and Shipment Summary have printed
    private bool _printedHours = false;
    private bool _printedSummary = false;

    // Create the fonts we need to print with
    private readonly Font _fontTitleBold = new Font("Arial", 16, FontStyle.Bold);
    private readonly Font _fontTitleNormal = new Font("Arial", 16, FontStyle.Regular);
    private readonly Font _fontLargeBold = new Font("Arial", 14, FontStyle.Bold);
    private readonly Font _fontLargeNormal = new Font("Arial", 14, FontStyle.Regular);
    private readonly Font _fontMedium12Bold = new Font("Arial", 12, FontStyle.Bold);
    private readonly Font _fontMedium12Normal = new Font("Arial", 12);
    private readonly Font _fontMedium10Bold = new Font("Arial", 10, FontStyle.Bold);
    private readonly Font _fontMedium10Normal = new Font("Arial", 10);
    private readonly Font _fontMedium9Bold = new Font("Arial", 9, FontStyle.Bold);
    private readonly Font _fontMedium9Normal = new Font("Arial", 9);
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
                SizeF bolPageNumberTextSize = e.Graphics.MeasureString("Page: " + CurrentPage + " of " + TotalPages, _fontMedium10Normal);
                float bolPageNumberTextX = _printArea.Width - 25.0f - bolPageNumberTextSize.Width - 10.5f;
                float bolPageNumberTextY = pageStartPoint.Y + bolTextY + 4.0f;
                e.Graphics.DrawString("Page: " + CurrentPage + " of " + TotalPages, _fontMedium10Bold, _blackBrush, bolPageNumberTextX, bolPageNumberTextY);


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
                            float liftgateLabelY = shipToStartPoint.Y + shipToRectangleHeight + 87.0f;
                            e.Graphics.DrawString("Liftgate Required: X", _fontMedium10Bold, _redBrush, liftgateLabelX, liftgateLabelY);
                        }

                        if (Bol.ShipToLocation?.AppointmentRequired == true)
                        {

                            SizeF appointmentLabelSize = e.Graphics.MeasureString("Appointment Required: X", _fontMedium10Bold);
                            float appointmentLabelX = shipToStartPoint.X + (liftgateLabelSize.Width == 0.0f ? 12.5f : liftgateLabelSize.Width + 36.5f);
                            float appointmentLabelY = shipToStartPoint.Y + shipToRectangleHeight + 87.0f;
                            e.Graphics.DrawString("Appointment Required: X", _fontMedium10Bold, _redBrush, appointmentLabelX, appointmentLabelY);

                            // Add the contact name and phone or email address
                            if (!string.IsNullOrEmpty(Bol.ShipToLocation?.ContactName) && !string.IsNullOrEmpty(Bol.ShipToLocation?.ContactPhone))
                            {
                                SizeF appointmentContactNameLabelSize = e.Graphics.MeasureString("Appt Contact: " + Bol.ShipToLocation?.ContactName + " at " + Bol.ShipToLocation?.ContactPhone, _fontSmallBold);
                                float appointmentContactNameLabelX = shipToStartPoint.X + 12.5f;
                                float appointmentContactNameLabelY = shipToStartPoint.Y + shipToRectangleHeight + 105.0f;
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
                float startY = PrintDocument_ShipmentDetails(sender, e);

                // End of If BolRequest Null Check
                if (e.HasMorePages == false)
                {
                    CurrentPage = 0; // Reset ONLY when all pages are printed
                }

            }

        }

    }

    /// <summary>
    /// Print the BOL Pallets within the document
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private float PrintDocument_ShipmentDetails(object sender, PrintPageEventArgs e)
    {

        // Define the printable area.
        RectangleF printArea = e.PageSettings.PrintableArea;

        _pageStartPoint.X = 24.0f;
        _pageStartPoint.Y = 24.0f;

        float objectContainerStartPointXKey = _pageStartPoint.X + 12.5f;
        float objectContainerStartPointXvalue = _pageStartPoint.X + 200.0f;
        float objectContainerStartPointY = _pageStartPoint.Y + 350.0f;

        bool printHeader = true;
        float startY = objectContainerStartPointY;
        int itemsPrinted = 0;

        // ////////////////////////
        // Print Pallet Items
        // ////////////////////////
        if (PALLETSs is not null && PALLETSs.Any())
        {

            for (int i = _currentPalletIndex, loopTo = PALLETSs.Count - 1; i <= loopTo; i++)
            {

                PALLETS pallet = PALLETSs
                    .OrderBy(ob => Convert.ToInt32(ob.PalletType, CultureInfo.InvariantCulture))
                    .ElementAt(i);

                // New Page Trigger for pallets
                if (startY >= 683f || itemsPrinted >= _itemsPerPage)
                {
                    _currentPalletIndex = i; // Continue with this pallet on the next page.
                    e.HasMorePages = true;
                    CurrentPage += 1;
                    return default; // Ensure it exits and waits for the next page
                }

                // Print the pallet item
                startY = PrintDocument_PrintPalletItems(e, PALLETSs, pallet, objectContainerStartPointXKey, objectContainerStartPointXvalue, startY, printHeader);
                printHeader = false;
                startY += 18.0f;
                itemsPrinted += 1;

            }

            // Mark all pallets printed
            _currentPalletIndex = PALLETSs.Count;

        }

        // ////////////////////////
        // Print Package Items after pallets
        // ////////////////////////
        if (PACKAGESs.Any())
        {
            for (int i = _currentPackageIndex; i < PACKAGESs.Count; i++)
            {
                if (startY >= 683f || itemsPrinted >= _itemsPerPage)
                {
                    _currentPackageIndex = i; // Continue with this package on the next page.
                    e.HasMorePages = true;
                    CurrentPage += 1;
                    return default;
                }

                startY = PrintDocument_PrintPackageItem(
                    e,
                    PACKAGESs[i],
                    objectContainerStartPointXKey,
                    objectContainerStartPointXvalue,
                    startY,
                    printHeader);
                printHeader = false;
                startY += 18.0f;
                itemsPrinted += 1;
                _currentPackageIndex = i + 1;
            }
        }

        // ////////////////////////
        // Print Customer Hours
        // ////////////////////////
        if (Bol.PrintHoursOfOperation == true &&
            _currentPalletIndex == PALLETSs.Count &&
            _currentPackageIndex == PACKAGESs.Count &&
            !_printedHours)
        {

            startY += 8.0f;

            // New Page Trigger before printing Hours of Operation
            if (startY >= 773f)
            {
                startY = objectContainerStartPointY;
                e.HasMorePages = true;
                CurrentPage += 1;
                return default; // Ensure it exits and waits for the next page
            }

            PrintDocument_HoursOfOperation(e, Bol, objectContainerStartPointXKey, objectContainerStartPointXvalue, startY);

            // Prevent reprinting
            _printedHours = true;

            // We always print a summary, so bump down the StartY coordinate
            startY += 72.0f;

        }

        // ////////////////////////
        // Print Pallet Summary
        // ////////////////////////
        // New Page Trigger before printing Shipment Summary
        if (_currentPalletIndex == PALLETSs.Count &&
            _currentPackageIndex == PACKAGESs.Count &&
            !_printedSummary)
        {

            if (startY >= 716f)
            {
                startY = objectContainerStartPointY;
                e.HasMorePages = true;
                CurrentPage += 1;
                return default; // Ensure it exits and waits for the next page
            }

            PrintDocument_ShipmentSummary(e, PALLETSs, objectContainerStartPointXKey, startY);

            // Prevent reprinting
            _printedSummary = true;

        }

        // No more pages left to print
        e.HasMorePages = false;

        return startY;

    }

    /// <summary>
    /// Print the Pallet, Misc and Oversized Items, return the startY coordinate
    /// </summary>
    /// <param name="e"></param>
    /// <param name="pallets"></param>
    /// <param name="pallet"></param>
    /// <param name="objectContainerStartPointXKey"></param>
    /// <param name="objectContainerStartPointXvalue"></param>
    /// <param name="objectContainerStartPointY"></param>
    /// <param name="printHeader"></param>
    /// <returns></returns>
    private float PrintDocument_PrintPalletItems(PrintPageEventArgs e, List<PALLETS> pallets, PALLETS pallet, float objectContainerStartPointXKey, float objectContainerStartPointXvalue, float objectContainerStartPointY, bool printHeader)
    {

        string limitedDescription = Conversions.ToString(pallet.Description.Length > 21 ? pallet.Description.Substring(0, 30) : pallet.Description);
        string palletTypeString = "Pallet";
        switch (pallet.PalletType)
        {
            case 0:
                {
                    palletTypeString = "Pallet";
                    break;
                }
            case 1:
                {
                    palletTypeString = "Misc";
                    break;
                }
            case 2:
                {
                    palletTypeString = "OverSized";
                    break;
                }
        }

        float startX = objectContainerStartPointXKey;
        float startY = objectContainerStartPointY;
        float spacing = 35.0f; // Extra space between columns

        // Measure text sizes for alignment
        SizeF typeHeaderTextSize = e.Graphics.MeasureString("Type", _fontMedium9Bold);
        SizeF descriptionHeaderTextSize = e.Graphics.MeasureString("Description", _fontMedium9Bold);
        SizeF dimensionsHeaderTextSize = e.Graphics.MeasureString("Dimensions", _fontMedium9Bold);
        SizeF weightHeaderTextSize = e.Graphics.MeasureString("Weight", _fontMedium9Bold);
        SizeF volumeHeaderTextSize = e.Graphics.MeasureString("Volume", _fontMedium9Bold);
        SizeF cartonsHeaderTextSize = e.Graphics.MeasureString("Cartons", _fontMedium9Bold);
        SizeF nmfcClassTextSize = e.Graphics.MeasureString("NMFC/Class", _fontMedium9Bold);

        // Define column start positions
        float column1StartX = startX;
        float column2StartX = column1StartX + typeHeaderTextSize.Width + spacing;
        float column3StartX = column2StartX + descriptionHeaderTextSize.Width + 110.0f;
        float column4StartX = column3StartX + dimensionsHeaderTextSize.Width + spacing;
        float column5StartX = column4StartX + volumeHeaderTextSize.Width + spacing;
        float column6StartX = column5StartX + weightHeaderTextSize.Width + spacing;
        float column7StartX = column6StartX + cartonsHeaderTextSize.Width + spacing - 20.0f;

        // Draw headers using column start positions
        if (printHeader)
        {

            e.Graphics.DrawString("Type", _fontMedium9Bold, _blackBrush, column1StartX, startY);
            e.Graphics.DrawString("Description", _fontMedium9Bold, _blackBrush, column2StartX, startY);
            e.Graphics.DrawString("Dimensions", _fontMedium9Bold, _blackBrush, column3StartX, startY);
            e.Graphics.DrawString("Volume", _fontMedium9Bold, _blackBrush, column4StartX, startY);
            e.Graphics.DrawString("Weight", _fontMedium9Bold, _blackBrush, column5StartX, startY);
            e.Graphics.DrawString("Cartons", _fontMedium9Bold, _blackBrush, column6StartX, startY);
            e.Graphics.DrawString("NMFC/Class", _fontMedium9Bold, _blackBrush, column7StartX, startY);

            // Bump up the StartY value
            startY += 20.0f;

            // Draw the line under the header section
            e.Graphics.DrawLine(_blackThinPen, objectContainerStartPointXKey, startY, _printArea.Width - 40.0f, startY);
            startY += 12.0f;

        }

        // Draw the data under the header 

        // Pallet Type String
        SizeF palletTypeTextSize = e.Graphics.MeasureString(palletTypeString, _fontMedium9Normal);
        e.Graphics.DrawString(palletTypeString, _fontMedium9Normal, _blackBrush, column1StartX, startY);

        // Pallet Description String
        SizeF descriptionTextSize = e.Graphics.MeasureString(limitedDescription, _fontMedium9Normal);
        e.Graphics.DrawString(limitedDescription, _fontMedium9Normal, _blackBrush, column2StartX, startY);

        // Pallet Dimensions String
        // Print the pallet dimensions if the user chooses to use them
        var dimensionUnit = GetDimensionUnit(pallet.VolumeType);
        e.Graphics.DrawString(pallet.Length.ToString() + " x " + pallet.Width.ToString() + " x " + pallet.Height.ToString() + " " + dimensionUnit, _fontMedium9Normal, _blackBrush, column3StartX, startY);

        // Pallet Volume String
        if (pallet.Volume > 0.00d)
        {

            var displayVolume = pallet.Volume;
            var displayVolumeType = dimensionUnit == "CM" ? "CM" : "CF";
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


            e.Graphics.DrawString(displayVolume.ToString("F2") + " " + displayVolumeType, _fontMedium9Normal, _blackBrush, column4StartX, startY);

        }

        // Pallet Weight String
        e.Graphics.DrawString(pallet.Weight.ToString() + " " + pallet.WeightType, _fontMedium9Normal, _blackBrush, column5StartX, startY);

        // Pallet Cartons or Box Count String
        e.Graphics.DrawString(pallet.BoxCount.ToString(), _fontMedium9Normal, _blackBrush, column6StartX, startY);

        // Determine if we should print the class code, or the class text
        string freightClassCodeString = pallet.ClassCode.ToString();
        if (!string.IsNullOrEmpty(pallet.ClassText))
        {
            freightClassCodeString = pallet.ClassText;
        }

        // Pallet Class Code
        if (pallet.ClassCode is not null)
        {
            e.Graphics.DrawString(freightClassCodeString, _fontMedium9Normal, _blackBrush, column7StartX, startY);
        }

        return startY;

    }

    /// <summary>
    /// Prints a package in the same shipment-detail table used for pallets.
    /// </summary>
    private float PrintDocument_PrintPackageItem(
        PrintPageEventArgs e,
        PACKAGES package,
        float objectContainerStartPointXKey,
        float objectContainerStartPointXvalue,
        float objectContainerStartPointY,
        bool printHeader)
    {
        string description = package.PackageDescription ?? string.Empty;
        string limitedDescription = description.Length > 30
            ? string.Concat(description.AsSpan(0, 30), "...")
            : description;

        float startX = objectContainerStartPointXKey;
        float startY = objectContainerStartPointY;
        const float spacing = 35.0f;

        SizeF typeHeaderTextSize = e.Graphics.MeasureString("Type", _fontMedium9Bold);
        SizeF descriptionHeaderTextSize = e.Graphics.MeasureString("Description", _fontMedium9Bold);
        SizeF dimensionsHeaderTextSize = e.Graphics.MeasureString("Dimensions", _fontMedium9Bold);
        SizeF weightHeaderTextSize = e.Graphics.MeasureString("Weight", _fontMedium9Bold);
        SizeF volumeHeaderTextSize = e.Graphics.MeasureString("Volume", _fontMedium9Bold);
        SizeF cartonsHeaderTextSize = e.Graphics.MeasureString("Cartons", _fontMedium9Bold);

        float column1StartX = startX;
        float column2StartX = column1StartX + typeHeaderTextSize.Width + spacing;
        float column3StartX = column2StartX + descriptionHeaderTextSize.Width + 110.0f;
        float column4StartX = column3StartX + dimensionsHeaderTextSize.Width + spacing;
        float column5StartX = column4StartX + volumeHeaderTextSize.Width + spacing;
        float column6StartX = column5StartX + weightHeaderTextSize.Width + spacing;
        float column7StartX = column6StartX + cartonsHeaderTextSize.Width + spacing - 20.0f;

        if (printHeader)
        {
            e.Graphics.DrawString("Type", _fontMedium9Bold, _blackBrush, column1StartX, startY);
            e.Graphics.DrawString("Description", _fontMedium9Bold, _blackBrush, column2StartX, startY);
            e.Graphics.DrawString("Dimensions", _fontMedium9Bold, _blackBrush, column3StartX, startY);
            e.Graphics.DrawString("Volume", _fontMedium9Bold, _blackBrush, column4StartX, startY);
            e.Graphics.DrawString("Weight", _fontMedium9Bold, _blackBrush, column5StartX, startY);
            e.Graphics.DrawString("Cartons", _fontMedium9Bold, _blackBrush, column6StartX, startY);
            e.Graphics.DrawString("NMFC/Class", _fontMedium9Bold, _blackBrush, column7StartX, startY);

            startY += 20.0f;
            e.Graphics.DrawLine(_blackThinPen, objectContainerStartPointXKey, startY, _printArea.Width - 40.0f, startY);
            startY += 12.0f;
        }

        e.Graphics.DrawString("Package", _fontMedium9Normal, _blackBrush, column1StartX, startY);
        e.Graphics.DrawString(limitedDescription, _fontMedium9Normal, _blackBrush, column2StartX, startY);

        string dimensionUnit = GetDimensionUnit(package.VolumeType);
        e.Graphics.DrawString(
            $"{package.Length} x {package.Width} x {package.Height} {dimensionUnit}",
            _fontMedium9Normal,
            _blackBrush,
            column3StartX,
            startY);

        if (package.Volume > 0.0f)
        {
            float displayVolume = package.Volume;
            string displayVolumeType = dimensionUnit == "CM" ? "CM" : "CF";
            if (dimensionUnit == "IN")
            {
                displayVolume /= 1728.0f;
            }
            else if (dimensionUnit == "CM")
            {
                displayVolume /= 1000000.0f;
            }

            e.Graphics.DrawString(
                $"{Math.Round(displayVolume, 2):F2} {displayVolumeType}",
                _fontMedium9Normal,
                _blackBrush,
                column4StartX,
                startY);
        }

        e.Graphics.DrawString($"{package.Weight} {package.WeightType}", _fontMedium9Normal, _blackBrush, column5StartX, startY);
        e.Graphics.DrawString(package.UnitCount.ToString(), _fontMedium9Normal, _blackBrush, column6StartX, startY);

        string freightClass = package.ClassCode?.Name
            ?? package.ClassCode?.CodeNumber?.ToString(CultureInfo.InvariantCulture)
            ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(freightClass))
        {
            e.Graphics.DrawString(freightClass, _fontMedium9Normal, _blackBrush, column7StartX, startY);
        }

        return startY;
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
    /// Print Customer Hours of Operation
    /// </summary>
    /// <param name="e"></param>
    /// <param name="bolRequest"></param>
    /// <param name="objectContainerStartPointXKey"></param>
    /// <param name="objectContainerStartPointY"></param>
    private void PrintDocument_HoursOfOperation(PrintPageEventArgs e, BILLOFLADINGS bol, float objectContainerStartPointXKey, float objectContainerStartPointXValue, float objectContainerStartPointY)
    {
        Dictionary<string, string> schedule = BuildHoursSchedule(bol);

        // Draw title
        e.Graphics.DrawString("Ship to: Hours of Operation", _fontMedium10Bold, _blackBrush, objectContainerStartPointXKey, objectContainerStartPointY);
        objectContainerStartPointY += 6.0f;

        // Starting position for horizontal text
        float currentX = objectContainerStartPointXKey;
        float currentY = objectContainerStartPointY + 16.0f;

        foreach (KeyValuePair<string, string> day in schedule)
        {

            // When reaching Saturday, reset X and move down
            if (day.Key == "Sat")
            {
                currentX = objectContainerStartPointXKey; // Reset X position
                currentY += 20.0f; // Move to new line
            }

            string[] hours = day.Value.Split('-');
            string open = hours.Length == 2 ? hours[0].Trim() : day.Value;
            string close = hours.Length == 2 ? hours[1].Trim() : "";

            string dayText = day.Key + " ";
            string openText = open == "Closed" ? "Closed " : open + " ";
            string toText = !string.IsNullOrEmpty(close) ? "to " : "";
            string closeText = !string.IsNullOrEmpty(close) ? close + " " : "";

            // Measure text width to adjust X position dynamically
            float dayTextWidth = e.Graphics.MeasureString(dayText, _fontMedium9Bold).Width;
            float openTextWidth = e.Graphics.MeasureString(openText, _fontMedium9Normal).Width;
            float toTextWidth = e.Graphics.MeasureString(toText, _fontMedium9Normal).Width;
            float closeTextWidth = e.Graphics.MeasureString(closeText, _fontMedium9Normal).Width;

            // Draw elements horizontally
            e.Graphics.DrawString(dayText, _fontMedium9Bold, _blackBrush, currentX, currentY);
            currentX += dayTextWidth;

            e.Graphics.DrawString(openText, _fontMedium9Normal, _blackBrush, currentX, currentY);
            currentX += openTextWidth;

            e.Graphics.DrawString(toText, _fontMedium9Normal, _blackBrush, currentX, currentY);
            currentX += toTextWidth;

            e.Graphics.DrawString(closeText, _fontMedium9Normal, _blackBrush, currentX, currentY);
            currentX += closeTextWidth + 10.0f; // Add extra spacing for next entry

        }

    }

    private static Dictionary<string, string> BuildHoursSchedule(BILLOFLADINGS bol)
    {
        return (bol.ShipToLocation?.HoursOfOperation?.Days ?? [])
            .OrderBy(day => (int)day.Day)
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
        int totalCount = pallets.Count + PACKAGESs.Count;
        decimal totalWeight = pallets.Sum(tw => tw.Weight) + PACKAGESs.Sum(package => package.Weight);
        decimal totalVolumeCubicFeet =
            (decimal)pallets.Sum(tv => tv.Volume) +
            (decimal)PACKAGESs.Sum(package => package.Volume);
        string? weightType = pallets
            .Select(item => item.WeightType)
            .Concat(PACKAGESs.Select(item => item.WeightType))
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
        string? volumeType = pallets
            .Select(item => item.VolumeType)
            .Concat(PACKAGESs.Select(item => item.VolumeType))
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));

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
        startPointY += 24.0f;

        float spacing = 32.0f; // Extra spacing between items

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
        float currentY = startPointY;

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
        _currentPalletIndex = 0;
        _currentPackageIndex = 0;
        _printedHours = false;
        _printedSummary = false;

    }

    // Override the OnPrintPage method to call your custom PrintPage event handler
    protected override void OnPrintPage(PrintPageEventArgs e)
    {
        base.OnPrintPage(e);
        PrintDocument_PrintPage(this, e);
    }

}
