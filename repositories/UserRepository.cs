using ExchangeRate.Interface;
using ExchangeRate.middleware;
using ExchangeRate.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.repositories
{
    internal class UserRepository : IUserRepository
    {
        public List<User> _userList = [];
        public readonly string path = @"../../../SaveInfo/userInfo.json";

        private CheckJSONFilesForUser _checkJSONFilesForUser;

        public UserRepository(CheckJSONFilesForUser checkJSONFilesForUser)
        {
            _checkJSONFilesForUser = checkJSONFilesForUser;
        }

        public void Add(User user)
        {
            _userList.Add(user);
            _checkJSONFilesForUser.SaveJSONUser(_userList, path);
        }

        public void Delete(int id)
        {
            _userList.RemoveAll(user => user.Id == id);
            _checkJSONFilesForUser.SaveJSONUser(_userList, path);
        }

        public List<User> GetAll()
        {
            return _userList;
        }

        public User GetById(int id)
        {
            return _userList.FirstOrDefault(user => user.Id == id)!;
        }

        public void Update(User user)
        {
            int index = _userList.FindIndex(u => u.Id == user.Id);
            if (index != -1) { _userList[index] = user; }
            _checkJSONFilesForUser.SaveJSONUser(_userList, path);
        }

        public int GetId()
        {
            int idMax = _userList.Max(u => u.Id);
            return idMax +1;
        }

        public void Load()
        {
            _userList = _checkJSONFilesForUser.LoadJSONUser(path).ToList();
        }

        public void CheckPath()
        {
            _checkJSONFilesForUser.CheckFileForUser(path);
        }
    }
}
