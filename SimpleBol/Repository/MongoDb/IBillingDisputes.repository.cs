using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBol.Classes.Errors;
using SimpleBol.Context.MongoDb;
using SimpleBol.Models.MongoDb;


namespace SimpleBol.Repository.MongoDb
{
    public interface IBillingDisputesRepository
    {
        Task<List<BILLINGDISPUTES>> GetAllBillingDisputesAsync();
        Task<List<BILLINGDISPUTES>> GetAllBillingDisputesByShipperAsync(string shipperId);
        Task<BILLINGDISPUTES?> GetOneBillingDisputeAsync(string disputeId);
        Task<bool> AddBillingDisputeAsync(BILLINGDISPUTES dispute);
        Task<bool> UpdateBillingDisputeAsync(BILLINGDISPUTES dispute, string disputeId);
        Task<bool> RemoveBillingDisputeAsync(string disputeId);
    }

    public class BillingDisputeRepository : IBillingDisputesRepository
    {
        private readonly MongoDbContext _context;

        public BillingDisputeRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<BILLINGDISPUTES>> GetAllBillingDisputesAsync()
        {
            try
            {
                return await _context.BillingDisputes.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetAllBillingDisputesAsync");
                throw;
            }

        }

        public async Task<List<BILLINGDISPUTES>> GetAllBillingDisputesByShipperAsync(string shipperId)
        {
            return await _context.BillingDisputes.Find(sid => sid.ShipperId == shipperId).ToListAsync();
        }

        public async Task<BILLINGDISPUTES?> GetOneBillingDisputeAsync(string disputeId)
        {
            try
            {
                return await _context.BillingDisputes.Find(dispute => dispute.BillingDisputeId == disputeId).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_GetOneBillingDisputeAsync");
                throw;
            }
        }

        public async Task<bool> AddBillingDisputeAsync(BILLINGDISPUTES dispute)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(dispute);
                dispute.BillingDisputeId = string.IsNullOrWhiteSpace(dispute.BillingDisputeId)
                    ? ObjectId.GenerateNewId().ToString()
                    : dispute.BillingDisputeId;
                dispute.CreatedOnUtc = dispute.CreatedOnUtc == default
                    ? DateTime.UtcNow
                    : dispute.CreatedOnUtc;
                dispute.UpdatedOnUtc = DateTime.UtcNow;
                await _context.BillingDisputes.InsertOneAsync(dispute);
                return true;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_AddBillingDisputeAsync");
                throw;                
            }

        }

        public async Task<bool> UpdateBillingDisputeAsync(BILLINGDISPUTES dispute, string disputeId)
        {
            try
            {
                var filter = Builders<BILLINGDISPUTES>.Filter.Eq(s => s.BillingDisputeId, disputeId);
                var updateFilter = Builders<BILLINGDISPUTES>.Update
                    .Set(s => s.DisputeDate, dispute.DisputeDate)
                    .Set(s => s.Name, dispute.Name)
                    .Set(s => s.Subject, dispute.Subject)
                    .Set(s => s.Argument, dispute.Argument)
                    .Set(s => s.Remarks, dispute.Remarks)
                    .Set(s => s.Resolved, dispute.Resolved)
                    .Set(s => s.Resolution, dispute.Resolution)
                    .Set(s => s.ResolutionDate, dispute.ResolutionDate)
                    .Set(s => s.QuotedPrice, dispute.QuotedPrice)
                    .Set(s => s.ActualPrice, dispute.ActualPrice)
                    .Set(s => s.CreditedAmount, dispute.CreditedAmount)
                    .Set(s => s.AdjustedPrice, dispute.AdjustedPrice)
                    .Set(s => s.ShipperId, dispute.ShipperId)
                    .Set(s => s.ShipperName, dispute.ShipperName)
                    .Set(s => s.ShipperInvoiceNumber, dispute.ShipperInvoiceNumber)
                    .Set(s => s.ShipperQuoteNumber, dispute.ShipperQuoteNumber)
                    .Set(s => s.ShipperReferenceNumber, dispute.ShipperReferenceNumber)
                    .Set(s => s.ShipperShipDate, dispute.ShipperShipDate)
                    .Set(s => s.ShipperDeliveryDate, dispute.ShipperDeliveryDate)
                    .Set(s => s.Shipper, dispute.Shipper)
                    .Set(s => s.ShipperContactId, dispute.ShipperContactId)
                    .Set(s => s.ShipperContacts, dispute.ShipperContacts)
                    .Set(s => s.OrderNumber, dispute.OrderNumber)
                    .Set(s => s.DisputedBolId, dispute.DisputedBolId)
                    .Set(s => s.DisputedBol, dispute.DisputedBol)
                    .Set(s => s.CustomerId, dispute.CustomerId)
                    .Set(s => s.Customer, dispute.Customer)
                    .Set(s => s.UpdatedOnUtc, DateTime.UtcNow)
                    .Set(s => s.MarkAsDeleted, dispute.MarkAsDeleted);

                UpdateResult actionResult
                    = await _context.BillingDisputes.UpdateOneAsync(filter, updateFilter);

                

                return actionResult.IsAcknowledged
                    && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_UpdateBillingDisputeAsync");
                throw;
            }
        }

        public async Task<bool> RemoveBillingDisputeAsync(string disputeId)
        {
            try
            {
                var filter = Builders<BILLINGDISPUTES>.Filter.Eq(
                    dispute => dispute.BillingDisputeId, disputeId);
                var update = Builders<BILLINGDISPUTES>.Update
                    .Set(dispute => dispute.MarkAsDeleted, true)
                    .Set(dispute => dispute.UpdatedOnUtc, DateTime.UtcNow);
                UpdateResult actionResult = await _context.BillingDisputes
                    .UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                        && actionResult.MatchedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                ErrorLogging.NLogException(ex, "MongoDb_RemoveBillingDisputeAsync");
                throw;
            }
        }


    }
}
