using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    [Table("functionalities")]
    public class Functionalities : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("description")]
        public string? Description { get; set; }

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

