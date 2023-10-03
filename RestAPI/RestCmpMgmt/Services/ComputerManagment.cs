using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libCmpMgmt.Models;
using RestCmpMgmt.Services;
using RestCmpMgmt.DTO;


namespace RestCmpMgmt.Services
{
    public class ComputerManagment : IComputerManagment
    {
        private CMPMGMTContext _context;
        public ComputerManagment(CMPMGMTContext context)
        {
            _context = context;
        }
        public async Task<bool> doesUserExist(string userId)
        {
            return await _context.CompUsers.AnyAsync<CompUser>(c => c.UserId == userId);
        }
        public async Task<bool> doesComputerExist(string sn)
        {
            return await _context.Computers.AnyAsync<Computer>(c => c.Sn == sn);
        }
        public async Task<IEnumerable<CompUser>> ListComputerUsers()
        {
            return await _context.CompUsers.ToListAsync<CompUser>();
        }
        public async Task<IEnumerable<Computer>> getComputers()
        {
            return await _context.Computers.ToListAsync<Computer>();
        }
        public async Task<CompUser> getUserById(string id)
        {
            IQueryable<CompUser> retVal = _context.CompUsers.Where(c => c.UserId == id);
            return await retVal.FirstOrDefaultAsync<CompUser>();
        }
        public async Task<Computer> getComputerById(string sn)
        {
            IQueryable<Computer> retVal = _context.Computers.Where(c => c.Sn == sn);
            return await retVal.FirstOrDefaultAsync<Computer>();
        }
        public void setStateModified(CompUser compUser)
        {
            _context.Entry(compUser).State = EntityState.Modified;
        }
        public void setStateModifiedComputer(Computer comp)
        {
            _context.Entry(comp).State = EntityState.Modified;
        }
        public async Task<int> saveUpdate(string id)
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                Task<bool> t1 = Task<bool>.Run(() => _context.CompUsers.AnyAsync<CompUser>(c => c.UserId == id));
                if (!t1.Result)
                {
                    return -1;
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<CompUser> saveCompUser(CompUser user)
        {
            _context.CompUsers.Add(user);
            try
            {
                Task<int> t1 = Task<int>.Run(() => _context.SaveChangesAsync());
                if (t1.Result == 1)
                {
                    return await this.getUserById(user.UserId);
                }
                else
                {
                    return new CompUser();
                }
                //return CreatedAtAction("GetCompUser", new { id = compUser.UserId }, compUser);
            }
            catch (DbUpdateException)
            {
                Task<bool> t1 = Task<bool>.Run(() => _context.CompUsers.AnyAsync<CompUser>(c => c.UserId == user.UserId));
                if (!t1.Result)
                {
                    return new CompUser();
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<Computer> saveComputer(Computer comp)
        {
            _context.Computers.Add(comp);
            try
            {
                Task<int> t1 = Task<int>.Run(() => _context.SaveChangesAsync());
                if (t1.Result == 1)
                {
                    return await this.getComputerById(comp.Sn);
                }
                else
                {
                    return new Computer();
                }
                //return CreatedAtAction("GetCompUser", new { id = compUser.UserId }, compUser);
            }
            catch (DbUpdateException)
            {
                Task<bool> t1 = Task<bool>.Run(() => _context.Computers.AnyAsync<Computer>(c => c.Sn == comp.Sn));
                if (!t1.Result)
                {
                    return new Computer();
                }
                else
                {
                    throw;
                }
            }
        }
        public async Task<int> delCompUser(string userId)
        {
            var compUser = await _context.CompUsers.FindAsync(userId);
            //Task<IEnumerable<CompUser>> t1 = Task<IEnumerable<CompUser>>.Run(() => this.getFullUser(userId));
            //var list = t1.Result;
            foreach(var a in compUser.Computers)
            {
                if(a.UserId.Equals(userId))
                {
                    //set foreign key to null
                    this.setStateModifiedComputer(a);
                    Task<int> t2 = Task<int>.Run(() => this.saveUpdate(a.Sn));
                    if(t2.Result == 1)
                    {
                        continue;
                    }
                    else
                    {
                        
                    }
                }
            }    
            if (compUser == null)
            {
                return -1;
            }

            _context.CompUsers.Remove(compUser);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> delComputer(string compId)
        {
            var computer = await _context.Computers.FindAsync(compId);
            //Task<IEnumerable<CompUser>> t1 = Task<IEnumerable<CompUser>>.Run(() => this.getFullUser(userId));
            //var list = t1.Result;
            /*foreach (var a in compUser.Computers)
            {
                if (a.UserId.Equals(userId))
                {
                    //set foreign key to null
                    this.setStateModifiedComputer(a);
                    Task<int> t2 = Task<int>.Run(() => this.saveUpdate(a.Sn));
                    if (t2.Result == 1)
                    {
                        continue;
                    }
                    else
                    {

                    }
                }
            }*/
            if (computer == null)
            {
                return -1;
            }

            _context.Computers.Remove(computer);
            return await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<CompUser>> getFullUser(string id)
        {
            //either the database is broken or you need to include the rest of the function to get the sub table
            return await _context.CompUsers.ToListAsync<CompUser>();
        }
    }
}
