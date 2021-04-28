using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Train
    {
        public Train()
        {
            Affectations = new HashSet<Affectation>();
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public int? NbPlaces { get; set; }

        public virtual ICollection<Affectation> Affectations { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
