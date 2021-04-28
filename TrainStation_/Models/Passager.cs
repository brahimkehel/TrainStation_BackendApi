using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Passager
    {
        public Passager()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Cin { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
