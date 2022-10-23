using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public enum Colors : int
    {
        Black = 1,
        Silver,
        SpaceBlack,
        SpaceGrey,
        DeepPurple,
        Starlight,
        Green,
        Gold,
        SierraBlue,
        Midnight
    }
    public class Color
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }

        public ICollection<Phone> Phones { get; set; } = new HashSet<Phone>();
    }
}
