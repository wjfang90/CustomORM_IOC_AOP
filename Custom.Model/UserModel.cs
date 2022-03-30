using Custom.Framework.Mapping;
using Custom.Framework.Validate;
using System;

namespace Custom.Model
{
    [CustomTableName("User")]
    public class UserModel : BaseModel
    {
        [CustomRequired]
        public string Name { get; set; }
        [CustomRequired]
        [CustomLength(5,20)]
        public string Account { get; set; }
        public string Password { get; set; }

        [CustomEmail]
        public string Email { get; set; }
        [CustomMobile]
        public string Mobile { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        
        [CustomRequired]
        [CustomIntContains(1,2,4,8)]
        [CustomColumnName("State")]
        public int Status { get; set; }
        [CustomIntRange(0,6)]
        public int UserType { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatorId { get; set; }
        public int LastModifierId { get; set; }
        public DateTime LastModifyTime { get; set; }
    }
}
