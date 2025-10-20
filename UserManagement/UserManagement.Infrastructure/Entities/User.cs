using System;

namespace UserManagement.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public char Gender { get; set; }
    }
}
