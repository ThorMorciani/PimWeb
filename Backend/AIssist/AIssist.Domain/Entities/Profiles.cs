using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    [Table("profiles")]
    public class Profiles : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("profile")]
        public string? Profile { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("created_by")]
        public string? Created_By { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }

        [Column("updated_at")]
        public DateTime Updated_At { get; set; }

        [Column("updated_by")]
        public string? Updated_By { get; set; }
    }
}

