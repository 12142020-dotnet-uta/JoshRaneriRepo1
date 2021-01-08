using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.ViewModels;

namespace LogicLayer
{
    public class LogicClass
    {
        private readonly Repo _repo;
        public LogicClass(Repo repo)
        {
            _repo = repo;
        }

        //public UserViewModel LoginUser(LoginUserViewModel loginUserViewModel)
        //{
        //    UserViewModel uvm = new UserViewModel();
        //    return uvm;
        //}
    }
}
