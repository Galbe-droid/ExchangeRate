using ExchangeRate.Interface;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetUsersAsync()
        {
            return _userRepository.GetAll();
        }

        public User GetUserByIdAsync(int id)
        {
            return _userRepository.GetById(id);
        }

        public void AddAsync(User user)
        {
            _userRepository.Add(user);
        }

        public void UpdateAsync(User user)
        {
            _userRepository.Update(user);
        }

        public void DeleteAsync(int id)
        {
            _userRepository.Delete(id);
        }

        public void LoadUsers()
        {
            _userRepository.Load();

        }

        public void CheckPath()
        {
            _userRepository.CheckPath();
        }
    }
}
