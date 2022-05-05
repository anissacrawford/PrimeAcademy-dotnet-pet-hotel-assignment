using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        // [HttpGet]
        // public IEnumerable<PetOwner> GetPets() {
        //     return new List<PetOwner>();
        // }

        [HttpGet]
        public IEnumerable<PetOwner> GetAll()
        {
            return _context.PetOwners;
        }

        [HttpGet("{id}")]
        public ActionResult<PetOwner> GetById(int id)
        {
            PetOwner petOwner = _context.PetOwners
                .SingleOrDefault(petOwner => petOwner.id == id);

            if (petOwner == null)
            {
                return NotFound();
            }
            return petOwner;
        }

        [HttpPost]
        public PetOwner Post(PetOwner newOwner)
        {
            _context.Add(newOwner);
            _context.SaveChanges();

            return newOwner;
        }

        [HttpPut("{id}")]
        public ActionResult<PetOwner> Put(int id, PetOwner petOwnerToChange)
        {
            petOwnerToChange.id = id;

            _context.Update(petOwnerToChange);

            _context.SaveChanges();

            return petOwnerToChange;
            
        }


    }
}
