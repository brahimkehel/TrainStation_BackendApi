using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Trajet
    {
        public Trajet()
        {
            Affectations = new HashSet<Affectation>();
        }

        public int Id { get; set; }
        public int? GareDep { get; set; }
        public int? GareArr { get; set; }

        public virtual Gare GareArrNavigation { get; set; }
        public virtual Gare GareDepNavigation { get; set; }
        public virtual ICollection<Affectation> Affectations { get; set; }
    }
}
