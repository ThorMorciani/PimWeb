using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    [Table("Tickets")]
    public class Tickets : BaseModel
    {
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("solution")]
        public string? Solution { get; set; }

        [Column("assignee_id")]
        public long Assignee_Id { get; set; }

        [Column("reporter_id")]
        public long Reporter_Id { get; set; }

        [Column("status_id")]
        public long Status_id { get; set; }

        [Column("root_cause_id")]
        public long Root_Cause_id { get; set; }

        [Column("created_at")]
        public DateTime Created_At { get; set; }

        [Column("updated_at")]
        public DateTime Updated_At { get; set; }
    }
}

