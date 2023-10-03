using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using libCmpMgmt.Models;

namespace RestCmpMgmt.Services
{
    public interface IComputerManagment
    {
        Task<bool> doesUserExist(string userId);
        Task<bool> doesComputerExist(string sn);
        Task<IEnumerable<CompUser>> ListComputerUsers();
        Task<IEnumerable<Computer>> getComputers();
        Task<CompUser> getUserById(string id);
        Task<Computer> getComputerById(string compId);
        void setStateModified(CompUser compUser);
        public void setStateModifiedComputer(Computer comp);
        Task<int> saveUpdate(string id);
        Task<CompUser> saveCompUser(CompUser user);
        Task<Computer> saveComputer(Computer comp);
        Task<int> delCompUser(string id);
        Task<int> delComputer(string compId);
        Task<IEnumerable<CompUser>> getFullUser(string id);
    }
}
