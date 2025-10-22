using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using CommunityPlant.Domain.Enums;

namespace CommunityPlant.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public EnumTypeUser TypeUser { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public List<Participation> Participations { get; set; } = new List<Participation>();
        public List<Task> AssignedTasks { get; set; } = new List<Task>();

        public void SetPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                Password = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                string hashedPassword = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return Password == hashedPassword;
            }
        }
    }
}