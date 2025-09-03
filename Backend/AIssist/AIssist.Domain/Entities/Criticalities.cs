using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    [Table("criticalities")]
    public class Criticalities : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("criticality")]
        public string? Criticality { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }
    }
}

