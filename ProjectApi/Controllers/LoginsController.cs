﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly LoginContext _context;

        public LoginsController(LoginContext context)
        {
            _context = context;
        }

        [Route("api/[controller]/Get/{paramOne}/{paramTwo}")]
        public string Get(string paramOne, string paramTwo)
        { return $"something{paramOne} and {paramTwo}"; }
        // GET: api/Logins
        [HttpGet]
        public IEnumerable<Login> GetLogin()
        {
            return _context.Login;
        }


        // GET: api/Logins/byName?name=michal
        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetLogin(string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login.Where(x => x.login == name).ToListAsync(); 

            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }
        // GET api/user/firstname/lastname/address
        [HttpGet("{login}/{password}")]
        public string GetQuery(string id, string login, string password)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/c cd C:\\projekt\\ && .\\sterownikbazysqlite.exe login {login} {password}";
            process.StartInfo = startInfo;
            process.Start();
            //Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpResponseStream
            // var response= client.repos.restsharp.restsharp.releases.Get().Result.ToString();
            Console.WriteLine("\n\n\n\n--------odp" + Response.ContentType);
            
            return $"{login}:{password}";
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogin([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return Ok(login);
        }

        // PUT: api/Logins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogin([FromRoute] long id, [FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != login.Id)
            {
                return BadRequest();
            }

            _context.Entry(login).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Logins
        [HttpPost]
        //public async Task<IActionResult> PostLogin([FromBody] Login login)
        public async Task<IActionResult> PostLogin([FromBody] Login user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //string check;
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/c cd C:\\projekt\\ && .\\sterownikbazysqlite.exe login {user.login} {user.password}";
            process.StartInfo = startInfo;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine("--------"+output+"hehehe");
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            process.WaitForExit();
            // check= System.Net.ProcessStartInfo.GetResponse();
            var login = await _context.Login.Where(x => x.login == user.login).ToListAsync();
            _context.Login.Add(user);
            await _context.SaveChangesAsync();
            if (output !=null&& output!="") 
            {
                Console.WriteLine("Zalogowano pomyslnie");
                return Ok(user.login);
                //return CreatedAtAction(nameof(GetLogin), new { id = user.Id }, user);
            }
            else
            {
                return NotFound();
            }
            
            
            

            
            //proc.CloseMainWindow();
            //proc.Close();

            //return CreatedAtAction("GetLogin", new { id = login.Id }, login);
            return CreatedAtAction(nameof(GetLogin), new { id = user.Id }, user);
        }

        // DELETE: api/Logins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogin([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();

            return Ok(login);
        }

        private bool LoginExists(long id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}