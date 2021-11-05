using System;
using System.Text.Json.Serialization;

namespace TopCourseWorkBl.Dtos.Auth
{

    public class RefreshToken
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; } 
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public DateTime CreatedAt { get; set; }
        public string CreatedByIp { get; set; }  = null!;
        public DateTime? RevokedAt { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsActive => RevokedAt == null && !IsExpired;
    }
}