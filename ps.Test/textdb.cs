namespace ps.Test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class textdb : DbContext
    {
        public textdb()
            : base("name=textdb")
        {
        }

        public virtual DbSet<ps_data_adjust> ps_data_adjust { get; set; }
        public virtual DbSet<ps_sys_user> ps_sys_user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ps_data_adjust>()
                .Property(e => e.IsDeleted)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ps_sys_user>()
                .Property(e => e.gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ps_sys_user>()
                .Property(e => e.IsDeleted)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
