using JobSystemApp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace UserManagementApp
{
    public class User : IUser
    {
        private string? loginUsername;
        private string? loginPassword;



        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public void SayHello(User user)
        {
            Console.WriteLine("Hello "+ user.Username);
        }
    }

}
