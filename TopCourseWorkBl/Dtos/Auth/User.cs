using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TopCourseWorkBl.Dtos.Auth
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public Role RoleMetaData { get; set; } = null!;

        [JsonIgnore]
        public string Password { get; set; } = null!;
        [JsonIgnore] 
        public List<RefreshToken> RefreshTokens { get; set; } = null!;
    }
}