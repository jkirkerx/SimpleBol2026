using System.Drawing.Printing;
using SimpleBol.Models.MongoDb;

namespace SimpleBol.Classes.DirectPrint
{
    public class PrintDefault : PrintDocument
    {
        public BILLOFLADINGS Bol { get; set; } = null!;
        public int CurrentPage { get; set; } 
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        
        // Track Page Coordinates
        private PointF pageStartPoint;
        // Define Counters
        private bool countersDefined = false;
        private int containerCount = 0;
        private int palletCount = 0;
        private int packageCount = 0;

        // Create the fonts we need to print with
        private readonly Font fontTitleBold = new("Arial", 16, FontStyle.Bold);
        private readonly Font fontTitleNormal = new("Arial", 16, FontStyle.Regular);
        private readonly Font fontLargeBold = new("Arial", 14, FontStyle.Bold);
        private readonly Font fontLargeNormal = new("Arial", 14, FontStyle.Regular);
        private readonly Font fontMedium12Bold = new("Arial", 12, FontStyle.Bold);
        private readonly Font fontMedium12Normal = new("Arial", 12);
        private readonly Font fontMedium10Bold = new("Arial", 10, FontStyle.Bold);
        private readonly Font fontMedium10Normal = new("Arial", 10);
        private readonly Font fontSmallBold = new("Arial", 8, FontStyle.Bold);
        private readonly Font fontSmallBoldUnderline = new("Arial", 8, FontStyle.Bold | FontStyle.Underline);
        private readonly Font fontSmallNormal = new("Arial", 8);
        private readonly Font fontSmallItalic = new("Arial", 8, FontStyle.Italic);
        private readonly Font fontTinyBold = new("Arial", 6, FontStyle.Bold);
        private readonly Font fontTinyNormal = new("Arial", 6);

        // Brushes
        private readonly SolidBrush blackBrush = new SolidBrush(Color.Black);
        private readonly SolidBrush whiteBrush = new SolidBrush(Color.White);
        private readonly SolidBrush redBrush = new SolidBrush(Color.Red);
        private readonly SolidBrush blueBrush = new SolidBrush(Color.Blue);

        // Pens
        private readonly Pen blackPen = new Pen(Color.Black, 2);
        private readonly Pen blackThinPen = new Pen(Color.Black, 1);
        private readonly Pen whitePen = new Pen(Color.White, 2);
        private readonly Pen redPen = new Pen(Color.Red, 2);
        private readonly Pen bluePen = new Pen(Color.Blue, 2);

        // Create a PrintPage event handler method
        public void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {            

            // Define the printable area. Safe to always define the printable area over and over
            RectangleF printArea = e.PageSettings.PrintableArea;

            // Check if counters are already defined before defining them again
            // Define on Page one, but not again on more pages, or we lose track of everything
            if (!countersDefined)
            {
                DefineCounters();
            }

            if (e.Graphics != null)
            {

                // Draw the main rectangle around the page with 1/4" margins
                PointF pageStartPoint = new PointF((printArea.Width - printArea.Width + 25), (printArea.Height - printArea.Height + 25));
                e.Graphics.DrawRectangle(blackPen, pageStartPoint.X, pageStartPoint.Y, printArea.Width - 50, printArea.Height - 50);

                // Draw the first line in the rectangle
                e.Graphics.DrawLine(blackPen, pageStartPoint.X, pageStartPoint.Y + 50, printArea.Width - 25, printArea.Height - printArea.Height + 75);

                // Draw the text within the box above
                SizeF bolTextSize = e.Graphics.MeasureString("BILL OF LADING", fontTitleBold);
                float bolTextX = (printArea.Width - 25F - bolTextSize.Width - 15.5F);
                float bolTextY = (pageStartPoint.Y + bolTextSize.Height) / 2;
                e.Graphics.DrawString("BILL OF LADDING", fontTitleBold, blackBrush, bolTextX, bolTextY);

                // Generate BOL Print Data
                if (Bol != null)
                {

                    // Draw the Bol Create Date inside the BOL textbox
                    SizeF bolDateTextSize = e.Graphics.MeasureString(Bol.CreatedOnUtc.ToLocalTime().ToString("MM/dd/yyyy"), fontMedium10Normal);
                    float bolDateTextX = printArea.Width - printArea.Width + 25 + 12.5F;
                    float bolDateTextY = pageStartPoint.Y + bolTextY + 4F;
                    e.Graphics.DrawString("Date: " + Bol.CreatedOnUtc.ToLocalTime().ToString("MM/dd/yyyy"), fontMedium10Bold, blackBrush, bolDateTextX, bolDateTextY);

                    // Draw the Bol Page Number
                    SizeF bolPageNumberTextSize = e.Graphics.MeasureString("Page: " + CurrentPage.ToString() + " of " + TotalPages, fontMedium10Normal);
                    float bolPageNumberTextX = printArea.Width - 25F - bolPageNumberTextSize.Width - 10.5F;
                    float bolPageNumberTextY = pageStartPoint.Y + bolTextY + 4F;
                    e.Graphics.DrawString("Page: " + CurrentPage.ToString() + " of " + TotalPages, fontMedium10Bold, blackBrush, bolPageNumberTextX, bolPageNumberTextY);


                    if (Bol.ShipFromVendor != null)
                    {
                        if (Bol.ShipFromVendor.CompanyName != null)
                        {
                            // Draw the Company Name within the BOL textbox
                            SizeF bolCompanyNameTextSize = e.Graphics.MeasureString(Bol.ShipFromVendor.CompanyName, fontTitleBold);
                            string bolCompanyNameText = Bol.ShipFromVendor.CompanyName;
                            float bolCompanyNameTextX = pageStartPoint.X + 10F;
                            float bolCompanyNameTextY = (pageStartPoint.Y + bolCompanyNameTextSize.Height) / 2F;
                            e.Graphics.DrawString(Bol.ShipFromVendor.CompanyName, fontTitleBold, blackBrush, bolCompanyNameTextX, bolCompanyNameTextY);
                        }

                        //////////////////////////////////////////////////////////////////////////////
                        // Start of Ship From Address

                        // Create the Ship From Header, black rectangle with white text
                        PointF shipFromStartPoint = new PointF(pageStartPoint.X, pageStartPoint.Y + 50F);
                        SizeF shipFromTextSize = e.Graphics.MeasureString("SHIP FROM", fontSmallBold);
                        float shipFromRectangleWidth = (printArea.Width - 50F) / 2F;
                        float shipFromRectangleHeight = 12.5F;

                        e.Graphics.DrawRectangle(blackPen, shipFromStartPoint.X, shipFromStartPoint.Y, shipFromRectangleWidth, shipFromRectangleHeight);
                        e.Graphics.FillRectangle(blackBrush, shipFromStartPoint.X, shipFromStartPoint.Y, shipFromRectangleWidth, shipFromRectangleHeight);

                        // Draw the text Ship From, with help from ChatGPT
                        float shipFromTextX = shipFromStartPoint.X + 12.5F;
                        float shipFromTextY = shipFromStartPoint.Y + (shipFromRectangleHeight - shipFromTextSize.Height) / 2F;
                        e.Graphics.DrawString("SHIP FROM", fontSmallBold, whiteBrush, shipFromTextX, shipFromTextY);

                        // We are now 50 units below the top of the rectangle                        

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
                                SizeF lineSize = e.Graphics.MeasureString(line, fontMedium10Normal);
                                float lineX = shipFromAddressStartX + 12.5F; // Indent the address
                                float lineY = shipFromAddressStartY;

                                e.Graphics.DrawString(line, fontMedium10Normal, blackBrush, lineX, lineY);

                                // Increment the Y position for the next line
                                shipFromAddressStartY += lineSize.Height + 1.5F; // Add a small vertical spacing
                            }

                        }

                        /// End of Ship From Address
                        //////////////////////////////////////////////////////////////////////////////

                        //////////////////////////////////////////////////////////////////////////////
                        /// Start of Ship To Address

                        // Create the Ship From Header, black rectangle with white text
                        PointF shipToStartPoint = new PointF(pageStartPoint.X, pageStartPoint.Y + 190F);
                        SizeF shipToTextSize = e.Graphics.MeasureString("SHIP TO", fontSmallBold);
                        float shipToRectangleWidth = (printArea.Width - 50F) / 2F;
                        float shipToRectangleHeight = 12.5F;

                        e.Graphics.DrawRectangle(blackPen, shipToStartPoint.X, shipToStartPoint.Y, shipToRectangleWidth, shipToRectangleHeight);
                        e.Graphics.FillRectangle(blackBrush, shipToStartPoint.X, shipToStartPoint.Y, shipToRectangleWidth, shipToRectangleHeight);

                        // Draw the text Ship From, with help from ChatGPT
                        float shipToTextX = shipToStartPoint.X + 12.5F;
                        float shipToTextY = shipToStartPoint.Y + (shipToRectangleHeight - shipToTextSize.Height) / 2F;
                        e.Graphics.DrawString("SHIP TO", fontSmallBold, whiteBrush, shipToTextX, shipToTextY);

                        if (Bol.ShipToCustomer != null)
                        {
                            // Write the ShipFromVendor Information //

                            if (Bol.ShipToCustomer != null && Bol.ShipToLocation != null)
                            {
                                // Define address information
                                List<string> shipToAddressLines = new List<string>();

                                if (!string.IsNullOrEmpty(Bol.ShipToCustomer.CompanyName))
                                    shipToAddressLines.Add(Bol.ShipToCustomer.CompanyName);

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.Address1))
                                    shipToAddressLines.Add(Bol.ShipToLocation.Address1);

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.Address2))
                                    shipToAddressLines.Add(Bol.ShipToLocation.Address2);

                                // Build a single line here, City, Region, Postal Code
                                string shipToCityRegionPostal = "";

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.City))
                                    shipToCityRegionPostal += " " + Bol.ShipToLocation.City;

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.RegionName))
                                    shipToCityRegionPostal += " " + Bol.ShipToLocation.RegionAbbr;

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.PostalCode))
                                    shipToCityRegionPostal += " " + Bol.ShipToLocation.PostalCode;

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.CountryName))
                                    shipToCityRegionPostal += " " + Bol.ShipToLocation.CountryAbbr;

                                shipToAddressLines.Add(shipToCityRegionPostal);
                                shipToAddressLines.Add("\r\n");

                                if (!string.IsNullOrEmpty(Bol.ShipToLocation.ContactName) && !string.IsNullOrEmpty(Bol.ShipToLocation.ContactPhone))
                                    shipToAddressLines.Add("Contact: " + Bol.ShipToLocation.ContactName + ": " + Bol.ShipToLocation.ContactPhone);

                                // Calculate position for drawing address lines
                                float shipToAddressStartX = shipToStartPoint.X;
                                float shipToAddressStartY = shipToStartPoint.Y + shipToRectangleHeight + 10F; // Adjust vertical position

                                foreach (string line in shipToAddressLines)
                                {
                                    SizeF lineSize = e.Graphics.MeasureString(line, fontMedium10Normal);
                                    float lineX = shipToAddressStartX + 12.5F; // Indent the address
                                    float lineY = shipToAddressStartY;

                                    e.Graphics.DrawString(line, fontMedium10Normal, blackBrush, lineX, lineY);

                                    // Increment the Y position for the next line
                                    shipToAddressStartY += lineSize.Height + 1.5F; // Add a small vertical spacing
                                }
                            }

                        }

                        /// End of Ship To Address
                        //////////////////////////////////////////////////////////////////////////////

                        // Draw the top middle line that divides the leftside ShipTo and ShipFrom, from the right side of BOL information
                        PointF topCenterVerticalLineStartPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 50F);
                        PointF topCenterVerticalLineStopPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 325F);
                        e.Graphics.DrawLine(blackPen, topCenterVerticalLineStartPoint, topCenterVerticalLineStopPoint);


                        // We are now below the top of the main rectangle, working the right side column

                        //////////////////////////////////////////////////////////////////////////////
                        /// Start of BOL Information

                        // Bol Number
                        SizeF bolNumberLabelSize = e.Graphics.MeasureString("Bill of Ladding Number: ", fontMedium10Bold);
                        float bolNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                        float bolNumberLabelY = pageStartPoint.Y + 58F;
                        e.Graphics.DrawString("Bill of Ladding Number: ", fontMedium10Bold, blackBrush, bolNumberLabelX, bolNumberLabelY);

                        if (Bol.BolNumber != null)
                        {
                            SizeF bolNumberTextSize = e.Graphics.MeasureString(Bol.BolNumber, fontMedium12Bold);
                            float bolNumberTextX = bolNumberLabelX + bolNumberLabelSize.Width + 3.125F;
                            float bolNumberTextY = pageStartPoint.Y + 56F;
                            e.Graphics.DrawString(Bol.BolNumber, fontMedium12Normal, blackBrush, bolNumberTextX, bolNumberTextY);
                        }

                        // Ship From PO Number
                        if (Bol.ShipFromVendor != null)
                        {
                            if (Bol.ShipFromVendor.CompanyName != null)
                            {
                                SizeF bolPoNumberLabelSize = e.Graphics.MeasureString(Bol.ShipFromVendor.CompanyName + " PO Id: ", fontMedium10Bold);
                                float bolPoNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                                float bolPoNumberLabelY = pageStartPoint.Y + 58F + bolPoNumberLabelSize.Height + 1.5F;
                                e.Graphics.DrawString(Bol.ShipFromVendor.CompanyName + " PO Id: ", fontMedium10Bold, blackBrush, bolPoNumberLabelX, bolPoNumberLabelY);

                                if (Bol.ShipperReferenceNumber != null)
                                {
                                    SizeF bolPoNumberTextSize = e.Graphics.MeasureString(Bol.ShipperReferenceNumber, fontMedium12Bold);
                                    float bolPoNumberTextX = bolPoNumberLabelX + bolPoNumberLabelSize.Width + 3.125F;
                                    float bolPoNumberTextY = pageStartPoint.Y + 56F + bolPoNumberLabelSize.Height + 1.5F;
                                    e.Graphics.DrawString(Bol.ShipperReferenceNumber, fontMedium12Normal, blackBrush, bolPoNumberTextX, bolPoNumberTextY);
                                }

                            }
                        }

                        // Ship From Order Number
                        if (Bol.ShipFromVendor != null)
                        {
                            if (Bol.ShipFromVendor.CompanyName != null)
                            {
                                SizeF bolOrderNumberLabelSize = e.Graphics.MeasureString(Bol.ShipFromVendor.CompanyName + " Order #: ", fontMedium10Bold);
                                float bolOrderNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                                float bolOrderNumberLabelY = pageStartPoint.Y + 78F + bolOrderNumberLabelSize.Height + 1.5F;
                                e.Graphics.DrawString(Bol.ShipFromVendor.CompanyName + " Order #: ", fontMedium10Bold, blackBrush, bolOrderNumberLabelX, bolOrderNumberLabelY);

                                if (Bol.OrderNumber != null)
                                {
                                    SizeF bolOrderNumberTextSize = e.Graphics.MeasureString(Bol.OrderNumber, fontMedium12Normal);
                                    float bolOrderNumberTextX = bolOrderNumberLabelX + bolOrderNumberLabelSize.Width + 3.125F;
                                    float bolOrderNumberTextY = pageStartPoint.Y + 76F + bolOrderNumberLabelSize.Height + 1.5F;
                                    e.Graphics.DrawString(Bol.OrderNumber, fontMedium12Normal, blackBrush, bolOrderNumberTextX, bolOrderNumberTextY);
                                }
                            }
                        }

                        // Draw a line to finish the top BOL Reference Container, on the right side                        
                        PointF bolReferenceContainerStartPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 122F);
                        PointF bolReferenceContainerStopPoint = new PointF(printArea.Width - 50F / 2F, pageStartPoint.Y + 122F);
                        e.Graphics.DrawLine(blackPen, bolReferenceContainerStartPoint, bolReferenceContainerStopPoint);

                        // End of BOL Information
                        //////////////////////////////////////////////////////////////////////////////


                        //////////////////////////////////////////////////////////////////////////////
                        // Start a new Container for the Selected Shipper or Carrier

                        if (Bol.Shipper != null)
                        {
                            SizeF carrierNameLabelSize = e.Graphics.MeasureString("Carrier Name: ", fontMedium10Bold);
                            float carrierNameLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                            float carrierNameLabelY = pageStartPoint.Y + 109F + carrierNameLabelSize.Height + 1.5F;
                            e.Graphics.DrawString("Carrier Name: ", fontMedium10Bold, blackBrush, carrierNameLabelX, carrierNameLabelY);

                            if (Bol.Shipper.CompanyName != null)
                            {
                                SizeF carrierNameTextSize = e.Graphics.MeasureString(Bol.Shipper.CompanyName, fontMedium12Normal);
                                float carrierNameTextX = carrierNameLabelX + carrierNameLabelSize.Width + 3.125F;
                                float carrierNameTextY = pageStartPoint.Y + 107F + carrierNameLabelSize.Height + 1.5F;
                                e.Graphics.DrawString(Bol.Shipper.CompanyName, fontMedium12Normal, blackBrush, carrierNameTextX, carrierNameTextY);
                            }

                        }

                        // Shipper Quote Number
                        if (Bol.ShipperQuoteNumber != null)
                        {
                            SizeF carrierQuoteNumberLabelSize = e.Graphics.MeasureString("Quote Number: ", fontMedium10Bold);
                            float carrierQuoteNumberLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                            float carrierQuoteNumberLabelY = pageStartPoint.Y + 128F + carrierQuoteNumberLabelSize.Height + 1.5F;
                            e.Graphics.DrawString("Quote Number: ", fontMedium10Bold, blackBrush, carrierQuoteNumberLabelX, carrierQuoteNumberLabelY);

                            SizeF carrierQuoteNumberTextSize = e.Graphics.MeasureString(Bol.ShipperQuoteNumber, fontMedium12Normal);
                            float carrierQuoteNumberTextX = carrierQuoteNumberLabelX + carrierQuoteNumberLabelSize.Width + 3.125F;
                            float carrierQuoteNumberTextY = pageStartPoint.Y + 123F + carrierQuoteNumberTextSize.Height + 1.5F;
                            e.Graphics.DrawString(Bol.ShipperQuoteNumber, fontMedium12Normal, blackBrush, carrierQuoteNumberTextX, carrierQuoteNumberTextY);
                        }

                        // Shipper Quote Price                        
                        SizeF carrierQuotePriceLabelSize = e.Graphics.MeasureString("Quoted Price: ", fontMedium10Bold);
                        float carrierQuotePriceLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                        float carrierQuotePriceLabelY = pageStartPoint.Y + 147F + carrierQuotePriceLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("Quoted Price: ", fontMedium10Bold, blackBrush, carrierQuotePriceLabelX, carrierQuotePriceLabelY);

                        SizeF carrierQuotePriceTextSize = e.Graphics.MeasureString(Bol.ShipperQuotePrice.ToString("c"), fontMedium10Normal);
                        float carrierQuotePriceTextX = carrierQuotePriceLabelX + carrierQuotePriceLabelSize.Width + 3.125F;
                        float carrierQuotePriceTextY = pageStartPoint.Y + 145F + carrierQuotePriceTextSize.Height + 1.5F;
                        e.Graphics.DrawString(Bol.ShipperQuotePrice.ToString("c"), fontMedium12Normal, blackBrush, carrierQuotePriceTextX, carrierQuotePriceTextY);

                        // End of Carrier Selected
                        //////////////////////////////////////////////////////////////////////////////

                        //////////////////////////////////////////////////////////////////////////////
                        // Third Party Freight Charges
                        // Create the 3rd Part Frieght Charge Header, black rectangle with white text
                        // This line matches Ship To at 190F Y Axis

                        PointF freightChargeStartPoint = new PointF(topCenterVerticalLineStartPoint.X, pageStartPoint.Y + 190F);
                        SizeF freightChargeTextSize = e.Graphics.MeasureString("THIRD PARTY/FREIGHT CHARGES BILL TO", fontSmallBold);
                        float freightChargeRectangleWidth = (printArea.Width - 50F) / 2F;
                        float freightChargeRectangleHeight = 12.5F;

                        e.Graphics.DrawRectangle(blackPen, freightChargeStartPoint.X, freightChargeStartPoint.Y, freightChargeRectangleWidth, freightChargeRectangleHeight);
                        e.Graphics.FillRectangle(blackBrush, freightChargeStartPoint.X, freightChargeStartPoint.Y, freightChargeRectangleWidth, freightChargeRectangleHeight);

                        // Draw the text Ship From, with help from ChatGPT
                        float freightChargeTextX = freightChargeStartPoint.X + 12.5F;
                        float freightChargeTextY = freightChargeStartPoint.Y + (freightChargeRectangleHeight - freightChargeTextSize.Height) / 2F;
                        e.Graphics.DrawString("THIRD PARTY/FREIGHT CHARGES BILL TO", fontSmallBold, whiteBrush, freightChargeTextX, freightChargeTextY);

                        // Third Party Billing Information
                        if (Bol.BillToId != null)
                        {
                            if (Bol.BillToAccount != null)
                            {
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
                                    SizeF lineSize = e.Graphics.MeasureString(line, fontMedium10Normal);
                                    float lineX = billTo3rdPartyAddressStartX + 12.5F; // Indent the address
                                    float lineY = billTo3rdPartyAddressStartY;

                                    e.Graphics.DrawString(line, fontMedium10Normal, blackBrush, lineX, lineY);

                                    // Increment the Y position for the next line
                                    billTo3rdPartyAddressStartY += lineSize.Height + 1F; // Add a small vertical spacing
                                }

                            }

                        }

                        // Draw a line to finish the center of the Bill To Account Container, on the right side                        
                        PointF freightChargeTermsContainerStartPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 270F);
                        PointF freightChargeTermsContainerStopPoint = new PointF(printArea.Width - 50F / 2F, pageStartPoint.Y + 270F);
                        e.Graphics.DrawLine(blackPen, freightChargeTermsContainerStartPoint, freightChargeTermsContainerStopPoint);

                        // Freight Charge Terms Label                      
                        SizeF freightChargeTermsLabelSize = e.Graphics.MeasureString("Freight charges are prepaid unless marked otherwise", fontSmallBold);
                        float freightChargeTermsLabelX = topCenterVerticalLineStartPoint.X + 12.5F;
                        float freightChargeTermsLabelY = pageStartPoint.Y + 260F + freightChargeTermsLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("Freight charges are prepaid unless marked otherwise", fontSmallBold, blackBrush, freightChargeTermsLabelX, freightChargeTermsLabelY);

                        if (Bol.FreightPrePaid == true)
                        {

                            // Freight Charge Terms Method                      
                            SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ______X______   Collect ___________   3rd Party _________", fontSmallBold);
                            float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5F;
                            float freightChargeTermsTextY = pageStartPoint.Y + 290F + freightChargeTermsLabelSize.Height + 1.5F;
                            e.Graphics.DrawString("Prepaid _____X_____   Collect ___________   3rd Party _________", fontSmallBold, blackBrush, freightChargeTermsTextX, freightChargeTermsTextY);

                        }
                        else if (Bol.CodAmount > 0.00M)
                        {

                            // Freight Charge Terms Method                      
                            SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ___________   Collect _____X_____   3rd Party _________", fontSmallBold);
                            float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5F;
                            float freightChargeTermsTextY = pageStartPoint.Y + 290F + freightChargeTermsLabelSize.Height + 1.5F;
                            e.Graphics.DrawString("Prepaid ___________   Collect _____X_____   3rd Party _________", fontSmallBold, redBrush, freightChargeTermsTextX, freightChargeTermsTextY);

                        }
                        else if (Bol.BillToAccount != null)
                        {
                            // Freight Charge Terms Method                      
                            SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ___________   Collect ___________   3rd Party ____X____", fontSmallBold);
                            float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5F;
                            float freightChargeTermsTextY = pageStartPoint.Y + 290F + freightChargeTermsLabelSize.Height + 1.5F;
                            e.Graphics.DrawString("Prepaid ___________   Collect _____________   3rd Party ____X____", fontSmallBold, blackBrush, freightChargeTermsTextX, freightChargeTermsTextY);

                        }
                        else
                        {
                            // Freight Charge Terms Method                      
                            SizeF freightChargeTermsTextSize = e.Graphics.MeasureString("Prepaid ___________   Collect ___________   3rd Party _________", fontSmallBold);
                            float freightChargeTermsTextX = topCenterVerticalLineStartPoint.X + 12.5F;
                            float freightChargeTermsTextY = pageStartPoint.Y + 290F + freightChargeTermsLabelSize.Height + 1.5F;
                            e.Graphics.DrawString("Prepaid _________   Collect ___________   3rd Party ___________", fontSmallBold, blackBrush, freightChargeTermsTextX, freightChargeTermsTextY);

                        }


                        // End of 3rd Party Billing
                        //////////////////////////////////////////////////////////////////////////////

                        //////////////////////////////////////////////////////////////////////////////
                        // We are now 325 units below the top of the rectangle, draw a horizontal line
                        //////////////////////////////////////////////////////////////////////////////

                        // Packages, Pallets, Containers
                        // Create the BOL Data Header, black rectangle with white text
                        // This line matches Ship To at 325F Y Axis
                        PointF shipmentDetailsStartPoint = new PointF(pageStartPoint.X, pageStartPoint.Y + 325F);
                        SizeF shipmentDetailsTextSize = e.Graphics.MeasureString("SHIPMENT DETAILS", fontSmallBold);
                        float shipmentDetailsRectangleWidth = (printArea.Width - 50F);
                        float shipmentDetailsRectangleHeight = 12.5F;

                        e.Graphics.DrawRectangle(blackPen, shipmentDetailsStartPoint.X, shipmentDetailsStartPoint.Y, shipmentDetailsRectangleWidth, shipmentDetailsRectangleHeight);
                        e.Graphics.FillRectangle(blackBrush, shipmentDetailsStartPoint.X, shipmentDetailsStartPoint.Y, shipmentDetailsRectangleWidth, shipmentDetailsRectangleHeight);

                        // Draw the text Shipment Details Text, I got this
                        float shipmentDetailsTextX = shipmentDetailsStartPoint.X + 12.5F;
                        float shipmentDetailsTextY = shipmentDetailsStartPoint.Y + (shipmentDetailsRectangleHeight - shipmentDetailsTextSize.Height) / 2F;
                        e.Graphics.DrawString("SHIPMENT DETAILS", fontSmallBold, whiteBrush, shipmentDetailsTextX, shipmentDetailsTextY);

                        //////////////////////////////////////////////////////////////////////////////
                        /// Shipment Details - Pallet, Package Details
                        //////////////////////////////////////////////////////////////////////////////

                        // Draw a line to finish the center of the Shipping Details Container                        
                        PointF endOfShipmentDetailsContainerStartPoint = new PointF(printArea.Width - printArea.Width + 25F, pageStartPoint.Y + 750F);
                        PointF endOfShipmentDetailsContainerStopPoint = new PointF(printArea.Width - 25, pageStartPoint.Y + 750F);
                        e.Graphics.DrawLine(blackPen, endOfShipmentDetailsContainerStartPoint, endOfShipmentDetailsContainerStopPoint);

                        /// Moved the Pallet, Package generator to a seperate function, which I will call at the end of this page generation or rendering
                        /// 03/06/2024 jkirkerx
                        

                        /// End of Shipment Details
                        //////////////////////////////////////////////////////////////////////////////

                        //////////////////////////////////////////////////////////////////////////////
                        /// Shipment Value declaration and COD Payment Terms
                        //////////////////////////////////////////////////////////////////////////////

                        // Shipment Value Declaration Text String
                        SizeF shipValueDelarationTextSize = e.Graphics.MeasureString("Where the rate is dependent on value, shippers are required to state specifically in writing the\r\nagreedordeclared value of the property as follows:\r\nThe agreed or declared value of the property is specifically stated by the shipper to be not\r\nexceeding", fontTinyNormal);
                        float shipValueDelarationTextX = pageStartPoint.X + 6.125F;
                        float shipValueDelarationTextY = pageStartPoint.Y + 715F + shipValueDelarationTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Where the rate is dependent on value, shippers are required to state specifically in writing the\r\nagreed or declared value of the property as follows:\r\nThe agreed or declared value of the property is specifically stated by the shipper to be not\r\nexceeding", fontTinyNormal, blackBrush, shipValueDelarationTextX, shipValueDelarationTextY);

                        // Draw a vertical line to create two containers, Limited Liability and Shipper Signature
                        PointF shipValueDelarationVerticalLineContainerStartPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 750F);
                        PointF shipValueDelarationVerticalLineContainerStopPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 815F);
                        e.Graphics.DrawLine(blackPen, shipValueDelarationVerticalLineContainerStartPoint, shipValueDelarationVerticalLineContainerStopPoint);

                        // Draw an X if the shipment is COD
                        if (Bol.COD == true && Bol.CodAmount > 0.00M)
                        {
                            // Write the COD Payment Amount
                            SizeF CodAmountDeclarationTextSize = e.Graphics.MeasureString("COD AMOUNT: " + Bol.CodAmount.ToString("c"), fontMedium10Bold);
                            float CodAmountDeclarationTextX = (printArea.Width / 2F) + 6.125F;
                            float CodAmountDeclarationTextY = pageStartPoint.Y + 740F + CodAmountDeclarationTextSize.Height + 1.5F;
                            e.Graphics.DrawString("COD AMOUNT: " + Bol.CodAmount.ToString("c"), fontMedium10Bold, redBrush, CodAmountDeclarationTextX, CodAmountDeclarationTextY);
                        }
                        else
                        {
                            // Write the COD Payment Amount
                            SizeF CodAmountDeclarationTextSize = e.Graphics.MeasureString("COD AMOUNT: $__________________________________", fontMedium10Bold);
                            float CodAmountDeclarationTextX = (printArea.Width / 2F) + 6.125F;
                            float CodAmountDeclarationTextY = pageStartPoint.Y + 740F + CodAmountDeclarationTextSize.Height + 1.5F;
                            e.Graphics.DrawString("COD AMOUNT: $__________________________________", fontMedium10Bold, blackBrush, CodAmountDeclarationTextX, CodAmountDeclarationTextY);
                        }


                        // Write the COD Fee Terms Label
                        SizeF CodAmountFeeTermsTextSize = e.Graphics.MeasureString("Fee Terms:", fontMedium10Bold);
                        float CodAmountFeeTermsTextX = (printArea.Width / 2F) + 6.125F;
                        float CodAmountFeeTermsTextY = pageStartPoint.Y + 760F + CodAmountFeeTermsTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Fee Terms:", fontMedium10Bold, blackBrush, CodAmountFeeTermsTextX, CodAmountFeeTermsTextY);

                        // Write the COD Fee Term Choice Collect
                        SizeF CodAmountFeeTermsCollectTextSize = e.Graphics.MeasureString("Collect:", fontMedium10Bold);
                        float CodAmountFeeTermsCollectTextX = (printArea.Width / 2F) + CodAmountFeeTermsTextSize.Width + 31F;
                        float CodAmountFeeTermsCollectTextY = pageStartPoint.Y + 760F + CodAmountFeeTermsCollectTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Collect", fontMedium10Bold, blackBrush, CodAmountFeeTermsCollectTextX, CodAmountFeeTermsCollectTextY);

                        // Draw the Fee Terms COD Checkbox
                        PointF CodAmountFeeTermsCollectCheckboxStartPoint = new PointF(CodAmountFeeTermsCollectTextX + CodAmountFeeTermsCollectTextSize.Width + 6.125F, pageStartPoint.Y + 760F + CodAmountFeeTermsCollectTextSize.Height + 2.5F);
                        e.Graphics.DrawRectangle(blackPen, CodAmountFeeTermsCollectCheckboxStartPoint.X, CodAmountFeeTermsCollectCheckboxStartPoint.Y, 12, 12);

                        // Draw an X if the shipment is COD
                        if (Bol.COD == true && Bol.CodAmount > 0.00M)
                        {
                            SizeF CodAmountFeeTermsCollectXTextSize = e.Graphics.MeasureString("X", fontMedium10Bold);
                            float CodAmountFeeTermsCollectXTextX = CodAmountFeeTermsCollectTextX + CodAmountFeeTermsCollectTextSize.Width + 6F;
                            float CodAmountFeeTermsCollectXTextY = pageStartPoint.Y + 760F + CodAmountFeeTermsCollectXTextSize.Height + 1F;
                            e.Graphics.DrawString("X", fontMedium10Bold, redBrush, CodAmountFeeTermsCollectXTextX, CodAmountFeeTermsCollectXTextY);
                        }

                        // Write the COD Fee Term Choice Prepaid
                        SizeF CodAmountFeeTermsPrepaidTextSize = e.Graphics.MeasureString("Prepaid", fontMedium10Bold);
                        float CodAmountFeeTermsPrepaidTextX = (printArea.Width / 2F) + CodAmountFeeTermsTextSize.Width + 120F;
                        float CodAmountFeeTermsPrepaidTextY = pageStartPoint.Y + 760F + CodAmountFeeTermsPrepaidTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Prepaid", fontMedium10Bold, blackBrush, CodAmountFeeTermsPrepaidTextX, CodAmountFeeTermsPrepaidTextY);

                        // Draw the Fee Terms Freight PrePaid Checkbox
                        PointF CodAmountFeeTermsPrepaidCheckboxStartPoint = new PointF(CodAmountFeeTermsPrepaidTextX + CodAmountFeeTermsPrepaidTextSize.Width + 6.125F, pageStartPoint.Y + 760F + CodAmountFeeTermsCollectTextSize.Height + 2.5F);
                        e.Graphics.DrawRectangle(blackPen, CodAmountFeeTermsPrepaidCheckboxStartPoint.X, CodAmountFeeTermsPrepaidCheckboxStartPoint.Y, 12, 12);

                        if (Bol.FreightPrePaid)
                        {
                            // Draw an X indicating the Shipment is Freight PrePaid
                            SizeF CodAmountFeeTermsPrepaidXTextSize = e.Graphics.MeasureString("X", fontMedium10Bold);
                            float CodAmountFeeTermsPrepaidXTextX = CodAmountFeeTermsPrepaidTextX + CodAmountFeeTermsPrepaidTextSize.Width + 6.125F;
                            float CodAmountFeeTermsPrepaidXTextY = pageStartPoint.Y + 760F + CodAmountFeeTermsPrepaidXTextSize.Height + 1.5F;
                            e.Graphics.DrawString("X", fontMedium10Bold, blackBrush, CodAmountFeeTermsPrepaidXTextX, CodAmountFeeTermsPrepaidXTextY);
                        }

                        // Write the Customer Check Acceptable
                        SizeF CustomerCheckAcceptableTextSize = e.Graphics.MeasureString("Customer check acceptable:", fontSmallNormal);
                        float CustomerCheckAcceptableTextX = (printArea.Width / 2F) + 6.125F;
                        float CustomerCheckAcceptableTextY = pageStartPoint.Y + 785F + CustomerCheckAcceptableTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Customer check acceptable:", fontSmallNormal, blackBrush, CustomerCheckAcceptableTextX, CustomerCheckAcceptableTextY);

                        // Draw the Customer Check Acceptable Checkbox
                        PointF CustomerCheckAcceptableCheckboxStartPoint = new PointF(CustomerCheckAcceptableTextX + CustomerCheckAcceptableTextSize.Width + 6.125F, CustomerCheckAcceptableTextY + 2F);
                        e.Graphics.DrawRectangle(blackPen, CustomerCheckAcceptableCheckboxStartPoint.X, CustomerCheckAcceptableCheckboxStartPoint.Y, 8, 8);


                        //////////////////////////////////////////////////////////////////////////////
                        /// Shipment Liability Limitation
                        //////////////////////////////////////////////////////////////////////////////

                        // Draw a line to start the shipment liability limitation and Shipper Signature                       
                        PointF shipmentLiabilityContainerTitleStartPoint = new PointF(printArea.Width - printArea.Width + 25F, pageStartPoint.Y + 816F);
                        PointF shipmentLiabilityContainerTitleStopPoint = new PointF(printArea.Width - 25, pageStartPoint.Y + 816F);
                        e.Graphics.DrawLine(blackPen, shipmentLiabilityContainerTitleStartPoint, shipmentLiabilityContainerTitleStopPoint);

                        // Liability Text String
                        SizeF shipperSignatureLiabilityClauseTextSize = e.Graphics.MeasureString("Liability Limitation for loss or damage in this shipment may be applicable. See 49 U.S.C. 14706(c)(1)(A) and (B).", fontMedium10Bold);
                        float shipperSignatureLiabilityClauseTextX = pageStartPoint.X + 6.125F;
                        float shipperSignatureLiabilityClauseTextY = pageStartPoint.Y + 803F + shipperSignatureLiabilityClauseTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Liability Limitation for loss or damage in this shipment may be applicable. See 49 U.S.C. 14706(c)(1)(A) and (B).", fontMedium10Bold, redBrush, shipperSignatureLiabilityClauseTextX, shipperSignatureLiabilityClauseTextY);

                        // Draw a line to start the shipment liability limitation and Shipper Signature                       
                        PointF shipmentLiabilityContainerStartPoint = new PointF(printArea.Width - printArea.Width + 25F, pageStartPoint.Y + 844F);
                        PointF shipmentLiabilityContainerStopPoint = new PointF(printArea.Width - 25, pageStartPoint.Y + 844F);
                        e.Graphics.DrawLine(blackPen, shipmentLiabilityContainerStartPoint, shipmentLiabilityContainerStopPoint);

                        // Write the legal text for limited liability                        
                        SizeF legalLiabilityTextSize = e.Graphics.MeasureString("RECEIVED, subject to individually determined rates or contracts that have been agreed upon in\r\n writing between the carrier and shipper, if applicable, otherwise to the rates, classifications\r\nand rules that have been established by the carrier and are available to the shipper, on request,\r\nand to all applicable state and federal regulations.", fontTinyNormal);
                        float legalLiabilityTextX = pageStartPoint.X + 6.125F;
                        float legalLiabilityTextY = pageStartPoint.Y + 808F + legalLiabilityTextSize.Height + 1.5F;
                        e.Graphics.DrawString("RECEIVED, subject to individually determined rates or contracts that have been agreed upon in\r\nwriting between the carrier and shipper, if applicable, otherwise to the rates, classifications\r\nand rules that have been established by the carrier and are available to the shipper, on request,\r\nand to all applicable state and federal regulations.", fontTinyNormal, blackBrush, legalLiabilityTextX, legalLiabilityTextY);


                        // Draw a vertical line to create two containers, Limited Liability and Shipper Signature
                        PointF shipmentLiabilityCodVerticalLineContainerStartPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 844F);
                        PointF shipmentLiabilityCodVerticalLineContainerStopPoint = new PointF(printArea.Width / 2F, pageStartPoint.Y + 910F);
                        e.Graphics.DrawLine(blackPen, shipmentLiabilityCodVerticalLineContainerStartPoint, shipmentLiabilityCodVerticalLineContainerStopPoint);

                        // Write the Shipper signature for not delivering unless payment is made
                        SizeF carrierCODAgreementTextSize = e.Graphics.MeasureString("The carrier shall not make delivery of this shipment without payment of freight\r\nand all other lawful charges.", fontTinyNormal);
                        float carrierCODAgreementTextX = (printArea.Width / 2F) + 6.125F;
                        float carrierCODAgreementTextY = pageStartPoint.Y + 828F + carrierCODAgreementTextSize.Height + 1.5F;
                        e.Graphics.DrawString("The carrier shall not make delivery of this shipment without payment of freight\r\nand all other lawful charges.", fontTinyNormal, blackBrush, carrierCODAgreementTextX, carrierCODAgreementTextY);


                        // Write the Shipper signature for not delivering unless payment is made
                        SizeF carrierCODAgreementSignatureTextSize = e.Graphics.MeasureString("Shipper Signature _________________________________________", fontSmallBold);
                        float carrierCODAgreementSignatureTextX = (printArea.Width / 2F) + 6.125F;
                        float carrierCODAgreementSignatureTextY = pageStartPoint.Y + 868F + carrierCODAgreementTextSize.Height + 1.5F;
                        e.Graphics.DrawString("Shipper Signature _________________________________________", fontSmallBold, blackBrush, carrierCODAgreementSignatureTextX, carrierCODAgreementSignatureTextY);

                        /// End of Shipment Liability Limitations
                        /////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////////////////////////
                        /// Shipment Terms and Signatures
                        /////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////////////////////////
                        // Draw a line to start the Shipper Signature Container                      
                        PointF startOfShipperSignatureContainerStartPoint = new PointF(printArea.Width - printArea.Width + 25F, pageStartPoint.Y + 910F);
                        PointF startOfShipperSignatureContainerStopPoint = new PointF(printArea.Width - 25, pageStartPoint.Y + 910F);
                        e.Graphics.DrawLine(blackPen, startOfShipperSignatureContainerStartPoint, startOfShipperSignatureContainerStopPoint);


                        // Shipper Signature                    
                        SizeF shipperSignatureLabelSize = e.Graphics.MeasureString("SHIPPER SIGNATURE / DATE", fontSmallBold);
                        float shipperSignatureLabelX = pageStartPoint.X + 12.5F;
                        float shipperSignatureLabelY = pageStartPoint.Y + 898F + shipperSignatureLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("SHIPPER SIGNATURE / DATE", fontSmallBold, blackBrush, shipperSignatureLabelX, shipperSignatureLabelY);

                        // Shipper Signature Legal Terms                    
                        SizeF shipperSignatureLegalTermsLabelSize = e.Graphics.MeasureString("This is to certify that the above named materials are properly\r\nclassified, packaged, marked and labeled, and are in proper\r\ncondition for transportation according to the applicable\r\nregulations of the DOT.", fontTinyNormal);
                        float shipperSignatureLegalTermsLabelX = pageStartPoint.X + 6.125F;
                        float shipperSignatureLegalTermsLabelY = pageStartPoint.Y + 889F + shipperSignatureLegalTermsLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("This is to certify that the above named materials are properly\r\nclassified, packaged, marked and labeled, and are in proper\r\ncondition for transportation according to the applicable\r\nregulations of the DOT.", fontTinyNormal, blackBrush, shipperSignatureLegalTermsLabelX, shipperSignatureLegalTermsLabelY);

                        /////////////////////////////////////////////////////////////////////////////////////
                        /// End of Shipper Signature Container
                        /////////////////////////////////////////////////////////////////////////////////////


                        /////////////////////////////////////////////////////////////////////////////////////
                        // Draw a vertical line in the Frieght Loaded and Counted Container 1/3 across the page width                       
                        PointF shipperSignatureVerticalLineContainerStartPoint = new PointF(printArea.Width / 3F, pageStartPoint.Y + 910F);
                        PointF shipperSignatureVerticalLineContainerStopPoint = new PointF(printArea.Width / 3F, printArea.Height - 25);
                        e.Graphics.DrawLine(blackPen, shipperSignatureVerticalLineContainerStartPoint, shipperSignatureVerticalLineContainerStopPoint);

                        /////////////////////////////////////////////////////////////////////////////////////
                        /// End of Freight Loaded and Counted Container
                        /////////////////////////////////////////////////////////////////////////////////////
                        ///

                        // Trailer Loaded //////////                   
                        SizeF trailerLoadedLabelSize = e.Graphics.MeasureString("TRAILER LOADED", fontSmallBold);
                        float trailerLoadedLabelX = (printArea.Width / 3F) + 6.125F;
                        float trailerLoadedLabelY = pageStartPoint.Y + 898F + trailerLoadedLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("TRAILER LOADED", fontSmallBold, blackBrush, trailerLoadedLabelX, trailerLoadedLabelY);

                        // Draw 2 rectangles as checkboxes

                        // Draw the Shipper loaded Checkbox
                        PointF shipperLoadedCheckboxStartPoint = new PointF((printArea.Width / 3F) + 8.125F, (pageStartPoint.Y + 920F + trailerLoadedLabelSize.Height + 1.5F));
                        e.Graphics.DrawRectangle(blackPen, shipperLoadedCheckboxStartPoint.X, shipperLoadedCheckboxStartPoint.Y, 12, 12);

                        // Draw the Carrier loaded Checkbox
                        PointF driverLoadedCheckboxStartPoint = new PointF((printArea.Width / 3F) + 8.125F, (pageStartPoint.Y + 945F + trailerLoadedLabelSize.Height + 1.5F));
                        e.Graphics.DrawRectangle(blackPen, driverLoadedCheckboxStartPoint.X, driverLoadedCheckboxStartPoint.Y, 12, 12);

                        // Draw the Text for the Checkboxes

                        // Shipper Loaded Text
                        SizeF shipperLoadedTextSize = e.Graphics.MeasureString("By Shipper", fontSmallNormal);
                        float shipperLoadedTextX = (printArea.Width / 3F) + 24.25F;
                        float shipperLoadedTextY = pageStartPoint.Y + 919F + shipperLoadedTextSize.Height + 1.5F;
                        e.Graphics.DrawString("By Shipper", fontSmallNormal, blackBrush, shipperLoadedTextX, shipperLoadedTextY);

                        // Driver Loaded Text
                        SizeF driverLoadedTextSize = e.Graphics.MeasureString("By Driver", fontSmallNormal);
                        float driverLoadedTextX = (printArea.Width / 3F) + 24.25F;
                        float driverLoadedTextY = pageStartPoint.Y + 944F + driverLoadedTextSize.Height + 1.5F;
                        e.Graphics.DrawString("By Driver", fontSmallNormal, blackBrush, driverLoadedTextX, driverLoadedTextY);

                        ////////////////////////////////////////////////////////////////////////////////////
                        // Freight Counted //////////                    
                        SizeF freightCountedLabelSize = e.Graphics.MeasureString("FREIGHT COUNTED", fontSmallBold);
                        float freightCountedLabelX = (printArea.Width / 2F) + 6.125F;
                        float freightCountedLabelY = pageStartPoint.Y + 898F + freightCountedLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("FREIGHT COUNTED", fontSmallBold, blackBrush, freightCountedLabelX, freightCountedLabelY);

                        // Draw 3 Checkboxes

                        // Draw the Shipper Counted Checkbox
                        PointF shipperCountedCheckbox1StartPoint = new PointF((printArea.Width / 2F) + 8.125F, (pageStartPoint.Y + 920F + trailerLoadedLabelSize.Height + 1.5F));
                        e.Graphics.DrawRectangle(blackPen, shipperCountedCheckbox1StartPoint.X, shipperCountedCheckbox1StartPoint.Y, 12, 12);

                        // Draw the Driver Counted Checkbox
                        PointF driverPalletsCountedCheckboxStartPoint = new PointF((printArea.Width / 2F) + 8.125F, (pageStartPoint.Y + 945F + trailerLoadedLabelSize.Height + 1.5F));
                        e.Graphics.DrawRectangle(blackPen, driverPalletsCountedCheckboxStartPoint.X, driverPalletsCountedCheckboxStartPoint.Y, 12, 12);

                        // Draw the Driver Counted Checkbox
                        PointF driverPiecesCountedCheckboxStartPoint = new PointF((printArea.Width / 2F) + 8.125F, (pageStartPoint.Y + 970F + trailerLoadedLabelSize.Height + 1.5F));
                        e.Graphics.DrawRectangle(blackPen, driverPiecesCountedCheckboxStartPoint.X, driverPiecesCountedCheckboxStartPoint.Y, 12, 12);

                        // Shipper Counted Text
                        SizeF shipperCountedTextSize = e.Graphics.MeasureString("By Shipper", fontSmallNormal);
                        float shipperCountedTextX = (printArea.Width / 2F) + 24.25F;
                        float shipperCountedTextY = pageStartPoint.Y + 919F + shipperLoadedTextSize.Height + 1.5F;
                        e.Graphics.DrawString("By Shipper", fontSmallNormal, blackBrush, shipperCountedTextX, shipperCountedTextY);

                        // Driver Counted By Pallets Loaded Text
                        SizeF driverCountedPalletsTextSize = e.Graphics.MeasureString("By Driver/By Pallet", fontSmallNormal);
                        float driverCountedPalletsTextX = (printArea.Width / 2F) + 24.25F;
                        float driverCountedPalletsTextY = pageStartPoint.Y + 944F + driverCountedPalletsTextSize.Height + 1.5F;
                        e.Graphics.DrawString("By Driver/By Pallet", fontSmallNormal, blackBrush, driverCountedPalletsTextX, driverCountedPalletsTextY);

                        // Driver Counted By Pieces Loaded Text
                        SizeF driverCountedPackagesTextSize = e.Graphics.MeasureString("By Driver/By Pieces", fontSmallNormal);
                        float driverCountedPackagesTextX = (printArea.Width / 2F) + 24.25F;
                        float driverCountedPackagesTextY = pageStartPoint.Y + 969F + driverCountedPackagesTextSize.Height + 1.5F;
                        e.Graphics.DrawString("By Driver/By Pieces", fontSmallNormal, blackBrush, driverCountedPackagesTextX, driverCountedPackagesTextY);

                        // End of Freight Cocunted
                        /////////////////////////////////////////////////////////////////////////////////////

                        /////////////////////////////////////////////////////////////////////////////////////
                        // Draw a vertical line in the Carrier Signature Container 2/3 across the page width                       
                        PointF carrierSignatureVerticalLineContainerStartPoint = new PointF((printArea.Width / 3F) + (printArea.Width / 3F), pageStartPoint.Y + 910F);
                        PointF carrierSignatureVerticalLineContainerStopPoint = new PointF((printArea.Width / 3F) + (printArea.Width / 3F), printArea.Height - 25);
                        e.Graphics.DrawLine(blackPen, carrierSignatureVerticalLineContainerStartPoint, carrierSignatureVerticalLineContainerStopPoint);

                        // Carrier Signature                    
                        SizeF carrierSignatureLabelSize = e.Graphics.MeasureString("CARRIER SIGNATURE / PICKUP DATE", fontSmallBold);
                        float carrierSignatureLabelX = (printArea.Width / 3F) + (printArea.Width / 3F) + 12.5F;
                        float carrierSignatureLabelY = pageStartPoint.Y + 898F + carrierSignatureLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("CARRIER SIGNATURE / PICKUP DATE", fontSmallBold, blackBrush, carrierSignatureLabelX, carrierSignatureLabelY);

                        // Shipper Signature Legal Terms                    
                        SizeF carrierSignatureLegalTermsLabelSize = e.Graphics.MeasureString("Carrier acknowledges receipt of packages and required placards,\r\ncertifies emergency response information was made available\r\nand/or carrier has the DOT emergency response guidebook nor\r\nequivalent documentation in the vehicle. Property described\r\nabove is received in good order, except as noted.", fontTinyNormal);
                        float carrierSignatureLegalTermsLabelX = (printArea.Width / 3F) + (printArea.Width / 3F) + 6.125F;
                        float carrierSignatureLegalTermsLabelY = pageStartPoint.Y + 880F + carrierSignatureLegalTermsLabelSize.Height + 1.5F;
                        e.Graphics.DrawString("Carrier acknowledges receipt of packages and required placards,\r\ncertifies emergency response information was made available\r\nand/or carrier has the DOT emergency response guidebook nor\r\nequivalent documentation in the vehicle. Property described\r\nabove is received in good order, except as noted.", fontTinyNormal, blackBrush, carrierSignatureLegalTermsLabelX, carrierSignatureLegalTermsLabelY);

                        ////////////////////////////////////////////////////////////////////////////////////
                        /// End of Carrier Signature Container
                        ////////////////////////////////////////////////////////////////////////////////////
                        ///

                        ////////////////////////////////////////////////////////////////////////////////////
                        /// Shipment Details - Containers, Pallets, Packages
                        /// 

                        PrintDocument_ShipmentDetails(sender, e);                      


                    }

                }                

            }

        }

        private void PrintDocument_ShipmentDetails(object sender, PrintPageEventArgs e)
        {
            ////////////////////////////////////////////////////////////////////////////////
            /// Program the shipment detail start points
            /// 

            // Set the Pallet, Package Container Object Start Point
            // This will represent temp index 0, the first pallet or package
            //

            // Define the printable area.
            RectangleF printArea = e.PageSettings.PrintableArea;

            pageStartPoint.X = 24F;
            pageStartPoint.Y = 24F;

            float objectContainerStartPointXkey = pageStartPoint.X + 12.5F;
            float objectContainerStartPointXvalue = pageStartPoint.X + 200F;
            float objectContainerStartPointY = pageStartPoint.Y + 350F;

            // Reset the TempStartIndex
            int tempStartIndex = 0;

            // Paginate the Pallets and Packages, deduct 1 because we start with zero or 0
            int startIndex = (CurrentPage * ItemsPerPage) - ItemsPerPage;
            int endIndex = Math.Min(startIndex + ItemsPerPage, TotalItems) - 1;

            // We can print 4 objects per page
            ///////////////////////////////////////////////////////////////////////////////
            /// Pallets
            /// 

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
                            objectContainerStartPointY += 200F;
                            break;

                        case 2:
                            // 3rd Pallet Top Right
                            objectContainerStartPointY = pageStartPoint.Y + 350F;
                            objectContainerStartPointXkey = printArea.Width / 2;
                            objectContainerStartPointXvalue = (printArea.Width / 2) + 200F;
                            break;

                        case 3:
                            // 4th Pallet Bottom Right
                            objectContainerStartPointY += 200F;

                            // Time to start a new page, if we have more Pallets or Package
                            // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                            tempStartIndex = -1;
                            break;

                    }

                    #region eGraphicsPallets
                    e.Graphics.DrawString("Pallet Name: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY);
                    e.Graphics.DrawString(pallet.PalletDescription, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY);

                    e.Graphics.DrawString("Total Cartons: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 16F);
                    e.Graphics.DrawString(pallet.BoxCount.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 16F);

                    e.Graphics.DrawString("Total Units: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 32F);
                    e.Graphics.DrawString(pallet.UnitCount.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 32F);

                    e.Graphics.DrawString("Num. Stackable Pallets: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 48F);
                    e.Graphics.DrawString(pallet.StackablePallet.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 48F);

                    e.Graphics.DrawString("Num. Unstackable Pallets: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 64F);
                    e.Graphics.DrawString(pallet.NonStackablePallet.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 64F);

                    // Draw the line under "Num. Unstackable Pallets" section
                    e.Graphics.DrawLine(blackThinPen, objectContainerStartPointXkey, objectContainerStartPointY + 80F + 4F, objectContainerStartPointXvalue + 165F, objectContainerStartPointY + 80F + 4F);

                    e.Graphics.DrawString("Total Pallet Weight: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 88F);
                    e.Graphics.DrawString(pallet.Weight.ToString() + " " + pallet.WeightType, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 88F);

                    e.Graphics.DrawString("Total Pallet Volume: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 104F);
                    e.Graphics.DrawString(pallet.Volume.ToString() + " " + pallet.VolumeType, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 104F);

                    // Draw the line under "Total Pallet Volume" section
                    e.Graphics.DrawLine(blackThinPen, objectContainerStartPointXkey, objectContainerStartPointY + 120F + 4F, objectContainerStartPointXvalue + 165F, objectContainerStartPointY + 120F + 4F);

                    if (pallet.ClassCode != null)
                    {
                        e.Graphics.DrawString("Freight Class: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 128F);
                        e.Graphics.DrawString(pallet.ClassCode.CodeNumber.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 128F);
                    }

                    e.Graphics.DrawString("Shipment Type: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 144F);
                    e.Graphics.DrawString(Bol.ShipmentType, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 144F);

                    e.Graphics.DrawString("Declared (Insurable) Value: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 160F);
                    e.Graphics.DrawString(pallet.EstimatedValue.ToString() + " " + pallet.CurrencyCode, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 160F);
                    #endregion eGraphicsPallets

                    // Increment the temp start index, if were below the ItemsPerPage
                    if (tempStartIndex < ItemsPerPage) { 
                        tempStartIndex++; 
                    } else
                    {
                        tempStartIndex = 0;
                    }

                    // Remove a pallet from the counter, that tracks when to generate packages
                    palletCount--;

                }

                Console.WriteLine("startIndex: " + startIndex + " endIndex: " + endIndex);

                // After printing the content for the current page, determine if there are more pages to print
                if (tempStartIndex == 0 || endIndex < TotalItems - 1)
                {
                    e.HasMorePages = true;
                    CurrentPage++; // Move to the next page for the next print event
                }
                else
                {
                    e.HasMorePages = false; // No more pages to print
                    CurrentPage = 0; // Reset the page counter for potential future print jobs
                }                

            }

            ///////////////////////////////////////////////////////////////////////////////
            /// Packages
            ///

            if (Bol.Packages != null && palletCount == 0 && packageCount > 0)
            {

                if (Bol.Packages.Count > 0)
                {

                    for (int i = startIndex; i < endIndex; i++)
                    {
                        if (this.packageCount == 0)
                        {
                            // Break out of the loop if packageCount is 0
                            startIndex = 0;
                            endIndex = 0;
                            break;
                        }

                        PACKAGES package = this.Bol.Packages[i];

                        // Create code to set the new print position for tempIndex 1 to 3
                        switch (tempStartIndex)
                        {
                            case 0:
                                // use the defined above the loop
                                // 1st Package Top Left
                                break;

                            case 1:
                                // 2nd Package bottom left
                                objectContainerStartPointY += 200F;
                                break;

                            case 2:
                                // 3rd Package Top Right
                                objectContainerStartPointY = pageStartPoint.Y + 350F;
                                objectContainerStartPointXkey = printArea.Width / 2;
                                objectContainerStartPointXvalue = (printArea.Width / 2) + 200F;
                                break;

                            case 3:
                                // 4th Package Bottom Right
                                objectContainerStartPointY += 200F;

                                // Time to start a new page, if we have more Pallets or Package
                                // Set this to -1, so the last pallet can print, and still toggle tempStartIndex++
                                tempStartIndex = -1;
                                break;

                        }

                        String measurementCode = package.MeasurementCode == "English" ? "IN" : "CM";

                        #region eGraphicsPackages

                        // Package - Name or Description
                        e.Graphics.DrawString("Package Name: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY);
                        e.Graphics.DrawString(package.PackageDescription, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY);

                        // Package - Total Units
                        e.Graphics.DrawString("Total Units: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 16F);
                        e.Graphics.DrawString(package.UnitCount.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 16F);

                        // Package - Length Measurement
                        e.Graphics.DrawString("Length: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 32F);
                        e.Graphics.DrawString(package.Length.ToString() + " " + measurementCode, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 32F);

                        // Package - Width Measurement
                        e.Graphics.DrawString("Width: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 48F);
                        e.Graphics.DrawString(package.Width.ToString() + " " + measurementCode, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 48F);

                        // Package - Height Measurement
                        e.Graphics.DrawString("Height: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 64F);
                        e.Graphics.DrawString(package.Height.ToString() + " " + measurementCode, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 64F);

                        // Draw the line under "Height" section
                        e.Graphics.DrawLine(blackThinPen, objectContainerStartPointXkey, objectContainerStartPointY + 80F + 4F, objectContainerStartPointXvalue + 165F, objectContainerStartPointY + 80F + 4F);

                        e.Graphics.DrawString("Total Package Weight: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 88F);
                        e.Graphics.DrawString(package.Weight.ToString() + " " + package.WeightType, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 88F);

                        e.Graphics.DrawString("Total Package Volume: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 104F);
                        e.Graphics.DrawString(package.Volume.ToString() + " " + package.VolumeType, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 104F);

                        // Draw the line under "Total Pallet Volume" section
                        e.Graphics.DrawLine(blackThinPen, objectContainerStartPointXkey, objectContainerStartPointY + 120F + 4F, objectContainerStartPointXvalue + 165F, objectContainerStartPointY + 120F + 4F);

                        if (package.ClassCode != null)
                        {
                            e.Graphics.DrawString("Freight Class: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 128F);
                            e.Graphics.DrawString(package.ClassCode.CodeNumber.ToString(), fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 128F);
                        }

                        e.Graphics.DrawString("Shipment Type: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 144F);
                        e.Graphics.DrawString(Bol.ShipmentType, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 144F);

                        e.Graphics.DrawString("Declared (Insurable) Value: ", fontMedium10Bold, blackBrush, objectContainerStartPointXkey, objectContainerStartPointY + 160F);
                        e.Graphics.DrawString(package.EstimatedValue.ToString() + " " + package.CurrencyCode, fontMedium10Normal, blackBrush, objectContainerStartPointXvalue, objectContainerStartPointY + 160F);


                        #endregion eGraphicsPackages

                        // Console.WriteLine("startIndex: " + startIndex + " endIndex: " + endIndex);

                        // Remove a pallet from the counter, that tracks when to generate packages
                        packageCount--;

                        // After printing the content for the current page, determine if there are more pages to print
                        if (tempStartIndex == 0 || endIndex < TotalItems - 1)
                        {
                            e.HasMorePages = true;
                            CurrentPage++; // Move to the next page for the next print event
                        }
                        else
                        {
                            e.HasMorePages = false; // No more pages to print
                            CurrentPage = 0; // Reset the page counter for potential future print jobs
                        }                        

                    }
                }

            }

        }

        /// <summary>
        /// Counters need to be defined on Page One, but not on addtional pages, or else the Shipment Detail counter won't work
        /// This is the start point of ensuring all shipment details are printed and new pages are generated
        /// </summary>
        private void DefineCounters()
        {
            this.containerCount = this.Bol.Containers != null ? this.Bol.Containers.Count() : 0;
            this.palletCount = this.Bol.Pallets != null ? this.Bol.Pallets.Count() : 0;
            this.packageCount = this.Bol.Packages != null ? this.Bol.Packages.Count() : 0;

            // Set the flag to true to indicate that counters are defined
            countersDefined = true;
        }

        /// <summary>
        /// Receives a call to end Printing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            this.CurrentPage = 0;
        }

        // Override the OnPrintPage method to call your custom PrintPage event handler
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            PrintDocument_PrintPage(this, e);
        }
    }
}
