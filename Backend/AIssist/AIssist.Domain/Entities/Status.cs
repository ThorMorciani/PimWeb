using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    [Table("status")]
    public class Status : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("status")]
        public string? Status_name { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }
    }
}

