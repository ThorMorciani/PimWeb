using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    public class Logs : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("action")]
        public string? Action { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }
    }
}

