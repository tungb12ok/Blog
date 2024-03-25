using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BussinessLogic
{
    public class UserBusinessLogic
    {
        private FblogContext userRepository;

        public UserBusinessLogic()
        {
            userRepository = new FblogContext();
        }

        public void CreateUser(string name, string email, string password, string phoneNumber, DateTime dateOfBirth, string gender, int fieldId)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Email and password are required.");
            }

            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (!IsValidPassword(password))
            {
                throw new ArgumentException("Password must be at least 8 characters long.");
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            var user = new User
            {
                Name = name,
                Email = email,
                Password = password,
                Phonenumber = phoneNumber,
                Yearofbirth = dateOfBirth.Year,
                Gender = gender,
                FieldId = fieldId,
                RoleId = 3,
                Status = "Active"
            };

            userRepository.Users.Add(user);
            userRepository.SaveChanges();
        }
        public void UpdateUser(int userId, string name, string email, string password, string phoneNumber, DateTime dateOfBirth, string gender, int fieldId)
        {
            var existingUser = userRepository.Users.FirstOrDefault(u => u.Id == userId);

            if (existingUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (!string.IsNullOrWhiteSpace(email) && !IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }
            if (!IsValidPassword(password))
            {
                throw new ArgumentException("Password must be at least 8 characters long.");
            }
            if (!string.IsNullOrWhiteSpace(phoneNumber) && !IsValidPhoneNumber(phoneNumber))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            if (dateOfBirth != default(DateTime))
            {
                existingUser.Yearofbirth = dateOfBirth.Year;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                existingUser.Name = name;
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                existingUser.Email = email;
            }

            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                existingUser.Phonenumber = phoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(gender))
            {
                existingUser.Gender = gender;
            }

            if (fieldId != 0) // Ensure a valid fieldId is provided
            {
                existingUser.FieldId = fieldId;
            }

            userRepository.SaveChanges();
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidPassword(string password)
        {
            return password.Length >= 8;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
        }
    }
}
