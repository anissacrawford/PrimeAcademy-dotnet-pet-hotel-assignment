using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        // [HttpGet]
        // public IEnumerable<Pet> GetPets() {
        //     return new List<Pet>();
        // }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

            // Pet newPet2 = new Pet {
            //     name = "Little Dog",
            //     petOwner = blaine,
            //     color = PetColorType.Golden,
            //     breed = PetBreedType.Labrador,
            // };

        //     return new List<Pet>{ newPet1, newPet2};
        // }

        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            return _context.Pets 

            .Include(pet => pet.ownedBy);
        }


        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id) {
            Pet pet = _context.Pets
                .SingleOrDefault(pet => pet.id == id);

                if (pet == null) {
                    return NotFound();
                }
                return pet;
        }

        [HttpPost]
        public IActionResult Post(Pet newPet) {
            _context.Add(newPet);
            _context.SaveChanges();

            return Created("/api/Pets", newPet);
        }

        [HttpPut("{id}")]

        public Pet Put(int id, Pet PetToChange){
            PetToChange.id = id; 

             _context.Update(PetToChange);

             _context.SaveChanges();
            
            return PetToChange;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            Pet petToChange = _context.Pets.Find(id);

            _context.Pets.Remove(petToChange);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/checkin")]
        public Pet PutCheckIn(int id) {

            Pet petToCheckIn = _context.Pets.SingleOrDefault(pet => pet.id == id);
            
            petToCheckIn.checkedInAt = DateTime.Now;

            _context.Pets.Update(petToCheckIn);

            _context.SaveChanges();

            return petToCheckIn;
        }

        [HttpPut("{id}/checkout")]
        public Pet PutCheckOut(int id) {

            Pet petToCheckIn = _context.Pets.SingleOrDefault(pet => pet.id == id);
            
            petToCheckIn.checkedInAt = null;

            _context.Pets.Update(petToCheckIn);

            _context.SaveChanges();

            return petToCheckIn;
        }

    }
}
