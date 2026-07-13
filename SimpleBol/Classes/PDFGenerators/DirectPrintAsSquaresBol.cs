using SimpleBol.Models.MongoDb;
using SimpleBol.Properties;
using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System.IO;
using System.Diagnostics;

namespace SimpleBol.Classes.PDFGenerators
{
    public class DirectPrintAsSquaresBol
    {
        public BILLOFLADINGS Bol { get; set; } = null!;
        public string? BolDocumentPath { get; set; }
        public string? DocumentName { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        private BILLOFLADINGS BolCopy { get; set; } = null!;
        private Document _document { get; set; }
        private bool _hasMorePages { get; set; }
        private Page? _currentPage;
        private readonly PageDimensions _pageDimensions;

        // Track what has printed
        private int containerCount = 0;
        private int palletCount = 0;
        private int packageCount = 0;

        public enum PdfMode
        {
            Preview,
            Transmit
        }

        public DirectPrintAsSquaresBol()
        {

            string productName = AppInfo.Name;

            // Create a new Document within this object initialization

            _document = new Document()
            {
                Author = productName,
                Title = this.DocumentName ?? "Bill of Lading",
                Subject = "Bill of Ladding",
                Creator = AppInfo.Name,
                InitialPage = 1,
                CompressionLevel = 0,
                InitialPageZoom = PageZoom.FitPage
            };

            // Create the Page Dimensions within this object initialization
            _pageDimensions = new PageDimensions(PageSize.Letter, PageOrientation.Portrait)
            {
                BottomMargin = 0,
                TopMargin = 0,
                LeftMargin = 0,
                RightMargin = 0
            };

            // Just create the page upon this object initialization
            AddDocumentPage(_document);

        }

        public async Task<bool> GeneratePdfAsync()
        {

            bool result = false;

            // Paginate the Pallets and Packages, deduct 1 because we start with zero or 0
            int startIndex = (CurrentPage * ItemsPerPage) - ItemsPerPage;
            int endIndex = Math.Min(startIndex + ItemsPerPage, TotalItems) - 1;

            if (this.Bol != null)
            {

                if (_document != null)
                {
                    try
                    {

                        this.CurrentPage = 1;
                        this.containerCount = this.Bol.Containers != null ? this.Bol.Containers.Count() : 0;
                        this.palletCount = this.Bol.Pallets != null ? this.Bol.Pallets.Count() : 0;
                        this.packageCount = this.Bol.Packages != null ? this.Bol.Packages.Count() : 0;

                        if (_currentPage != null)
                        {
                            PrintBolDocument();
                            PrintBolDocumentShipmentDetails(startIndex, endIndex);
                        }

                        if (!this._hasMorePages)
                        {
                            SaveDocument(_document);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }


                }

            }

            await Task.Delay(100);

            return result;

        }

        // Add another page to the document, and print the BOL Document Template
        private void AddDocumentPage(Document document)
        {
            _currentPage = new Page(_pageDimensions);

            document?.Pages.Add(_currentPage);

            // Add the layout grid
            // _currentPage.Elements.Add(new LayoutGrid());

        }

        private void PrintBolDocument()
        {

            if (_currentPage != null)
            {
                // Main Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle mainRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(20F, 20F, 572.5F, 750F, Grayscale.Black, null!, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(mainRectangle);

                // Bill of Ladding
                ceTe.DynamicPDF.PageElements.Label bolText = new ceTe.DynamicPDF.PageElements.Label("BILL OF LADDING", 442.5F, 22F, 140F, 16F, ceTe.DynamicPDF.Font.HelveticaBold, 16, TextAlign.Right, RgbColor.Black);
                _currentPage.Elements.Add(bolText);

                if (Bol.ShipFromVendor != null)
                {
                    if (Bol.ShipFromVendor.CompanyName != null)
                    {
                        // Ship From Company
                        ceTe.DynamicPDF.PageElements.Label shipFromText = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipFromVendor.CompanyName, 24F, 22F, 250F, 16F, ceTe.DynamicPDF.Font.HelveticaBold, 16, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(shipFromText);
                    }

                }

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Ship From Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle shipFromRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(20F, 55F, 285F, 100F, Grayscale.Black, null!, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(shipFromRectangle);

                // Ship From Title Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle shipFromTitleRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(20F, 55F, 285F, 12F, Grayscale.Black, Grayscale.Black, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(shipFromTitleRectangle);

                // Ship From Title Label
                ceTe.DynamicPDF.PageElements.Label shipFromTitleText = new ceTe.DynamicPDF.PageElements.Label("SHIP FROM", 29F, 56.5F, 50F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.White);
                _currentPage.Elements.Add(shipFromTitleText);

                PointF shipFromStartPoint = new PointF(20F, 52F);
                float shipFromRectangleHeight = 10F;

                // Ship From Data
                if (Bol.ShipFromVendor != null && Bol.ShipFromLocation != null)
                {
                    // Define address information
                    List<string> shipFromAddressLines = new List<string>();

                    if (!string.IsNullOrEmpty(Bol.ShipFromVendor.CompanyName))
                        shipFromAddressLines.Add(Bol.ShipFromVendor.CompanyName);

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.Address1))
                        shipFromAddressLines.Add(Bol.ShipFromLocation.Address1);

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.Address2))
                        shipFromAddressLines.Add(Bol.ShipFromLocation.Address2);

                    // Build a single line here, City, Region, Postal Code
                    string shipFromCityRegionPostal = "";

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.City))
                        shipFromCityRegionPostal += " " + Bol.ShipFromLocation.City;

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.RegionName))
                        shipFromCityRegionPostal += " " + Bol.ShipFromLocation.RegionAbbr;

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.PostalCode))
                        shipFromCityRegionPostal += " " + Bol.ShipFromLocation.PostalCode;

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.CountryName))
                        shipFromCityRegionPostal += " " + Bol.ShipFromLocation.CountryAbbr;

                    shipFromAddressLines.Add(shipFromCityRegionPostal);
                    shipFromAddressLines.Add("\b\n");

                    if (!string.IsNullOrEmpty(Bol.ShipFromLocation.ContactName) && !string.IsNullOrEmpty(Bol.ShipFromLocation.ContactPhone))
                        shipFromAddressLines.Add("Contact: " + Bol.ShipFromLocation.ContactName + ": " + Bol.ShipFromLocation.ContactPhone);

                    // Calculate position for drawing address lines
                    float shipFromAddressStartX = shipFromStartPoint.X;
                    float shipFromAddressStartY = shipFromStartPoint.Y + shipFromRectangleHeight + 10F; // Adjust vertical position

                    foreach (string line in shipFromAddressLines)
                    {
                        SizeF lineSize = new SizeF(280F, 11F);
                        float lineX = shipFromAddressStartX + 9F; // Indent the address
                        float lineY = shipFromAddressStartY;

                        ceTe.DynamicPDF.PageElements.Label shipFromAddressLabel = new ceTe.DynamicPDF.PageElements.Label(line, lineX, lineY, 280F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(shipFromAddressLabel);

                        // Increment the Y position for the next line
                        shipFromAddressStartY += lineSize.Height + 1.5F; // Add a small vertical spacing
                    }

                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                // ShipTo Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle shipToRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(20F, 150F, 285F, 100F, Grayscale.Black, null!, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(shipToRectangle);

                // ShipTo Title Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle shipToTitleRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(20F, 150F, 285F, 12F, Grayscale.Black, Grayscale.Black, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(shipToTitleRectangle);

                // Ship To Title Label
                ceTe.DynamicPDF.PageElements.Label shipToTitleText = new ceTe.DynamicPDF.PageElements.Label("SHIP TO", 29F, 152F, 50F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.White);
                _currentPage.Elements.Add(shipToTitleText);

                PointF shipToStartPoint = new PointF(20F, 146F);
                float shipToRectangleHeight = 10F;

                // Ship From Data
                if (Bol.ShipToCustomer != null && Bol.ShipToLocation != null)
                {
                    // Define address information
                    List<string> shipToAddressLines = new List<string>();

                    if (!string.IsNullOrEmpty(Bol.ShipToCustomer.CompanyName))
                        shipToAddressLines.Add(Bol.ShipToCustomer.CompanyName);

                    if (!string.IsNullOrEmpty(Bol.ShipToCustomer.Address1))
                        shipToAddressLines.Add(Bol.ShipToCustomer.Address1);

                    if (!string.IsNullOrEmpty(Bol.ShipToCustomer.Address2))
                        shipToAddressLines.Add(Bol.ShipToCustomer.Address2);

                    // Build a single line here, City, Region, Postal Code
                    string shipToCityRegionPostal = "";

                    if (!string.IsNullOrEmpty(Bol.ShipToCustomer.City))
                        shipToCityRegionPostal += " " + Bol.ShipToCustomer.City;

                    if (!string.IsNullOrEmpty(Bol.ShipToLocation.RegionName))
                        shipToCityRegionPostal += " " + Bol.ShipToLocation.RegionAbbr;

                    if (!string.IsNullOrEmpty(Bol.ShipToLocation.PostalCode))
                        shipToCityRegionPostal += " " + Bol.ShipToLocation.PostalCode;

                    if (!string.IsNullOrEmpty(Bol.ShipToLocation.CountryName))
                        shipToCityRegionPostal += " " + Bol.ShipToLocation.CountryAbbr;

                    shipToAddressLines.Add(shipToCityRegionPostal);
                    shipToAddressLines.Add("\b\n");

                    if (!string.IsNullOrEmpty(Bol.ShipToLocation.ContactName) && !string.IsNullOrEmpty(Bol.ShipToLocation.ContactPhone))
                        shipToAddressLines.Add("Contact: " + Bol.ShipToLocation.ContactName + ": " + Bol.ShipToLocation.ContactPhone);

                    // Calculate position for drawing address lines
                    float shipToAddressStartX = shipToStartPoint.X;
                    float shipToAddressStartY = shipToStartPoint.Y + shipToRectangleHeight + 10F; // Adjust vertical position

                    foreach (string line in shipToAddressLines)
                    {
                        SizeF lineSize = new SizeF(280F, 11F);
                        float lineX = shipToAddressStartX + 9F; // Indent the address
                        float lineY = shipToAddressStartY;

                        ceTe.DynamicPDF.PageElements.Label shipToAddressLabel = new ceTe.DynamicPDF.PageElements.Label(line, lineX, lineY, 280F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(shipToAddressLabel);

                        // Increment the Y position for the next line
                        shipToAddressStartY += lineSize.Height + 1.5F; // Add a small vertical spacing
                    }

                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Bol Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle bolRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(305F, 55F, 287F, 50F, Grayscale.Black, null!, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(bolRectangle);

                // Bol Number Label
                ceTe.DynamicPDF.PageElements.Label bolNumberLabel = new ceTe.DynamicPDF.PageElements.Label("Bill of Ladding Number: ", 314F, 63F, 120F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(bolNumberLabel);

                if (Bol.BolNumber != null)
                {
                    // Bol Number Text/Value
                    ceTe.DynamicPDF.PageElements.Label bolNumberValue = new ceTe.DynamicPDF.PageElements.Label(Bol.BolNumber, 450F, 63F, 135F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Right, RgbColor.Black);
                    _currentPage.Elements.Add(bolNumberValue);

                }

                if (Bol.ShipFromVendor != null)
                {
                    if (Bol.ShipFromVendor.CompanyName != null)
                    {
                        // Vendor PO Number Label
                        ceTe.DynamicPDF.PageElements.Label vendorPoNumberLabel = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipFromVendor.CompanyName + " PO Id: ", 314F, 75F, 120F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(vendorPoNumberLabel);

                        if (Bol.ShipperReferenceNumber != null)
                        {
                            // Vendor PO Number Value
                            ceTe.DynamicPDF.PageElements.Label vendorPoNumberValue = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipperReferenceNumber, 450F, 75F, 135F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Right, RgbColor.Black);
                            _currentPage.Elements.Add(vendorPoNumberValue);
                        }

                    }

                    if (Bol.OrderNumber != null)
                    {
                        // Vendor Order Number Label
                        ceTe.DynamicPDF.PageElements.Label vendorOrderNumberLabel = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipFromVendor.CompanyName + " Order Number: ", 314F, 88F, 120F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(vendorOrderNumberLabel);

                        // Vendor PO Number Value
                        ceTe.DynamicPDF.PageElements.Label vendorOrderNumberValue = new ceTe.DynamicPDF.PageElements.Label(Bol.OrderNumber, 450F, 88F, 135F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Right, RgbColor.Black);
                        _currentPage.Elements.Add(vendorOrderNumberValue);

                    }

                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Carrier Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle carrierRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(305F, 105F, 287F, 50F, Grayscale.Black, null!, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(carrierRectangle);

                if (Bol.Shipper != null)
                {
                    // Carrier Name Label
                    ceTe.DynamicPDF.PageElements.Label carrierNameLabel = new ceTe.DynamicPDF.PageElements.Label("Carrier Name: ", 314F, 111F, 120F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                    _currentPage.Elements.Add(carrierNameLabel);


                    if (Bol.Shipper.CompanyName != null)
                    {
                        // Carrier Name Value
                        ceTe.DynamicPDF.PageElements.Label carrierNameValue = new ceTe.DynamicPDF.PageElements.Label(Bol.Shipper.CompanyName, 450F, 111F, 135F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Right, RgbColor.Black);
                        _currentPage.Elements.Add(carrierNameValue);
                    }

                }

                // Quote Number Label
                ceTe.DynamicPDF.PageElements.Label quoteNumberLabel = new ceTe.DynamicPDF.PageElements.Label("Quote Number: ", 314F, 123F, 120F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(quoteNumberLabel);

                // Quote Number Value
                if (Bol.ShipperQuoteNumber != null)
                {
                    // Quote Number Value
                    ceTe.DynamicPDF.PageElements.Label quoteNumberValue = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipperQuoteNumber, 450F, 123F, 135F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Right, RgbColor.Black);
                    _currentPage.Elements.Add(quoteNumberValue);
                }

                // Quoted Price Label
                ceTe.DynamicPDF.PageElements.Label quotedPriceLabel = new ceTe.DynamicPDF.PageElements.Label("Quoted Price: ", 314F, 135F, 120F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(quotedPriceLabel);

                // Quoted Price Value
                ceTe.DynamicPDF.PageElements.Label quotedPriceValue = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipperQuotePrice.ToString("c"), 450F, 135F, 135F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Right, RgbColor.Black);
                _currentPage.Elements.Add(quotedPriceValue);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // 3rd Party Bill To Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle thirdPartyBillToRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(305F, 150F, 287F, 100F, Grayscale.Black, null!, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(thirdPartyBillToRectangle);

                // 3rd Party Bill To Title Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle thirdPartBillTitleRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(305F, 150F, 287F, 12F, Grayscale.Black, Grayscale.Black, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(thirdPartBillTitleRectangle);

                // 3rd Party Bill To Title Label
                ceTe.DynamicPDF.PageElements.Label thirdPartBillToTitleText = new ceTe.DynamicPDF.PageElements.Label("THIRD PARTY/FREIGHT CHARGES BILL TO", 314F, 152F, 287F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.White);
                _currentPage.Elements.Add(thirdPartBillToTitleText);

                // Third Party Billing Information
                if (Bol.BillToId != null)
                {
                    if (Bol.BillToAccount != null)
                    {
                        PointF freightChargeStartPoint = new PointF(305F, 150F);
                        float freightChargeRectangleHeight = 10F;

                        // Define address information
                        List<string> billTo3rdPartyAddressLines = new List<string>();

                        if (!string.IsNullOrEmpty(Bol.BillToAccount.CompanyName))
                            billTo3rdPartyAddressLines.Add(Bol.BillToAccount.CompanyName);

                        if (!string.IsNullOrEmpty(Bol.BillToAccount.Address1))
                            billTo3rdPartyAddressLines.Add(Bol.BillToAccount.Address1);

                        // Build a single line here, City, Region, Postal Code
                        string billTo3rdPartyCityRegionPostal = "";

                        if (!string.IsNullOrEmpty(Bol.BillToAccount.City))
                            billTo3rdPartyCityRegionPostal += " " + Bol.BillToAccount.City;

                        if (!string.IsNullOrEmpty(Bol.BillToAccount.RegionLongName))
                            billTo3rdPartyCityRegionPostal += " " + Bol.BillToAccount.RegionAbbr;

                        if (!string.IsNullOrEmpty(Bol.BillToAccount.PostalCode))
                            billTo3rdPartyCityRegionPostal += " " + Bol.BillToAccount.PostalCode;

                        if (!string.IsNullOrEmpty(Bol.BillToAccount.CountryLongName))
                            billTo3rdPartyCityRegionPostal += " " + Bol.BillToAccount.CountryAbbr;

                        billTo3rdPartyAddressLines.Add(billTo3rdPartyCityRegionPostal);

                        // Calculate position for drawing address lines
                        float billTo3rdPartyAddressStartX = freightChargeStartPoint.X;
                        float billTo3rdPartyAddressStartY = freightChargeStartPoint.Y + freightChargeRectangleHeight + 6F; // Adjust vertical position

                        foreach (string line in billTo3rdPartyAddressLines)
                        {
                            SizeF lineSize = new SizeF(280F, 11F);
                            float lineX = billTo3rdPartyAddressStartX + 12.5F; // Indent the address
                            float lineY = billTo3rdPartyAddressStartY;

                            ceTe.DynamicPDF.PageElements.Label billTo3rdPartAddressLabel = new ceTe.DynamicPDF.PageElements.Label(line, lineX, lineY, 280F, 10F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(billTo3rdPartAddressLabel);

                            // Increment the Y position for the next line
                            billTo3rdPartyAddressStartY += lineSize.Height + 1F; // Add a small vertical spacing
                        }
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Freight Charges are Prepaid unless marked otherwise

                // Draw a line to make a container for the Freight Charges are Prepaid
                ceTe.DynamicPDF.PageElements.Line freightChargesPrepaidLine = new ceTe.DynamicPDF.PageElements.Line(305F, 210F, 592F, 210F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(freightChargesPrepaidLine);

                // 3rd Party Frieght Charges Billed to Label
                ceTe.DynamicPDF.PageElements.Label freightChargesPrepaidLabel = new ceTe.DynamicPDF.PageElements.Label("Freight charges are prepaid unless marked otherwise", 317.5F, 214F, 280F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(freightChargesPrepaidLabel);

                if (Bol.FreightPrePaid == true)
                {
                    // 3rd Party Frieght Charges Billed Marked as Selected
                    ceTe.DynamicPDF.PageElements.Label freightChargesPrepaidMarkAsSelectedLabel = new ceTe.DynamicPDF.PageElements.Label("Prepaid _____X_____   Collect ___________   3rd Party _________", 317.5F, 236F, 280F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                    _currentPage.Elements.Add(freightChargesPrepaidMarkAsSelectedLabel);
                }
                else if (Bol.CodAmount > 0.00M)
                {
                    // 3rd Party Frieght Charges Billed Marked as Selected
                    ceTe.DynamicPDF.PageElements.Label freightChargesPrepaidMarkAsSelectedLabel = new ceTe.DynamicPDF.PageElements.Label("Prepaid ___________   Collect _____X_____   3rd Party _________", 317.5F, 236F, 280F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                    _currentPage.Elements.Add(freightChargesPrepaidMarkAsSelectedLabel);
                }
                else if (Bol.BillToAccount != null)
                {
                    // 3rd Party Frieght Charges Billed Marked as Selected
                    ceTe.DynamicPDF.PageElements.Label freightChargesPrepaidMarkAsSelectedLabel = new ceTe.DynamicPDF.PageElements.Label("Prepaid ___________   Collect _____________   3rd Party ____X____", 317.5F, 236F, 280F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                    _currentPage.Elements.Add(freightChargesPrepaidMarkAsSelectedLabel);
                }
                else
                {
                    // 3rd Party Frieght Charges Billed Marked as Selected
                    ceTe.DynamicPDF.PageElements.Label freightChargesPrepaidMarkAsSelectedLabel = new ceTe.DynamicPDF.PageElements.Label("Prepaid _________   Collect ___________   3rd Party ___________", 317.5F, 236F, 280F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                    _currentPage.Elements.Add(freightChargesPrepaidMarkAsSelectedLabel);
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                // Shipment Details Title Rectangle
                ceTe.DynamicPDF.PageElements.Rectangle shipmentDetailsTitleRectangle = new ceTe.DynamicPDF.PageElements.Rectangle(20F, 250F, 572.5F, 12F, Grayscale.Black, Grayscale.Black, 1F, LineStyle.Solid);
                _currentPage.Elements.Add(shipmentDetailsTitleRectangle);


                // Shipment Details To Title Label
                ceTe.DynamicPDF.PageElements.Label shipmentDetailsTitleText = new ceTe.DynamicPDF.PageElements.Label("SHIPMENT DETAILS", 29F, 252F, 285F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.White);
                _currentPage.Elements.Add(shipmentDetailsTitleText);

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Print Shipment Details Here - But we are print this in a function so we can add more pages if needed
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ///Bottom Part of the BOL, the legal section of the form
                /// We are now 570 points from the top of the page

                // Horizontal line to start the bottom part 3/4 of the way down
                ceTe.DynamicPDF.PageElements.Line legalSectionDividerLine = new ceTe.DynamicPDF.PageElements.Line(20F, 570F, 592F, 570F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(legalSectionDividerLine);

                // Ship Value Declaration Legal Text
                ceTe.DynamicPDF.PageElements.Label shipValueDeclarationText = new ceTe.DynamicPDF.PageElements.Label("Where the rate is dependent on value, shippers are required to state specifically in writing the agreed or declared value of the property as follows:\r\nThe agreed or declared value of the property is specifically stated by the shipper to be not exceeding", 26F, 576F, 273F, 44F, ceTe.DynamicPDF.Font.Helvetica, 7, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(shipValueDeclarationText);

                // Vertical line to divide the start the bottom part 3/4 of the way down
                ceTe.DynamicPDF.PageElements.Line legalSectionVerticalDividerLine = new ceTe.DynamicPDF.PageElements.Line(305F, 570F, 305F, 620F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(legalSectionVerticalDividerLine);

                // COD Amount Text and Form Fill Text
                ceTe.DynamicPDF.PageElements.Label codAmountTitle = new ceTe.DynamicPDF.PageElements.Label("COD AMOUNT: $", 311F, 576F, 273F, 44F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(codAmountTitle);

                // COD Amount Line
                ceTe.DynamicPDF.PageElements.Line codAmountLine = new ceTe.DynamicPDF.PageElements.Line(387F, 586F, 581F, 586F, 0.5F, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(codAmountLine);

                // COD Amount Fee Terms Title
                ceTe.DynamicPDF.PageElements.Label codFeeTermsTitle = new ceTe.DynamicPDF.PageElements.Label("Fee Terms:", 311F, 591F, 273F, 44F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(codFeeTermsTitle);

                // COD Amount Fee Term Collect Title
                ceTe.DynamicPDF.PageElements.Label codFeeTermCollectTitle = new ceTe.DynamicPDF.PageElements.Label("Collect", 382F, 591F, 273F, 44F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(codFeeTermCollectTitle);

                // COD Amount Fee Term Prepaid Title
                ceTe.DynamicPDF.PageElements.Label codFeeTermPrepaidTitle = new ceTe.DynamicPDF.PageElements.Label("Prepaid", 467F, 591F, 273F, 44F, ceTe.DynamicPDF.Font.HelveticaBold, 9, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(codFeeTermPrepaidTitle);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// Liability Section

                // Horizontal line for the Liability section
                ceTe.DynamicPDF.PageElements.Line liabilitySectionDividerLine = new ceTe.DynamicPDF.PageElements.Line(20F, 620F, 592F, 620F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(liabilitySectionDividerLine);

                // Liability Limitations Legal Text
                ceTe.DynamicPDF.PageElements.Label liabilityLimitationsText = new ceTe.DynamicPDF.PageElements.Label("Liability Limitation for loss or damage in this shipment may be applicable. See 49 U.S.C. 14706(c)(1)(A) and (B).", 26F, 624F, 597.5F, 16F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Red);
                _currentPage.Elements.Add(liabilityLimitationsText);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// Received Section
                /// 

                // Horizontal line for the Received section
                ceTe.DynamicPDF.PageElements.Line receivedSectionDividerLine = new ceTe.DynamicPDF.PageElements.Line(20F, 640F, 592F, 640F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(receivedSectionDividerLine);

                // Received Contract Agreement Legal Text
                ceTe.DynamicPDF.PageElements.Label receivedContractAgreementText = new ceTe.DynamicPDF.PageElements.Label("RECEIVED, subject to individually determined rates or contracts that have been agreed upon in writing between the carrier and shipper, if applicable, otherwise to the rates, classifications and rules that have been established by the carrier and are available to the shipper, on request, and to all applicable state and federal regulations.", 26F, 645F, 273F, 44F, ceTe.DynamicPDF.Font.Helvetica, 7, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(receivedContractAgreementText);


                // Vertical line for the Received section
                ceTe.DynamicPDF.PageElements.Line receivedSectionVerticalDividerLine = new ceTe.DynamicPDF.PageElements.Line(305F, 640F, 305F, 685F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(receivedSectionVerticalDividerLine);

                // Received Contract Agreement Signature Text
                ceTe.DynamicPDF.PageElements.Label receivedContractAgreementSignatureText = new ceTe.DynamicPDF.PageElements.Label("The carrier shall not make delivery of this shipment without payment of freight and all other lawful charges.", 309F, 645F, 273F, 22F, ceTe.DynamicPDF.Font.Helvetica, 7, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(receivedContractAgreementSignatureText);

                // Received Contract Agreement Signature Line
                ceTe.DynamicPDF.PageElements.Label receivedContractAgreementSignatureLine = new ceTe.DynamicPDF.PageElements.Label("Shipper Signature ______________________________________________________", 309F, 673F, 273F, 22F, ceTe.DynamicPDF.Font.Helvetica, 7, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(receivedContractAgreementSignatureLine);

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// Shipper Signature Section
                /// 

                // Horizontal line for the Shipper Signature section
                ceTe.DynamicPDF.PageElements.Line shipperSignatureSectionDividerLine = new ceTe.DynamicPDF.PageElements.Line(20F, 685F, 592F, 685F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(shipperSignatureSectionDividerLine);

                // Shipper Signature Title Date
                ceTe.DynamicPDF.PageElements.Label shipperSignatureTitleDateTitle = new ceTe.DynamicPDF.PageElements.Label("SHIPPER SIGNATURE / DATE", 29F, 690F, 180F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(shipperSignatureTitleDateTitle);

                // Shipper Signature Legal Statement
                ceTe.DynamicPDF.PageElements.Label shipperSignatureLegalStatementText = new ceTe.DynamicPDF.PageElements.Label("This is to certify that the above named materials are properly classified, packaged, marked and labeled, and are in proper condition for transportation according to the applicable regulations of the DOT.", 26F, 702F, 182F, 40F, ceTe.DynamicPDF.Font.Helvetica, 7, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(shipperSignatureLegalStatementText);

                // Final Shipper Signature Line
                ceTe.DynamicPDF.PageElements.Line shipperSignatureLine = new ceTe.DynamicPDF.PageElements.Line(26F, 765F, 205F, 765F, 0.5F, RgbColor.Black, LineStyle.Solid);
                _currentPage.Elements.Add(shipperSignatureLine);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Vertical Divider Line 2
                ceTe.DynamicPDF.PageElements.Line shipperSignatureSectionVertical2DividerLine = new ceTe.DynamicPDF.PageElements.Line(215F, 685F, 215F, 770F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(shipperSignatureSectionVertical2DividerLine);

                // Trailer Loaded Column Title
                ceTe.DynamicPDF.PageElements.Label trailerLoadedTitle = new ceTe.DynamicPDF.PageElements.Label("TRAILER LOADED", 221F, 690F, 90F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(trailerLoadedTitle);

                // Trailer loaded by Shipper Checkbox
                ceTe.DynamicPDF.PageElements.Rectangle trailerLoadedByShipperCheckbox = new ceTe.DynamicPDF.PageElements.Rectangle(221F, 705F, 10F, 10F);
                _currentPage.Elements.Add(trailerLoadedByShipperCheckbox);

                // Trailer loaded by Shipper Text
                ceTe.DynamicPDF.PageElements.Label trailerLoadedByShipperCheckboxText = new ceTe.DynamicPDF.PageElements.Label("By Shipper", 235F, 705F, 50F, 10F, ceTe.DynamicPDF.Font.Helvetica, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(trailerLoadedByShipperCheckboxText);

                // Trailer loaded by carrier checkbox
                ceTe.DynamicPDF.PageElements.Rectangle trailerLoadedByCarrierCheckbox = new ceTe.DynamicPDF.PageElements.Rectangle(221F, 725F, 10F, 10F);
                _currentPage.Elements.Add(trailerLoadedByCarrierCheckbox);

                // Trailer loaded by Carrier Text
                ceTe.DynamicPDF.PageElements.Label trailerLoadedByCarrierCheckboxText = new ceTe.DynamicPDF.PageElements.Label("By Carrier", 235F, 725F, 50F, 10F, ceTe.DynamicPDF.Font.Helvetica, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(trailerLoadedByCarrierCheckboxText);


                // Freight Counted Column Title
                ceTe.DynamicPDF.PageElements.Label freightCountedTitle = new ceTe.DynamicPDF.PageElements.Label("FREIGHT COUNTED", 310F, 690F, 90F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(freightCountedTitle);

                // Freight Counted by Shipper Checkbox
                ceTe.DynamicPDF.PageElements.Rectangle freightCountedByShipperCheckbox = new ceTe.DynamicPDF.PageElements.Rectangle(310F, 705F, 10F, 10F);
                _currentPage.Elements.Add(freightCountedByShipperCheckbox);

                // Freight Counted by Shipper Text
                ceTe.DynamicPDF.PageElements.Label freightCountedByShipperCheckboxText = new ceTe.DynamicPDF.PageElements.Label("By Shipper", 324F, 705F, 90F, 10F, ceTe.DynamicPDF.Font.Helvetica, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(freightCountedByShipperCheckboxText);

                // Freight Counted by Shipper by Pallet Checkbox
                ceTe.DynamicPDF.PageElements.Rectangle freightCountedByDriverByPalletCheckbox = new ceTe.DynamicPDF.PageElements.Rectangle(310F, 725F, 10F, 10F);
                _currentPage.Elements.Add(freightCountedByDriverByPalletCheckbox);

                // Freight Counted by Shipper by Pallet Text
                ceTe.DynamicPDF.PageElements.Label freightCountedByDriverByPalletCheckboxText = new ceTe.DynamicPDF.PageElements.Label("By Driver/By Pallet", 324F, 725F, 90F, 10F, ceTe.DynamicPDF.Font.Helvetica, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(freightCountedByDriverByPalletCheckboxText);

                // Freight Counted by Shipper by Pieces Checkbox
                ceTe.DynamicPDF.PageElements.Rectangle freightCountedByDriverByPiecesCheckbox = new ceTe.DynamicPDF.PageElements.Rectangle(310F, 745F, 10F, 10F);
                _currentPage.Elements.Add(freightCountedByDriverByPiecesCheckbox);

                // Freight Counted by Shipper by Pieces Text
                ceTe.DynamicPDF.PageElements.Label freightCountedByDriverByPiecesCheckboxText = new ceTe.DynamicPDF.PageElements.Label("By Driver/By Pieces", 324F, 745F, 90F, 10F, ceTe.DynamicPDF.Font.Helvetica, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(freightCountedByDriverByPiecesCheckboxText);

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Vertical line 3 for the Shipper Signature section
                ceTe.DynamicPDF.PageElements.Line shipperSignatureSectionVertical3DividerLine = new ceTe.DynamicPDF.PageElements.Line(400F, 685F, 400F, 770F, 1, Grayscale.Black, LineStyle.Solid);
                _currentPage.Elements.Add(shipperSignatureSectionVertical3DividerLine);

                // Carrier Signature Title Date
                ceTe.DynamicPDF.PageElements.Label carrierSignatureTitleDateTitle = new ceTe.DynamicPDF.PageElements.Label("CARRIER SIGNATURE / PICKUP DATE", 409F, 690F, 180F, 10F, ceTe.DynamicPDF.Font.HelveticaBold, 8, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(carrierSignatureTitleDateTitle);

                // Carrier Signature Legal Statement
                ceTe.DynamicPDF.PageElements.Label carrierSignatureLegalStatementText = new ceTe.DynamicPDF.PageElements.Label("Carrier acknowledges receipt of packages and required placards, certifies emergency response information was made available and/or carrier has the DOT emergency response guidebook nor equivalent documentation in the vehicle. Property described above is received in good order, except as noted.", 406F, 702F, 182F, 40F, ceTe.DynamicPDF.Font.Helvetica, 7, TextAlign.Left, RgbColor.Black);
                _currentPage.Elements.Add(carrierSignatureLegalStatementText);

                // Final Carrier Signature Line
                ceTe.DynamicPDF.PageElements.Line carrierSignatureLine = new ceTe.DynamicPDF.PageElements.Line(405F, 765F, 585F, 765F, 0.5F, RgbColor.Black, LineStyle.Solid);
                _currentPage.Elements.Add(carrierSignatureLine);

                ////////////////////////////////////////////////////////////////////////////////////
                /// End of Carrier Signature Container
                ////////////////////////////////////////////////////////////////////////////////////

            }

        }

        private void PrintBolDocumentShipmentDetails(int startIndex, int endIndex)
        {

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Shipment Details
            /// Pallets First
            /// Packages Second
            /// jKirkerx on 02/27/2024
            /// 

            // Page Parameters
            float pageStartPointX = 20F;
            float pageStartPointY = 20F;
            float printAreaWidth = 572.5F;


            // We can print 4 objects per page with Pallets
            ///////////////////////////////////////////////////////////////////////////////
            ///            

            // Set the Pallet, Package Container Object Start Point
            // This will represent temp index 0, the first pallet                        
            float objectContainerStartPointXkey = pageStartPointX + 12.5F;
            float objectContainerStartPointXvalue = pageStartPointX + 150F;
            float objectContainerStartPointY = pageStartPointY + 250F;
            int tempStartIndex = 0;

            if (Bol.Pallets != null && this.palletCount > 0)
            {

                for (int i = startIndex; i <= endIndex; i++)
                {

                    if (this.palletCount == 0)
                    {
                        // We are done generating Pallets, next is packages
                        // Recalculate the start and stop indexes, so packages start fresh at the begining
                        startIndex = 0;
                        endIndex = packageCount;
                        this.TotalItems -= this.Bol.Pallets.Count();
                        
                        // Break out of the loop if palletCount is 0
                        break;
                    }

                    PALLETS pallet = this.Bol.Pallets[i];

                    // Truncate the Pallet Description if required
                    if (pallet.PalletDescription != null)
                    {
                        if (pallet.PalletDescription.Length > 25)
                        {
                            pallet.PalletDescription = string.Concat(pallet.PalletDescription.AsSpan(0, 25), "...");
                        }
                    }

                    // Create code to set the new print position for tempIndex 1 to 3
                    switch (tempStartIndex)
                    {
                        case 0:
                            // use the defined above the loop
                            // 1st Pallet Top Left
                            break;

                        case 1:
                            // 2nd Pallet bottom left
                            objectContainerStartPointY += 154F;
                            break;

                        case 2:
                            // 3rd Pallet Top Right
                            objectContainerStartPointY = pageStartPointY + 250F;
                            objectContainerStartPointXkey = (printAreaWidth / 2) + 27F;
                            objectContainerStartPointXvalue = (printAreaWidth / 2) + 160F;
                            break;

                        case 3:
                            // 4th Pallet Bottom Right
                            objectContainerStartPointY += 154F;

                            // Time to start a new page, if we have more Pallets or Package
                            // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                            tempStartIndex = -1;
                            break;

                    }

                    if (_currentPage != null)
                    {

                        #region Pallet
                        // First Pallet - Pallet Description Key
                        ceTe.DynamicPDF.PageElements.Label palletDescriptionKey = new ceTe.DynamicPDF.PageElements.Label("Pallet Name: ", objectContainerStartPointXkey, objectContainerStartPointY, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletDescriptionKey);

                        // First Pallet - Pallet Description Value
                        ceTe.DynamicPDF.PageElements.Label palletDescriptionValue = new ceTe.DynamicPDF.PageElements.Label(pallet.PalletDescription ?? string.Empty, objectContainerStartPointXvalue, objectContainerStartPointY, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletDescriptionValue);

                        // First Pallet - Pallet Total Cartons Key
                        ceTe.DynamicPDF.PageElements.Label palletTotalCartonsKey = new ceTe.DynamicPDF.PageElements.Label("Total Cartons: ", objectContainerStartPointXkey, objectContainerStartPointY + 12F, 100F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalCartonsKey);

                        // First Pallet - Pallet Total Cartons Value
                        ceTe.DynamicPDF.PageElements.Label palletTotalCartonsValue = new ceTe.DynamicPDF.PageElements.Label(pallet.BoxCount.ToString(), objectContainerStartPointXvalue, objectContainerStartPointY + 12F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalCartonsValue);

                        // First Pallet - Pallet Total Units Key
                        ceTe.DynamicPDF.PageElements.Label palletTotalUnitsKey = new ceTe.DynamicPDF.PageElements.Label("Total Units: ", objectContainerStartPointXkey, objectContainerStartPointY + 24F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalUnitsKey);

                        // First Pallet - Pallet Total Units Value
                        ceTe.DynamicPDF.PageElements.Label palletTotalUnitsValue = new ceTe.DynamicPDF.PageElements.Label(pallet.UnitCount.ToString(), objectContainerStartPointXvalue, objectContainerStartPointY + 24F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalUnitsValue);

                        // First Pallet - Pallet Stackable Pallets Key
                        ceTe.DynamicPDF.PageElements.Label palletStackablePalletsKey = new ceTe.DynamicPDF.PageElements.Label("Num. Stackable Pallets: ", objectContainerStartPointXkey, objectContainerStartPointY + 36F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletStackablePalletsKey);

                        // First Pallet - Pallet Stackable Pallets Value
                        ceTe.DynamicPDF.PageElements.Label palletStackablePalletsValue = new ceTe.DynamicPDF.PageElements.Label(pallet.StackablePallet.ToString(), objectContainerStartPointXvalue, objectContainerStartPointY + 36F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletStackablePalletsValue);

                        // First Pallet - Pallet Unstackable Pallets Key
                        ceTe.DynamicPDF.PageElements.Label palletUnstackablePalletsKey = new ceTe.DynamicPDF.PageElements.Label("Num. Unstackable Pallets: ", objectContainerStartPointXkey, objectContainerStartPointY + 48F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletUnstackablePalletsKey);

                        // First Pallet - Pallet Unstackable Pallets Value
                        ceTe.DynamicPDF.PageElements.Label palletUnstackablePalletsValue = new ceTe.DynamicPDF.PageElements.Label(pallet.NonStackablePallet.ToString(), objectContainerStartPointXvalue, objectContainerStartPointY + 48F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletUnstackablePalletsValue);

                        // Draw a line under this section, then start another section
                        ceTe.DynamicPDF.PageElements.Line palletSectionLineOne = new ceTe.DynamicPDF.PageElements.Line(objectContainerStartPointXkey, objectContainerStartPointY + 62F, objectContainerStartPointXkey + 263F, objectContainerStartPointY + 62F, RgbColor.Black);
                        _currentPage.Elements.Add(palletSectionLineOne);

                        // First Pallet - Pallet Total Pallet Weight Key
                        ceTe.DynamicPDF.PageElements.Label palletTotalPalletWeightKey = new ceTe.DynamicPDF.PageElements.Label("Total Pallet Weight: ", objectContainerStartPointXkey, objectContainerStartPointY + 66F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalPalletWeightKey);

                        // First Pallet - Pallet Total Pallet Weight Value
                        ceTe.DynamicPDF.PageElements.Label palletTotalPalletWeightValue = new ceTe.DynamicPDF.PageElements.Label(pallet.Weight.ToString() + ' ' + pallet.WeightType, objectContainerStartPointXvalue, objectContainerStartPointY + 66F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalPalletWeightValue);

                        // First Pallet - Pallet Total Pallet Volume Key
                        ceTe.DynamicPDF.PageElements.Label palletTotalPalletVolumeKey = new ceTe.DynamicPDF.PageElements.Label("Total Pallet Volume: ", objectContainerStartPointXkey, objectContainerStartPointY + 78F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalPalletVolumeKey);

                        // First Pallet - Pallet Total Pallet Volume Value
                        ceTe.DynamicPDF.PageElements.Label palletTotalPalletVolumeValue = new ceTe.DynamicPDF.PageElements.Label(pallet.Volume.ToString() + ' ' + pallet.VolumeType, objectContainerStartPointXvalue, objectContainerStartPointY + 78F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletTotalPalletVolumeValue);

                        // Draw a line under this section, then start another section
                        ceTe.DynamicPDF.PageElements.Line palletSectionLineTwo = new ceTe.DynamicPDF.PageElements.Line(objectContainerStartPointXkey, objectContainerStartPointY + 92F, objectContainerStartPointXkey + 263F, objectContainerStartPointY + 92F, RgbColor.Black);
                        _currentPage.Elements.Add(palletSectionLineTwo);

                        // First Pallet - Pallet Freight Class Key
                        ceTe.DynamicPDF.PageElements.Label palletFreightClassKey = new ceTe.DynamicPDF.PageElements.Label("Freight Class: ", objectContainerStartPointXkey, objectContainerStartPointY + 94F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletFreightClassKey);

                        if (pallet.ClassCode != null)
                        {
                            // First Pallet - Pallet Freight Class Value
                            ceTe.DynamicPDF.PageElements.Label palletFreightClassValue = new ceTe.DynamicPDF.PageElements.Label(Convert.ToString(pallet.ClassCode.CodeNumber) ?? string.Empty, objectContainerStartPointXvalue, objectContainerStartPointY + 94F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletFreightClassValue);
                        }

                        // First Pallet - Shipment Type Key
                        ceTe.DynamicPDF.PageElements.Label palletShipmentTypeKey = new ceTe.DynamicPDF.PageElements.Label("Shipment Type: ", objectContainerStartPointXkey, objectContainerStartPointY + 106F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletShipmentTypeKey);

                        // First Pallet - Shipment Type Value
                        ceTe.DynamicPDF.PageElements.Label palletShipmentTypeValue = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipmentType ?? string.Empty, objectContainerStartPointXvalue, objectContainerStartPointY + 106F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletShipmentTypeValue);

                        // First Pallet - Declared Value Key
                        ceTe.DynamicPDF.PageElements.Label palletDeclaredValueKey = new ceTe.DynamicPDF.PageElements.Label("Declared (insurable) Value: ", objectContainerStartPointXkey, objectContainerStartPointY + 118F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletDeclaredValueKey);

                        // First Pallet - Declared Value Value
                        ceTe.DynamicPDF.PageElements.Label palletDeclaredValueValue = new ceTe.DynamicPDF.PageElements.Label(pallet.EstimatedValue.ToString() + " " + pallet.CurrencyCode, objectContainerStartPointXvalue, objectContainerStartPointY + 118F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                        _currentPage.Elements.Add(palletDeclaredValueValue);
                        #endregion

                        // Increment the temp start index, used to start the packages count
                        // objectContainerStartPointY += 50;
                        tempStartIndex++;


                        // Remove a pallet from the counter, that tracks when to generate packages
                        palletCount--;

                    }

                    // A Pallet Section is 130F Tall Total, Allow 24F below for the next Pallet                    

                }


            }

            ///////////////////////////////////////////////////////////////////////////////
            /// Packages
            /// Don't generate packages until we generate all the pallets

            if (Bol.Packages != null && palletCount == 0 && packageCount > 0)
            {

                if (Bol.Packages.Count > 0)
                {
                    // Recalculate the startIndex and endIndex

                    for (int i = startIndex; i < endIndex; i++)
                    {

                        if (this.packageCount == 0)
                        {
                            // We are done generating Pallets, next is packages
                            // Recalculate the start and stop indexes, so packages start fresh at the begining
                            startIndex = 0;
                            endIndex = 0;                            

                            // Break out of the loop if palletCount is 0
                            break;
                        }

                        PACKAGES package = this.Bol.Packages[i];

                        // Truncate the Pallet Description if required
                        if (package.PackageDescription != null)
                        {
                            if (package.PackageDescription.Length > 25)
                            {
                                package.PackageDescription = string.Concat(package.PackageDescription.AsSpan(0, 25), "...");
                            }
                        }

                        // Create code to set the new print position for tempIndex 1 to 3
                        switch (tempStartIndex)
                        {
                            case 0:
                                // use the defined above the loop
                                // 1st Pallet Top Left
                                break;

                            case 1:
                                // 2nd Pallet bottom left
                                objectContainerStartPointY += 154F;
                                break;

                            case 2:
                                // 3rd Pallet Top Right
                                objectContainerStartPointY = pageStartPointY + 250F;
                                objectContainerStartPointXkey = (printAreaWidth / 2) + 27F;
                                objectContainerStartPointXvalue = (printAreaWidth / 2) + 160F;
                                break;

                            case 3:
                                // 4th Pallet Bottom Right
                                objectContainerStartPointY += 154F;

                                // Time to start a new page, if we have more Pallets or Package
                                // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                                tempStartIndex = -1;
                                break;

                        }

                        if (package != null && _currentPage != null)
                        {
                            String measurementCode = package.MeasurementCode == "English" ? "IN" : "CM";


                            // First Package - Description Key
                            ceTe.DynamicPDF.PageElements.Label PackageDescriptionKey = new ceTe.DynamicPDF.PageElements.Label("Package Name: ", objectContainerStartPointXkey, objectContainerStartPointY, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageDescriptionKey);

                            // First Package - Description Value
                            ceTe.DynamicPDF.PageElements.Label palletDescriptionValue = new ceTe.DynamicPDF.PageElements.Label(package.PackageDescription ?? string.Empty, objectContainerStartPointXvalue, objectContainerStartPointY, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletDescriptionValue);


                            // First Package - Total Units
                            ceTe.DynamicPDF.PageElements.Label PackageTotalUnitsKey = new ceTe.DynamicPDF.PageElements.Label("Total Units: ", objectContainerStartPointXkey, objectContainerStartPointY + 12F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageTotalUnitsKey);

                            // First Package - Total Units Value
                            ceTe.DynamicPDF.PageElements.Label PackageTotalUnitsValue = new ceTe.DynamicPDF.PageElements.Label(package.UnitCount.ToString(), objectContainerStartPointXvalue, objectContainerStartPointY + 12F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageTotalUnitsValue);


                            // First Package - Length Key
                            ceTe.DynamicPDF.PageElements.Label PackageLengthKey = new ceTe.DynamicPDF.PageElements.Label("Length: ", objectContainerStartPointXkey, objectContainerStartPointY + 24F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageLengthKey);

                            // First Package - Length Value
                            ceTe.DynamicPDF.PageElements.Label PackageLengthValue = new ceTe.DynamicPDF.PageElements.Label(package.Length.ToString() + " " + measurementCode, objectContainerStartPointXvalue, objectContainerStartPointY + 24F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageLengthValue);


                            // First Package - Width Key
                            ceTe.DynamicPDF.PageElements.Label PackageWidthKey = new ceTe.DynamicPDF.PageElements.Label("Width: ", objectContainerStartPointXkey, objectContainerStartPointY + 36F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageWidthKey);

                            // First Package - Width Value
                            ceTe.DynamicPDF.PageElements.Label PackageWidthValue = new ceTe.DynamicPDF.PageElements.Label(package.Width.ToString() + " " + measurementCode, objectContainerStartPointXvalue, objectContainerStartPointY + 36F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageWidthValue);


                            // First Package - Height Key
                            ceTe.DynamicPDF.PageElements.Label PackageHeightKey = new ceTe.DynamicPDF.PageElements.Label("Height: ", objectContainerStartPointXkey, objectContainerStartPointY + 48F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageHeightKey);

                            // First Package - Height Value
                            ceTe.DynamicPDF.PageElements.Label PackageHeightValue = new ceTe.DynamicPDF.PageElements.Label(package.Height.ToString() + " " + measurementCode, objectContainerStartPointXvalue, objectContainerStartPointY + 48F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(PackageHeightValue);


                            // Draw a line under this section, then start another section
                            ceTe.DynamicPDF.PageElements.Line packageSectionLineOne = new ceTe.DynamicPDF.PageElements.Line(objectContainerStartPointXkey, objectContainerStartPointY + 62F, objectContainerStartPointXkey + 263F, objectContainerStartPointY + 62F, RgbColor.Black);
                            _currentPage.Elements.Add(packageSectionLineOne);


                            // First Package - Package Total Package Weight Key
                            ceTe.DynamicPDF.PageElements.Label palletTotalPalletWeightKey = new ceTe.DynamicPDF.PageElements.Label("Total Pacakge Weight: ", objectContainerStartPointXkey, objectContainerStartPointY + 66F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletTotalPalletWeightKey);

                            // First Package - Package Total Package Weight Value
                            ceTe.DynamicPDF.PageElements.Label palletTotalPalletWeightValue = new ceTe.DynamicPDF.PageElements.Label(package.Weight.ToString() + ' ' + package.WeightType, objectContainerStartPointXvalue, objectContainerStartPointY + 66F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletTotalPalletWeightValue);


                            // First Package - Package Total Package Volume Key
                            ceTe.DynamicPDF.PageElements.Label palletTotalPalletVolumeKey = new ceTe.DynamicPDF.PageElements.Label("Total Pacakge Volume: ", objectContainerStartPointXkey, objectContainerStartPointY + 78F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletTotalPalletVolumeKey);

                            // First Package - Package Total Package Volume Value
                            ceTe.DynamicPDF.PageElements.Label palletTotalPalletVolumeValue = new ceTe.DynamicPDF.PageElements.Label(package.Volume.ToString() + ' ' + package.VolumeType, objectContainerStartPointXvalue, objectContainerStartPointY + 78F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletTotalPalletVolumeValue);


                            // Draw a line under this section, then start another section
                            ceTe.DynamicPDF.PageElements.Line palletSectionLineTwo = new ceTe.DynamicPDF.PageElements.Line(objectContainerStartPointXkey, objectContainerStartPointY + 92F, objectContainerStartPointXkey + 263F, objectContainerStartPointY + 92F, RgbColor.Black);
                            _currentPage.Elements.Add(palletSectionLineTwo);


                            // First Package - Package Freight Class Key
                            ceTe.DynamicPDF.PageElements.Label palletFreightClassKey = new ceTe.DynamicPDF.PageElements.Label("Freight Class: ", objectContainerStartPointXkey, objectContainerStartPointY + 94F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletFreightClassKey);

                            if (package.ClassCode != null)
                            {
                                // First Package - Package Freight Class Value
                                ceTe.DynamicPDF.PageElements.Label palletFreightClassValue = new ceTe.DynamicPDF.PageElements.Label(Convert.ToString(package.ClassCode.CodeNumber) ?? string.Empty, objectContainerStartPointXvalue, objectContainerStartPointY + 94F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                                _currentPage.Elements.Add(palletFreightClassValue);
                            }

                            // First Package - Shipment Type Key
                            ceTe.DynamicPDF.PageElements.Label palletShipmentTypeKey = new ceTe.DynamicPDF.PageElements.Label("Shipment Type: ", objectContainerStartPointXkey, objectContainerStartPointY + 106F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletShipmentTypeKey);

                            // First Package - Shipment Type Value
                            ceTe.DynamicPDF.PageElements.Label palletShipmentTypeValue = new ceTe.DynamicPDF.PageElements.Label(Bol.ShipmentType ?? string.Empty, objectContainerStartPointXvalue, objectContainerStartPointY + 106F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletShipmentTypeValue);

                            // First Package - Declared Value Key
                            ceTe.DynamicPDF.PageElements.Label palletDeclaredValueKey = new ceTe.DynamicPDF.PageElements.Label("Declared (insurable) Value: ", objectContainerStartPointXkey, objectContainerStartPointY + 118F, 135F, 12F, ceTe.DynamicPDF.Font.HelveticaBold, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletDeclaredValueKey);

                            // First Package - Declared Value Value
                            ceTe.DynamicPDF.PageElements.Label palletDeclaredValueValue = new ceTe.DynamicPDF.PageElements.Label(package.EstimatedValue.ToString() + " " + package.CurrencyCode, objectContainerStartPointXvalue, objectContainerStartPointY + 118F, 135F, 12F, ceTe.DynamicPDF.Font.Helvetica, 10, TextAlign.Left, RgbColor.Black);
                            _currentPage.Elements.Add(palletDeclaredValueValue);


                        }

                        // Increment the temp start index, to trigger end of printing
                        objectContainerStartPointY += 50;
                        tempStartIndex++;

                        // Remove a package from the Packages.Count()
                        packageCount--;

                    }
                }

            }

            // End Of Printing Shipment Details
            // After printing the content for the current page, determine if there are more pages to print
            if (tempStartIndex == 0 || endIndex < this.TotalItems - 1)
            {
                this._hasMorePages = true;
                this.CurrentPage++;         // Move to the next page for the next print event
                PrintNextPage();

            }
            else
            {
                this._hasMorePages = false; // No more pages to print

            }

        }

        private void PrintNextPage()
        {
            // Paginate the Pallets and Packages, deduct 1 because we start with zero or 0
            int startIndex = (this.CurrentPage * this.ItemsPerPage) - this.ItemsPerPage;
            int endIndex = (Math.Min(startIndex + this.ItemsPerPage, this.TotalItems) - 1);

            AddDocumentPage(_document);
            PrintBolDocument();
            PrintBolDocumentShipmentDetails(startIndex, endIndex);
        }

        /// <summary>
        /// Writes the document to the system drive in appData
        /// </summary>
        /// <param name="document"></param>
        private void SaveDocument(Document document)
        {

            // Make sure the folder exists first
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Resources.PathBolDocuments;
            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Check if the file exists first and delete it, then generate a new one
            // The folderPath already hass the trailing slashes
            string documentPath = folderPath + this.DocumentName + ".pdf";
            if (File.Exists(documentPath))
            {
                try
                {
                    File.Delete(documentPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    var fileDeleteResult = MessageBox.Show("This print file is already open in a PDF reader, " + Environment.NewLine + "Please close it and click Ok", "PDF Generator Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (fileDeleteResult == DialogResult.OK)
                    {
                        File.Delete(documentPath);
                    }
                }

            }

            // Write the Document
            if (documentPath != null && document != null)
            {

                try
                {
                    // Write the Document to the Disk Drive
                    document.Draw(documentPath);

                    // Open the Document with the Default PDF Viewer
                    OpenDocument(documentPath);


                }
                catch (Exception ex)
                {

                    // Log the error later with code
                    Console.Write("PDF Write Error: " + ex.ToString());

                }


            }

        }

        private void OpenDocument(string filePath)
        {

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            // Start the default PDF viewer
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            };

            using (Process pdfViewerProcess = new Process { StartInfo = startInfo })
            {
                pdfViewerProcess.Start();
            }

        }


    }
}
