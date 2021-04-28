using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Gare
    {
        public Gare()
        {
            TrajetGareArrNavigations = new HashSet<Trajet>();
            TrajetGareDepNavigations = new HashSet<Trajet>();
        }

        public int Id { get; set; }
        public int? IdVille { get; set; }
        public string NomGare { get; set; }

        public virtual Ville IdVilleNavigation { get; set; }
        public virtual ICollection<Trajet> TrajetGareArrNavigations { get; set; }
        public virtual ICollection<Trajet> TrajetGareDepNavigations { get; set; }
    }
}
