using ExchangeRate.Interface;
using ExchangeRate.model;
using ExchangeRate.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.controllers
{
    internal class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public List<User> Get()
        {
            var userList = _userService.GetUsersAsync();
            if(userList == null)
            {
                Console.WriteLine("Lista não encontrada !");
                return null;
            }

            return userList;
        }

        public User Get(int id)
        {
            var user = _userService.GetUserByIdAsync(id);
            return user;
        }

        public int GetQuantity()
        {
            var userList = _userService.GetUsersAsync();
            if (userList == null)
            {
                Console.WriteLine("Lista não encontrada !");
                return 0;
            }

            return userList.Count;
        }

        public void Post(User user)
        {
            _userService.AddAsync(user);
        }

        public void Put(User user, int id)
        {
            if(id != user.Id)
            {
                Console.WriteLine("Usuario não encontrado");
                Console.ReadLine();
            }
            else
            {
                _userService.UpdateAsync(user);
                Console.WriteLine("User atualizado");
            }
        }

        public void Delete(int id)
        {
            _userService.DeleteAsync(id);
            Console.WriteLine("User deletado");
        }

        public void Load()
        {
            _userService.LoadUsers();
        }

        public void CheckPath()
        {
            _userService.CheckPath();
        }
    }
}
