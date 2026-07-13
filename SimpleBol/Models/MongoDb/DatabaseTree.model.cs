using System.Collections.Generic;

namespace SimpleBol.Models.MongoDb
{
    public class DatabaseTree
    {
        public List<DatabaseCollection> Collections { get; set; } = null!;
    }

    public class DatabaseCollection
    {
        public string? Name { get; set; }
        public long DocumentCount { get; set; }
        public List<DatabaseDocuments> Documents { get; set; } = null!;
    }

    public class DatabaseDocuments
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}

