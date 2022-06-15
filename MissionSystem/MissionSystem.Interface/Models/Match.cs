using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSystem.Interface.Models
{
    [NotMapped]
    public class Match
    {   
         /// <summary>
         /// Primary key for the match
         /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the match
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gadgets are assigned to this match
        /// </summary>
        public ICollection<Gadget> Gadgets { get; set; }
        public int Duration { get; set; }
        public bool IsEnglish { get; set; }
        public Arena Arena { get; set; }
        public string GameTypeName { get; set; }

        [NotMapped]
        public IBaseGame BaseGame { get; set; }


    }
}
