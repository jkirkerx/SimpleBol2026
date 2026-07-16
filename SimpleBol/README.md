# SimpleBol 2026

SimpleBol is a Windows desktop application for creating, managing, printing, and emailing bills of lading. It keeps shipping records in MongoDB and provides tools for customers, vendors, shippers, billing accounts, freight classes, and billing disputes.

Current release: **2026.07.16**

## Features

- Create and edit bills of lading
- Search and filter BOL records by date, shipper, vendor, and customer
- Print BOLs with line or square templates
- Email BOL PDFs through Gmail, Microsoft 365, SendGrid, or SMTP
- Track printed, emailed, and disputed status in the BOL list
- Display Windows notifications for successful print and email jobs
- Maintain customers, vendors, shippers, locations, contacts, pallets, packages, and containers
- Reconcile quoted and actual freight charges
- Store application data in MongoDB

## Requirements

- Windows 10 or Windows 11, x64
- .NET 10 SDK for development
- MongoDB Community Edition or access to a MongoDB server
- WiX Toolset v4 for building the MSI installer

The installed application is self-contained and does not require a separate .NET runtime installation.

## Build the application

```powershell
dotnet restore SimpleBol.sln
dotnet build SimpleBol.sln -c Release
```

## Build the installer

Install WiX and its UI extension if they are not already available:

```powershell
dotnet tool install --global wix
wix extension add WixToolset.UI.wixext
```

Build the Release MSI:

```powershell
dotnet build SimpleBol.Installer\SimpleBol.Installer.wixproj -c Release
```

The installer is written to:

```text
SimpleBol.Installer\bin\Release\SimpleBol2026.msi
```

## Email configuration

Email providers are configured inside SimpleBol. Gmail and Microsoft 365 may open a browser for account authorization the first time they are used. Keep credentials and provider secrets out of source control.

## Project structure

- `SimpleBol` - main Windows Forms application
- `SimpleBol.PrintNotifications` - notification-area helper for print and email confirmations
- `SimpleBol.Installer` - WiX v4 MSI installer

## License

See the installer EULA in `SimpleBol.Installer/EULA.rtf`.
