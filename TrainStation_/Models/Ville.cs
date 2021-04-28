using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Ville
    {
        public Ville()
        {
            Gares = new HashSet<Gare>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Gare> Gares { get; set; }
    }
}
