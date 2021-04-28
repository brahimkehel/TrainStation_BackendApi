using System;
using System.Collections.Generic;

#nullable disable

namespace TrainStation_.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public DateTime? DateRes { get; set; }
        public bool? Annulé { get; set; }
        public int? IdPassager { get; set; }
        public int? IdTrain { get; set; }

        public virtual Passager IdPassagerNavigation { get; set; }
        public virtual Train IdTrainNavigation { get; set; }
    }
}
