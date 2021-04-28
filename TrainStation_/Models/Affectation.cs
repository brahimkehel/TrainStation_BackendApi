using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Affectation
    {
        public int Id { get; set; }
        public int? IdTrain { get; set; }
        public int? IdTrajet { get; set; }
        public DateTime? DateDep { get; set; }
        public DateTime? DateArr { get; set; }
        public int? HeureDep { get; set; }
        public int? HeureArr { get; set; }

        public virtual Train IdTrainNavigation { get; set; }
        public virtual Trajet IdTrajetNavigation { get; set; }
    }
}
