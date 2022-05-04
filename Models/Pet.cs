using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    public enum PetBreedType {
        Shepherd, //0
        Poodle, 
        Beagle, 
        Bulldog, 
        Terrier, 
        Boxer, 
        Labrador, 
        Retriever, //7
    }
    public enum PetColorType {
        White, //0
        Black, 
        Golden, 
        Tricolor, 
        Spotted //4
    }
    public class Pet {
        public int id {get; set;}

        [Required]
        public string name {get; set;}

        [Required]
        public PetBreedType breed {get; set;}

        [Required]
        public PetColorType color {get; set;}

        public DateTime? checkedInAt {get; set;}

        [Required]
        [ForeignKey("ownedBy")]
        public int petOwnerId {get; set;}
        public PetOwner ownedBy {get; set;}
    }
}
