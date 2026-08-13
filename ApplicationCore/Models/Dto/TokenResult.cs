

using System;

namespace ApplicationCore.Models.Dto
{
    public class TokenResultDto
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public int Status { get; set; }
        public string Token { get; set; }
        public UserProfile Profile { get; set; }

    }
}
