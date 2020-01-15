//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ProjectApi.Models;

//namespace ProjectApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RegistrationsController : ControllerBase
//    {
//        private readonly RegistrationContext _context;

//        public RegistrationsController(RegistrationContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Registrations
//        [HttpGet]
//        public IEnumerable<Registration> GetRegistration()
//        {
//            return _context.Registration;
//        }

//        // GET: api/Registrations/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetRegistration([FromRoute] long id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var registration = await _context.Registration.FindAsync(id);

//            if (registration == null)
//            {
//                return NotFound();
//            }

//            return Ok(registration);
//        }

//        // PUT: api/Registrations/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutRegistration([FromRoute] long id, [FromBody] Registration registration)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != registration.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(registration).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!RegistrationExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Registrations
//        [HttpPost]
//        public async Task<IActionResult> PostRegistration([FromBody] Registration registration)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }


//            var escapedArgs = "adduser " + registration.login + " " + registration.password;

//            //var process = new Process()
//            //{
//            //StartInfo = new ProcessStartInfo
//            //{
//            //   // FileName = "/bin/bash",
//            //    FileName = "cmd C:/Users/Michal/Desktop/projekt/sterownikbazysqlite.exe",
//            //    Arguments = $"-c \"{escapedArgs}\"",
//            //    RedirectStandardOutput = true,
//            //    UseShellExecute = false,
//            //    CreateNoWindow = true,
//            //}

//            //};
//            //process.Start();
//            //string result = process.StandardOutput.ReadToEnd();

//            Process process = new Process();
//            ProcessStartInfo startInfo = new ProcessStartInfo();
//            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
//            startInfo.FileName = "cmd.exe";
//            startInfo.Arguments = $"/c cd C:\\projekt\\ && .\\sterownikbazysqlite.exe adduser {registration.login} {registration.password}";
//            process.StartInfo = startInfo;
//            process.Start();


//            _context.Registration.Add(registration);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetRegistration", new { id = registration.Id }, registration);
//        }

//        // DELETE: api/Registrations/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteRegistration([FromRoute] long id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var registration = await _context.Registration.FindAsync(id);
//            if (registration == null)
//            {
//                return NotFound();
//            }

//            _context.Registration.Remove(registration);
//            await _context.SaveChangesAsync();

//            return Ok(registration);
//        }

//        private bool RegistrationExists(long id)
//        {
//            return _context.Registration.Any(e => e.Id == id);
//        }
//    }
//}