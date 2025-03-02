using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CommunityPlant.Domain.Enums;

namespace CommunityPlant.Domain.Entities
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnumTypeUser TypeUser { get; set; }


        public void SetPassword(string password){
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                Password = BitConverter.ToString(bytes).Replace("-", "").ToLower(); // Armazenar como hex string
            }
        }

        // Método para verificar a senha
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