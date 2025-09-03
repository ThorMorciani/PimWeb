using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace AIssist.Domain.Entities
{
    public class ProfilesFunctionalities : BaseModel
    {
        [Column("profile_id")]
        public long Profile_Id { get; set; }

        [Column("functionality_id")]
        public long Functionality_Id { get; set; }
    }
}

