using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.Model
{
    public class PsRoot
    {
        /// <summary>
        /// Guid
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime Created {get;set;}
        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }
       
    }
}
