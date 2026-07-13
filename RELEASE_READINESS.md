# SimpleBol Release Readiness

## Current Status

- Release build passes with `dotnet build SimpleBol.sln -c Release`.
- A self-contained Windows x64 publish was generated at `artifacts\publish\win-x64`.
- There is no installer project in the repository yet.
- The project currently targets `net7.0-windows`, which is out of support.

## Blocking Before Sale Delivery

1. Replace the first-run MongoDB installer flow.
   - The app currently attempts to download/install MongoDB Community 4.0 from an old MSI URL.
   - A buyer-safe release should either connect to an existing MongoDB instance, use MongoDB Atlas, or install a supported local database through a maintained installer path.

2. Remove default credentials from generated settings.
   - `AppSettingsJson.CreateAppSettings()` currently writes default MongoDB credentials.
   - First run should prompt for database settings or create local credentials securely.

3. Fix database connection testing.
   - `MongoDbRepository.TestDbConnStr(string testConnStr)` currently ignores the supplied connection string.
   - Connection checks should run a real ping command against the selected database.

4. Move to a supported .NET target.
   - Recommended: migrate to a supported Windows desktop target such as `net10.0-windows` if the buyer will receive a self-contained build.

5. Update vulnerable/incompatible packages.
   - `MimeKit 4.4.0` has known vulnerability warnings.
   - Legacy `NBug` crash reporting was removed; unhandled exceptions now flow through the existing NLog/ErrorLogging path.

## Recommended Deployment Model

For a near-term sale, ship as a Windows desktop app with:

- A self-contained `win-x64` application bundle.
- A proper installer, preferably WiX or Inno Setup.
- A first-run database configuration dialog.
- MongoDB Atlas or customer-provided MongoDB as the default recommended database.
- Optional advanced path for a local MongoDB installation.

## Smoke Test Checklist

- Fresh Windows user profile starts the app without existing `%AppData%\SimpleBol`.
- App handles missing MongoDB by showing configuration, not by failing or trying an obsolete installer.
- MongoDB credentials can be entered, saved, and reloaded.
- Seed data loads for countries, regions, NMFC codes, and freight classes.
- Create, edit, print, and email a BOL.
- Create and resolve a billing dispute.
- Add/edit/delete customers, vendors, shippers, bill-to accounts, and user accounts.
- Generated PDFs open correctly.
- Release build runs from the published folder on a machine without the .NET runtime installed.

## Commands

```powershell
dotnet build SimpleBol.sln -c Release
dotnet publish SimpleBol.csproj -c Release -r win-x64 --self-contained true -o artifacts\publish\win-x64
```
