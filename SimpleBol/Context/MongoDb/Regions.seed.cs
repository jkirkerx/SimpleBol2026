using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Models.MongoDb;
using SimpleBol.Setup;

namespace SimpleBol.MongoDb.Seeds
{
    public class SeedRegions
    {
        /// <summary>
        /// Generate Website REGIONS for MongoDb
        /// </summary>
        /// <param name="_database"></param>
        /// <returns></returns>
        public static async Task SeedAsync(IMongoDatabase _database)
        {
            // Older SimpleBol databases stored SalesTax on region documents.
            // Remove it before using the current typed model so upgrades do not
            // fail deserialization during startup.
            var rawRegions = _database.GetCollection<BsonDocument>(
                MongoDbCollectionNames.Regions);
            await rawRegions.UpdateManyAsync(
                Builders<BsonDocument>.Filter.Exists("SalesTax"),
                Builders<BsonDocument>.Update.Unset("SalesTax"));

            var countries = _database.GetCollection<COUNTRIES>(MongoDbCollectionNames.Countries);
            var regions = _database.GetCollection<REGIONS>(MongoDbCollectionNames.Regions);

            var seedRegions = new List<REGIONS>();
            var countryList = await countries.Find(FilterDefinition<COUNTRIES>.Empty).ToListAsync();
            foreach (var country in countryList)
            {
                switch (country.ShortName)
                {
                        // United REGIONS
                        case "US":

                            // A
                            var US_AL_Id = ObjectId.GenerateNewId();
                            var state_AL = new REGIONS() { RegionId = US_AL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Alabama", ShortName = "AL", Enabled = true };
                            seedRegions.Add(state_AL);

                            var US_AK_Id = ObjectId.GenerateNewId();
                            var state_AK = new REGIONS() { RegionId = US_AK_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Alaska", ShortName = "AK", Enabled = true };
                            seedRegions.Add(state_AK);

                            var US_AZ_Id = ObjectId.GenerateNewId();
                            var state_AZ = new REGIONS() { RegionId = US_AZ_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Arizona", ShortName = "AZ", Enabled = true };
                            seedRegions.Add(state_AZ);

                            ObjectId US_AR_Id = ObjectId.GenerateNewId();
                            var state_AR = new REGIONS() { RegionId = US_AR_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Arkansas", ShortName = "AR", Enabled = true };
                            seedRegions.Add(state_AR);

                            // C
                            ObjectId US_CA_Id = ObjectId.GenerateNewId();
                            var state_CA = new REGIONS() { RegionId = US_CA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "California", ShortName = "CA", Enabled = true };
                            seedRegions.Add(state_CA);

                            ObjectId US_CO_Id = ObjectId.GenerateNewId();
                            var state_CO = new REGIONS() { RegionId = US_CO_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Colorado", ShortName = "CO", Enabled = true };
                            seedRegions.Add(state_CO);

                            ObjectId US_CT_Id = ObjectId.GenerateNewId();
                            var state_CT = new REGIONS() { RegionId = US_CT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Connecticut", ShortName = "CT", Enabled = true };
                            seedRegions.Add(state_CT);

                            // D
                            ObjectId US_DE_Id = ObjectId.GenerateNewId();
                            var state_DE = new REGIONS() { RegionId = US_DE_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Delaware", ShortName = "DE", Enabled = true };
                            seedRegions.Add(state_DE);

                            // F
                            ObjectId US_FL_Id = ObjectId.GenerateNewId();
                            var state_FL = new REGIONS() { RegionId = US_FL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Florida", ShortName = "FL", Enabled = true };
                            seedRegions.Add(state_FL);

                            // G
                            ObjectId US_GA_Id = ObjectId.GenerateNewId();
                            var state_GA = new REGIONS() { RegionId = US_GA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Georgia", ShortName = "GA", Enabled = true };
                            seedRegions.Add(state_GA);

                            // H
                            ObjectId US_HI_Id = ObjectId.GenerateNewId();
                            var state_HI = new REGIONS() { RegionId = US_HI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Hawaii", ShortName = "HI", Enabled = true };
                            seedRegions.Add(state_HI);

                            // I
                            ObjectId US_ID_Id = ObjectId.GenerateNewId();
                            var state_ID = new REGIONS() { RegionId = US_ID_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Idaho", ShortName = "ID", Enabled = true };
                            seedRegions.Add(state_ID);

                            ObjectId US_IL_Id = ObjectId.GenerateNewId();
                            var state_IL = new REGIONS() { RegionId = US_IL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Illinois", ShortName = "IL", Enabled = true };
                            seedRegions.Add(state_IL);

                            ObjectId US_IN_Id = ObjectId.GenerateNewId();
                            var state_IN = new REGIONS() { RegionId = US_IN_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Indiana", ShortName = "IN", Enabled = true };
                            seedRegions.Add(state_IN);

                            ObjectId US_IA_Id = ObjectId.GenerateNewId();
                            var state_IA = new REGIONS() { RegionId = US_IA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Iowa", ShortName = "IA", Enabled = true };
                            seedRegions.Add(state_IA);

                            // K
                            ObjectId US_KS_Id = ObjectId.GenerateNewId();
                            var state_KS = new REGIONS() { RegionId = US_KS_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Kansas", ShortName = "KS", Enabled = true };
                            seedRegions.Add(state_KS);

                            ObjectId US_KY_Id = ObjectId.GenerateNewId();
                            var state_KY = new REGIONS() { RegionId = US_KY_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Kentucky", ShortName = "KY", Enabled = true };
                            seedRegions.Add(state_KY);

                            // L
                            ObjectId US_LA_Id = ObjectId.GenerateNewId();
                            var state_LA = new REGIONS() { RegionId = US_LA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Louisiana", ShortName = "LA", Enabled = true };
                            seedRegions.Add(state_LA);

                            // M
                            ObjectId US_ME_Id = ObjectId.GenerateNewId();
                            var state_ME = new REGIONS() { RegionId = US_ME_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Maine", ShortName = "ME", Enabled = true };
                            seedRegions.Add(state_ME);

                            ObjectId US_MD_Id = ObjectId.GenerateNewId();
                            var state_MD = new REGIONS() { RegionId = US_MD_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Maryland", ShortName = "MD", Enabled = true };
                            seedRegions.Add(state_MD);

                            ObjectId US_MA_Id = ObjectId.GenerateNewId();
                            var state_MA = new REGIONS() { RegionId = US_MA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Massachusetts", ShortName = "MA", Enabled = true };
                            seedRegions.Add(state_MA);

                            ObjectId US_MI_Id = ObjectId.GenerateNewId();
                            var state_MI = new REGIONS() { RegionId = US_MI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Michigan", ShortName = "MI", Enabled = true };
                            seedRegions.Add(state_MI);

                            ObjectId US_MN_Id = ObjectId.GenerateNewId();
                            var state_MN = new REGIONS() { RegionId = US_MN_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Minnesota", ShortName = "MN", Enabled = true };
                            seedRegions.Add(state_MN);

                            ObjectId US_MS_Id = ObjectId.GenerateNewId();
                            var state_MS = new REGIONS() { RegionId = US_MS_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Mississippi", ShortName = "MS", Enabled = true };
                            seedRegions.Add(state_MS);

                            ObjectId US_MO_Id = ObjectId.GenerateNewId();
                            var state_MO = new REGIONS() { RegionId = US_MO_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Missouri", ShortName = "MO", Enabled = true };
                            seedRegions.Add(state_MO);

                            ObjectId US_MT_Id = ObjectId.GenerateNewId();
                            var state_MT = new REGIONS() { RegionId = US_MT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Montana", ShortName = "MT", Enabled = true };
                            seedRegions.Add(state_MT);

                            // N
                            ObjectId US_NE_Id = ObjectId.GenerateNewId();
                            var state_NE = new REGIONS() { RegionId = US_NE_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Nebraska", ShortName = "NE", Enabled = true };
                            seedRegions.Add(state_NE);

                            ObjectId US_NV_Id = ObjectId.GenerateNewId();
                            var state_NV = new REGIONS() { RegionId = US_NV_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Nevada", ShortName = "NV", Enabled = true };
                            seedRegions.Add(state_NV);

                            ObjectId US_NH_Id = ObjectId.GenerateNewId();
                            var state_NH = new REGIONS() { RegionId = US_NH_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "New Hampshire", ShortName = "NH", Enabled = true };
                            seedRegions.Add(state_NH);

                            ObjectId US_NJ_Id = ObjectId.GenerateNewId();
                            var state_NJ = new REGIONS() { RegionId = US_NJ_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "New Jersey", ShortName = "NJ", Enabled = true };
                            seedRegions.Add(state_NJ);

                            ObjectId US_NM_Id = ObjectId.GenerateNewId();
                            var state_NM = new REGIONS() { RegionId = US_NM_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "New Mexico", ShortName = "NM", Enabled = true };
                            seedRegions.Add(state_NM);

                            ObjectId US_NY_Id = ObjectId.GenerateNewId();
                            var state_NY = new REGIONS() { RegionId = US_NY_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "New York", ShortName = "NY", Enabled = true };
                            seedRegions.Add(state_NY);

                            ObjectId US_NC_Id = ObjectId.GenerateNewId();
                            var state_NC = new REGIONS() { RegionId = US_NC_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "North Carolina", ShortName = "NC", Enabled = true };
                            seedRegions.Add(state_NC);

                            ObjectId US_ND_Id = ObjectId.GenerateNewId();
                            var state_ND = new REGIONS() { RegionId = US_ND_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "North Dakota", ShortName = "ND", Enabled = true };
                            seedRegions.Add(state_ND);

                            // O
                            ObjectId US_OH_Id = ObjectId.GenerateNewId();
                            var state_OH = new REGIONS() { RegionId = US_OH_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Ohio", ShortName = "OH", Enabled = true };
                            seedRegions.Add(state_OH);

                            ObjectId US_OK_Id = ObjectId.GenerateNewId();
                            var state_OK = new REGIONS() { RegionId = US_OK_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Oklahoma", ShortName = "OK", Enabled = true };
                            seedRegions.Add(state_OK);

                            ObjectId US_OR_Id = ObjectId.GenerateNewId();
                            var state_OR = new REGIONS() { RegionId = US_OR_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Oregon", ShortName = "OR", Enabled = true };
                            seedRegions.Add(state_OR);

                            // P
                            ObjectId US_PA_Id = ObjectId.GenerateNewId();
                            var state_PA = new REGIONS() { RegionId = US_PA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Pennsylvania", ShortName = "PA", Enabled = true };
                            seedRegions.Add(state_PA);

                            // R
                            ObjectId US_RI_Id = ObjectId.GenerateNewId();
                            var state_RI = new REGIONS() { RegionId = US_RI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Rhode Island", ShortName = "RI", Enabled = true };
                            seedRegions.Add(state_RI);

                            // S
                            ObjectId US_SC_Id = ObjectId.GenerateNewId();
                            var state_SC = new REGIONS() { RegionId = US_SC_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "South Carolina", ShortName = "SC", Enabled = true };
                            seedRegions.Add(state_SC);

                            ObjectId US_SD_Id = ObjectId.GenerateNewId();
                            var state_SD = new REGIONS() { RegionId = US_SD_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "South Dakota", ShortName = "SD", Enabled = true };
                            seedRegions.Add(state_SD);

                            // T
                            ObjectId US_TX_Id = ObjectId.GenerateNewId();
                            var state_TX = new REGIONS() { RegionId = US_TX_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Texas", ShortName = "TX", Enabled = true };
                            seedRegions.Add(state_TX);

                            ObjectId US_TN_Id = ObjectId.GenerateNewId();
                            var state_TN = new REGIONS() { RegionId = US_TN_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Tennessee", ShortName = "TN", Enabled = true };
                            seedRegions.Add(state_TN);

                            // U
                            ObjectId US_UT_Id = ObjectId.GenerateNewId();
                            var state_UT = new REGIONS() { RegionId = US_UT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Utah", ShortName = "UT", Enabled = true };
                            seedRegions.Add(state_UT);

                            // V
                            ObjectId US_VT_Id = ObjectId.GenerateNewId();
                            var state_VT = new REGIONS() { RegionId = US_VT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Vermont", ShortName = "VT", Enabled = true };
                            seedRegions.Add(state_VT);

                            ObjectId US_VA_Id = ObjectId.GenerateNewId();
                            var state_VA = new REGIONS() { RegionId = US_VA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Virginia", ShortName = "VA", Enabled = true };
                            seedRegions.Add(state_VA);

                            //W
                            ObjectId US_WA_Id = ObjectId.GenerateNewId();
                            var state_WA = new REGIONS() { RegionId = US_WA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Washington", ShortName = "WA", Enabled = true };
                            seedRegions.Add(state_WA);

                            ObjectId US_WV_Id = ObjectId.GenerateNewId();
                            var state_WV = new REGIONS() { RegionId = US_WV_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "West Virginia", ShortName = "WV", Enabled = true };
                            seedRegions.Add(state_WV);

                            ObjectId US_WI_Id = ObjectId.GenerateNewId();
                            var state_WI = new REGIONS() { RegionId = US_WI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Wisconsin", ShortName = "WI", Enabled = true };
                            seedRegions.Add(state_WI);

                            ObjectId US_WY_Id = ObjectId.GenerateNewId();
                            var state_WY = new REGIONS() { RegionId = US_WY_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Wyoming", ShortName = "WY", Enabled = true };
                            seedRegions.Add(state_WY);

                            // Federal District
                            ObjectId US_DC_Id = ObjectId.GenerateNewId();
                            var state_DC = new REGIONS() { RegionId = US_DC_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "District of Columbia", ShortName = "DC", Enabled = true };
                            seedRegions.Add(state_DC);

                            // Territories
                            ObjectId US_AS_Id = ObjectId.GenerateNewId();
                            var state_AS = new REGIONS() { RegionId = US_AS_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "American Samoa", ShortName = "AS", Enabled = true };
                            seedRegions.Add(state_AS);

                            ObjectId US_GU_Id = ObjectId.GenerateNewId();
                            var state_GU = new REGIONS() { RegionId = US_GU_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Guam", ShortName = "GU", Enabled = true };
                            seedRegions.Add(state_GU);

                            ObjectId US_MP_Id = ObjectId.GenerateNewId();
                            var state_MP = new REGIONS() { RegionId = US_MP_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Northern Mariana Islands", ShortName = "MP", Enabled = true };
                            seedRegions.Add(state_MP);

                            ObjectId US_PR_Id = ObjectId.GenerateNewId();
                            var state_PR = new REGIONS() { RegionId = US_PR_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Puerto Rico", ShortName = "PR", Enabled = true };
                            seedRegions.Add(state_PR);

                            ObjectId US_VI_Id = ObjectId.GenerateNewId();
                            var state_VI = new REGIONS() { RegionId = US_VI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Virgin Islands", ShortName = "VI", Enabled = true };
                            seedRegions.Add(state_VI);

                            break;

                        ///////////////////////////////////////////////////////////////////////
                        // Canada
                        case "CA":

                            // A
                            ObjectId CA_AB_Id = ObjectId.GenerateNewId();
                            var province_AB = new REGIONS() { RegionId = CA_AB_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Alberta", ShortName = "AB", Enabled = true };
                            seedRegions.Add(province_AB);

                            // B
                            ObjectId CA_BC_Id = ObjectId.GenerateNewId();
                            var province_BC = new REGIONS() { RegionId = CA_BC_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "British Columbia", ShortName = "BC", Enabled = true };
                            seedRegions.Add(province_BC);

                            // O
                            ObjectId CA_ON_Id = ObjectId.GenerateNewId();
                            var province_ON = new REGIONS() { RegionId = CA_ON_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Ontario", ShortName = "ON", Enabled = true };
                            seedRegions.Add(province_ON);

                            // Q
                            ObjectId CA_QC_Id = ObjectId.GenerateNewId();
                            var province_QC = new REGIONS() { RegionId = CA_QC_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Quebec", ShortName = "QC", Enabled = true };
                            seedRegions.Add(province_QC);

                            // M
                            ObjectId CA_MB_Id = ObjectId.GenerateNewId();
                            var province_MB = new REGIONS() { RegionId = CA_MB_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Manitoba", ShortName = "MB", Enabled = true };
                            seedRegions.Add(province_MB);

                            // N
                            ObjectId CA_NB_Id = ObjectId.GenerateNewId();
                            var province_NB = new REGIONS() { RegionId = CA_NB_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "New Brunswick", ShortName = "NB", Enabled = true };
                            seedRegions.Add(province_NB);

                            ObjectId CA_NS_Id = ObjectId.GenerateNewId();
                            var province_NS = new REGIONS() { RegionId = CA_NS_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Nova Scotia", ShortName = "NS", Enabled = true };
                            seedRegions.Add(province_NS);

                            ObjectId CA_NL_Id = ObjectId.GenerateNewId();
                            var province_NL = new REGIONS() { RegionId = CA_NL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Newfoundland / Labrador", ShortName = "NL", Enabled = true };
                            seedRegions.Add(province_NL);

                            // P
                            ObjectId CA_PE_Id = ObjectId.GenerateNewId();
                            var province_PE = new REGIONS() { RegionId = CA_PE_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Prince Edward Island", ShortName = "PE", Enabled = true };
                            seedRegions.Add(province_PE);

                            // S
                            ObjectId CA_SK_Id = ObjectId.GenerateNewId();
                            var province_SK = new REGIONS() { RegionId = CA_SK_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Saskatchewan", ShortName = "SK", Enabled = true };
                            seedRegions.Add(province_SK);

                            // Territories
                            ObjectId CA_NT_Id = ObjectId.GenerateNewId();
                            var province_NT = new REGIONS() { RegionId = CA_NT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Northwest Territories", ShortName = "NT", Enabled = true };
                            seedRegions.Add(province_NT);

                            ObjectId CA_YT_Id = ObjectId.GenerateNewId();
                            var province_YT = new REGIONS() { RegionId = CA_YT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Yukon", ShortName = "YT", Enabled = true };
                            seedRegions.Add(province_YT);

                            ObjectId CA_NU_Id = ObjectId.GenerateNewId();
                            var province_NU = new REGIONS() { RegionId = CA_NU_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Nunavut", ShortName = "NU", Enabled = true };
                            seedRegions.Add(province_NU);

                            break;


                        ///////////////////////////////////////////////////////////////////////
                        // Mexico
                        case "MX":

                            // A
                            ObjectId MX_AG_Id = ObjectId.GenerateNewId();
                            var state_MX_AG = new REGIONS() { RegionId = MX_AG_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Aguascalientes", ShortName = "AG", Enabled = true };
                            seedRegions.Add(state_MX_AG);

                            // B
                            ObjectId MX_BN_Id = ObjectId.GenerateNewId();
                            var state_MX_BN = new REGIONS() { RegionId = MX_BN_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Baja California", ShortName = "BN", Enabled = true };
                            seedRegions.Add(state_MX_BN);

                            ObjectId MX_BS_Id = ObjectId.GenerateNewId();
                            var state_MX_BS = new REGIONS() { RegionId = MX_BS_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Baja California Sur", ShortName = "BS", Enabled = true };
                            seedRegions.Add(state_MX_BS);

                            // C
                            ObjectId MX_CM_Id = ObjectId.GenerateNewId();
                            var state_MX_CM = new REGIONS() { RegionId = MX_CM_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Campeche", ShortName = "CM", Enabled = true };
                            seedRegions.Add(state_MX_CM);

                            ObjectId MX_CP_Id = ObjectId.GenerateNewId();
                            var state_MX_CP = new REGIONS() { RegionId = MX_CP_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Chiapas", ShortName = "CP", Enabled = true };
                            seedRegions.Add(state_MX_CP);

                            ObjectId MX_CH_Id = ObjectId.GenerateNewId();
                            var state_MX_CH = new REGIONS() { RegionId = MX_CH_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Chihuahua", ShortName = "CH", Enabled = true };
                            seedRegions.Add(state_MX_CH);

                            ObjectId MX_CA_Id = ObjectId.GenerateNewId();
                            var state_MX_CA = new REGIONS() { RegionId = MX_CA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Coahuila", ShortName = "CA", Enabled = true };
                            seedRegions.Add(state_MX_CA);

                            ObjectId MX_CL_Id = ObjectId.GenerateNewId();
                            var state_MX_CL = new REGIONS() { RegionId = MX_CL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Colima", ShortName = "CL", Enabled = true };
                            seedRegions.Add(state_MX_CL);

                            // D
                            ObjectId MX_DU_Id = ObjectId.GenerateNewId();
                            var state_MX_DU = new REGIONS() { RegionId = MX_DU_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Durango", ShortName = "DU", Enabled = true };
                            seedRegions.Add(state_MX_DU);

                            // G
                            ObjectId MX_GT_Id = ObjectId.GenerateNewId();
                            var state_MX_GT = new REGIONS() { RegionId = MX_GT_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Guanajuato", ShortName = "GT", Enabled = true };
                            seedRegions.Add(state_MX_GT);

                            ObjectId MX_GR_Id = ObjectId.GenerateNewId();
                            var state_MX_GR = new REGIONS() { RegionId = MX_GR_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Guerrero", ShortName = "GR", Enabled = true };
                            seedRegions.Add(state_MX_GR);

                            // Federal district
                            ObjectId MX_DF_Id = ObjectId.GenerateNewId();
                            var state_MX_DF = new REGIONS() { RegionId = MX_DF_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Federal District", ShortName = "DF", Enabled = true };
                            seedRegions.Add(state_MX_DF);

                            // H
                            ObjectId MX_HI_Id = ObjectId.GenerateNewId();
                            var state_MX_HI = new REGIONS() { RegionId = MX_HI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Hidalgo", ShortName = "HI", Enabled = true };
                            seedRegions.Add(state_MX_HI);

                            // J
                            ObjectId MX_JA_Id = ObjectId.GenerateNewId();
                            var state_MX_JA = new REGIONS() { RegionId = MX_JA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Jalisco", ShortName = "JA", Enabled = true };
                            seedRegions.Add(state_MX_JA);

                            // M
                            ObjectId MX_MX_Id = ObjectId.GenerateNewId();
                            var state_MX_MX = new REGIONS() { RegionId = MX_MX_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "México", ShortName = "MX", Enabled = true };
                            seedRegions.Add(state_MX_MX);

                            ObjectId MX_MC_Id = ObjectId.GenerateNewId();
                            var state_MX_MC = new REGIONS() { RegionId = MX_MC_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Michoacán", ShortName = "MC", Enabled = true };
                            seedRegions.Add(state_MX_MC);

                            ObjectId MX_MR_Id = ObjectId.GenerateNewId();
                            var state_MX_MR = new REGIONS() { RegionId = MX_MR_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Morelos", ShortName = "MR", Enabled = true };
                            seedRegions.Add(state_MX_MR);

                            // N
                            ObjectId MX_NA_Id = ObjectId.GenerateNewId();
                            var state_MX_NA = new REGIONS() { RegionId = MX_NA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Nayarit", ShortName = "NA", Enabled = true };
                            seedRegions.Add(state_MX_NA);

                            ObjectId MX_NL_Id = ObjectId.GenerateNewId();
                            var state_MX_NL = new REGIONS() { RegionId = MX_NL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Nuevo León", ShortName = "NL", Enabled = true };
                            seedRegions.Add(state_MX_NL);

                            // O
                            ObjectId MX_OA_Id = ObjectId.GenerateNewId();
                            var state_MX_OA = new REGIONS() { RegionId = MX_OA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Oaxaca", ShortName = "OA", Enabled = true };
                            seedRegions.Add(state_MX_OA);

                            // P
                            ObjectId MX_PU_Id = ObjectId.GenerateNewId();
                            var state_MX_PU = new REGIONS() { RegionId = MX_PU_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Puebla", ShortName = "PU", Enabled = true };
                            seedRegions.Add(state_MX_PU);

                            // Q
                            ObjectId MX_QE_Id = ObjectId.GenerateNewId();
                            var state_MX_QE = new REGIONS() { RegionId = MX_QE_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Querétaro", ShortName = "QE", Enabled = true };
                            seedRegions.Add(state_MX_QE);

                            ObjectId MX_QR_Id = ObjectId.GenerateNewId();
                            var state_MX_QR = new REGIONS() { RegionId = MX_QR_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Quintana Roo", ShortName = "QR", Enabled = true };
                            seedRegions.Add(state_MX_QR);

                            // S
                            ObjectId MX_SL_Id = ObjectId.GenerateNewId();
                            var state_MX_SL = new REGIONS() { RegionId = MX_SL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "San Luis Potosí", ShortName = "SL", Enabled = true };
                            seedRegions.Add(state_MX_SL);

                            ObjectId MX_SI_Id = ObjectId.GenerateNewId();
                            var state_MX_SI = new REGIONS() { RegionId = MX_SI_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Sinaloa", ShortName = "SI", Enabled = true };
                            seedRegions.Add(state_MX_SI);

                            ObjectId MX_SO_Id = ObjectId.GenerateNewId();
                            var state_MX_SO = new REGIONS() { RegionId = MX_SO_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Sonora", ShortName = "SO", Enabled = true };
                            seedRegions.Add(state_MX_SO);

                            // T
                            ObjectId MX_TB_Id = ObjectId.GenerateNewId();
                            var state_MX_TB = new REGIONS() { RegionId = MX_TB_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Tabasco", ShortName = "TB", Enabled = true };
                            seedRegions.Add(state_MX_TB);

                            ObjectId MX_TM_Id = ObjectId.GenerateNewId();
                            var state_MX_TM = new REGIONS() { RegionId = MX_TM_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Tamaulipas", ShortName = "TM", Enabled = true };
                            seedRegions.Add(state_MX_TM);

                            ObjectId MX_TL_Id = ObjectId.GenerateNewId();
                            var state_MX_TL = new REGIONS() { RegionId = MX_TL_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Tlaxcala", ShortName = "TL", Enabled = true };
                            seedRegions.Add(state_MX_TL);

                            // V
                            ObjectId MX_VE_Id = ObjectId.GenerateNewId();
                            var state_MX_VE = new REGIONS() { RegionId = MX_VE_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Veracruz", ShortName = "VE", Enabled = true };
                            seedRegions.Add(state_MX_VE);

                            // Y
                            ObjectId MX_YU_Id = ObjectId.GenerateNewId();
                            var state_MX_YU = new REGIONS() { RegionId = MX_YU_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Yucatán", ShortName = "YU", Enabled = true };
                            seedRegions.Add(state_MX_YU);

                            // Z
                            ObjectId MX_ZA_Id = ObjectId.GenerateNewId();
                            var state_MX_ZA = new REGIONS() { RegionId = MX_ZA_Id.ToString(), CountryId = country.CountryId, CountryCode = country.ShortName, LongName = "Zacatecas", ShortName = "ZA", Enabled = true };
                            seedRegions.Add(state_MX_ZA);

                            break;
                }
            }

            var regionWrites = seedRegions.Select(region =>
                new UpdateOneModel<REGIONS>(
                    Builders<REGIONS>.Filter.And(
                        Builders<REGIONS>.Filter.Eq(item => item.CountryCode, region.CountryCode),
                        Builders<REGIONS>.Filter.Eq(item => item.ShortName, region.ShortName)),
                    Builders<REGIONS>.Update
                        .SetOnInsert(item => item.RegionId, region.RegionId)
                        .SetOnInsert(item => item.Enabled, region.Enabled)
                        .Set(item => item.CountryId, region.CountryId)
                        .Set(item => item.CountryCode, region.CountryCode)
                        .Set(item => item.LongName, region.LongName)
                        .Set(item => item.ShortName, region.ShortName))
                { IsUpsert = true });

            await regions.BulkWriteAsync(regionWrites, new BulkWriteOptions { IsOrdered = false });

            // Rebuild each country's region summary from the persisted records so
            // existing RegionIds remain stable across repeated seed runs.
            foreach (var country in countryList)
            {
                var persistedRegions = await regions
                    .Find(region => region.CountryCode == country.ShortName)
                    .SortBy(region => region.LongName)
                    .ToListAsync();
                var abbreviations = persistedRegions.Select(region => new REGIONS_ABBR
                {
                    Id = region.RegionId,
                    Name = region.LongName,
                    Abbr = region.ShortName
                }).ToList();
                await countries.UpdateOneAsync(
                    item => item.ShortName == country.ShortName,
                    Builders<COUNTRIES>.Update.Set(item => item.Regions, abbreviations));
            }

        }
    }
}
