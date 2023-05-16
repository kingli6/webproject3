using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace College_API.Models
{
    public class Education
    {
        public int Id { get; set; }
        public int EducationNumber { get; set; } //four digit. rnd generated?
        public string? ImagePath { get; set; }
        public string? ImageUrl { get; set; }
        public string? EducationName { get; set; }
        public string? Duration { get; set; }
        // public Category? Category { get; set; }
        public string? Description { get; set; }
        // Beskrivning (är en kortare övergripande beskrivning om kursen och varför man ska gå den)
        // Detaljer (här finns information om vilka moduler/delar kursen går igenom)
        public string? Details { get; set; }
    }
}