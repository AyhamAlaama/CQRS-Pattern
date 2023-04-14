using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Domain.IdentityModels.ExtendedUser;

    public class RefreshToken
    {
        [Key]
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; } 
        public bool IsRevoked { get; set; } 
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
