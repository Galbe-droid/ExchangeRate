using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Interface
{
    internal interface IUserService
    {
        List<User> GetUsersAsync();
        User GetUserByIdAsync(int id);
        void AddAsync(User user);
        void UpdateAsync(User user);
        void DeleteAsync(int id);
        void LoadUsers();
        void CheckPath();
    }
}
