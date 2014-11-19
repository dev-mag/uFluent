using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace uFluent.Migrate.Persistence
{
    [TableName("MigrationHistory")]
    [PrimaryKey("MigrationId")]
    [ExplicitColumns]
    public class MigrationHistory
    {
        [Column("MigrationId")]
        [PrimaryKeyColumn(Name = "PK_MigrationHistory", AutoIncrement = true, Clustered = true)]
        public int Id { get; set; }

        [Column("Name")]
        [Index(IndexTypes.UniqueNonClustered, Name="IX_MigrationHistory_name")]
        public string Name { get; set; }

        [Column("Timestamp")]
        [Constraint(Default = "GETDATE()")]
        public DateTime Timestamp { get; set; }

        [Column("Completed")]
        [Constraint(Default = "0")]
        public bool Completed { get; set; }
    }
}