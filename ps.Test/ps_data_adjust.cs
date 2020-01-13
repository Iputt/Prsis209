namespace ps.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ps_data_adjust
    {
        public Guid Id { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(256)]
        public string p_title { get; set; }

        [StringLength(256)]
        public string p_college { get; set; }

        [StringLength(256)]
        public string p_major { get; set; }

        [StringLength(256)]
        public string p_learnStyle { get; set; }

        public int? p_enrolment { get; set; }

        public DateTime? p_releaseTime { get; set; }

        public DateTime? p_contactMode { get; set; }

        [StringLength(256)]
        public string p_content { get; set; }

        [StringLength(1024)]
        public string p_spare { get; set; }

        [StringLength(10)]
        public string IsDeleted { get; set; }
    }
}
