namespace ps.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ps_sys_user
    {
        public Guid Id { get; set; }

        [StringLength(256)]
        public string avatarUrl { get; set; }

        [StringLength(128)]
        public string city { get; set; }

        [StringLength(128)]
        public string country { get; set; }

        [StringLength(10)]
        public string gender { get; set; }

        [StringLength(1)]
        public string lang { get; set; }

        [StringLength(128)]
        public string nickName { get; set; }

        [StringLength(128)]
        public string province { get; set; }

        [StringLength(128)]
        public string remark { get; set; }

        public DateTime? Created { get; set; }

        [StringLength(10)]
        public string IsDeleted { get; set; }
    }
}
