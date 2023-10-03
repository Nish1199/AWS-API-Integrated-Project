using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using libCmpMgmt.Models;
using RestCmpMgmt.Services;
using RestCmpMgmt.DTO;

namespace RestCmpMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompUsersController : ControllerBase
    {
        private readonly CMPMGMTContext _context;
        private readonly IComputerManagment _compContext;
        private readonly IMapper _mapper;
        //public CompUsersController(CMPMGMTContext context, IComputerManagment compContext, IMapper mapper)
        public CompUsersController(IComputerManagment compContext, IMapper mapper)
        {
            //_context = context;
            _compContext = compContext;
            _mapper = mapper;
        }

        // GET: api/CompUsers
        [HttpGet]
        //working
        //Description: this lists all of the users in the system
        public async Task<ActionResult<CompUser>> GetCompUsers()
        {
            var list = await _compContext.ListComputerUsers();//produces a list of computer users
            return Ok(_mapper.Map<IEnumerable<CompUser>>(list));
        }
        //working
        //Description: this lists all of the registered computers
        [HttpGet]
        [Route("/api/Computers")]
        public async Task<ActionResult<Computer>> listComputers()
        {
            var list = await _compContext.getComputers();
            return Ok(_mapper.Map<IEnumerable<ComputersDTO>>(list));
        }
        //working
        //Description: this return true/false depending on if the user is in the system
        [HttpGet("/api/{id}")]
        public async Task<ActionResult<bool>> doesUserExist(string id)
        {
            var compUser = await _compContext.doesUserExist(id);
            if (compUser == false)
            {
                return NotFound();
            }
            return compUser;
        }
        //working
        //Description: returns the user object by id
        [HttpGet]
        [Route("/api/CompUsers/{id}")]
        public async Task<ActionResult<JustUser>> getCompUserById(string id)
        {
            var compUser = await _compContext.getUserById(id);
            var retVal = _mapper.Map<JustUser>(compUser);
            return Ok(retVal);
        }
        //working
        //Description: returns the computer object by Sn
        [HttpGet]
        [Route("/api/Computers/{sn}")]
        public async Task<ActionResult<ComputersDTO>> listComputerById(string sn)
        {
            var comp = await _compContext.getComputerById(sn);
            var retVal = _mapper.Map<ComputersDTO>(comp);
            return Ok(retVal);
        }
        //working
        // PUT: api/CompUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompUser(string id, JustUser compUser)
        {
            var fullUser = _mapper.Map<CompUser>(compUser);
            //check the ids match - removed because there is now way to change the serial number if input wrong
            /*if (id != fullUser.UserId)
            {
                return BadRequest();
            }*/
            //verify that the user exists
            Task<bool> t1 = Task<bool>.Run(() => _compContext.doesUserExist(id));
            bool GoNoGo = t1.Result;
            if(GoNoGo)
            {
                _compContext.setStateModified(fullUser);
                return Ok(await _compContext.saveUpdate(id));
            }
            else
            {
                return NoContent();
            }
        }
        //working
        // PUT: api/Computer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("/api/Computers/{sn}")]
        public async Task<IActionResult> PutComputer(string sn, ComputersDTO comp)
        {
            var fullComp = _mapper.Map<Computer>(comp);
            //check the data that it matches
            if (sn != fullComp.Sn)
            {
                return BadRequest();
            }
            //verify that the user exists
            Task<bool> t1 = Task<bool>.Run(() => _compContext.doesComputerExist(sn));
            bool GoNoGo = t1.Result;
            if (GoNoGo)
            {
                _compContext.setStateModifiedComputer(fullComp);
                return Ok(await _compContext.saveUpdate(sn));
            }
            else
            {
                return NoContent();
            }
        }
        //working
        // POST: api/CompUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompUser>> PostCompUser(JustUser user)
        {
            var fullUser = _mapper.Map<CompUser>(user);
            //Task<int> t1 = Task<int>.Run(() => _compContext.saveCompUser(fullUser));
            
            return await _compContext.saveCompUser(fullUser);
        }
        //working
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/api/Computers")]
        public async Task<ActionResult<Computer>> PostComputer(ComputersDTO comp)
        {
            var fullComp = _mapper.Map<Computer>(comp);
            return await _compContext.saveComputer(fullComp);
        }
        //working
        // DELETE: api/CompUsers/5
        [HttpDelete("{id}")]
        public async Task<int> DeleteCompUser(string id)
        {
            return await _compContext.delCompUser(id);
        }
        //not working
        //DELETE: api/Computer/5
        [HttpDelete]
        [Route("/api/Computers/{id}")]
        public async Task<int> DeleteComputer(string id)
        {
            return await _compContext.delComputer(id);
        }
        private bool CompUserExists(string id)
        {
            return _context.CompUsers.Any(e => e.UserId == id);
        }
    }
}
