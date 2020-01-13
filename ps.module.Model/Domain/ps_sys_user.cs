using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.Model
{
    [Table("ps_sys_user")]
    public class User : PsRoot
    {
        /// <summary>
        /// 头像链接
        /// </summary>
        [MaxLength(256)]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        [MaxLength(128)]
        public string City { get; set; }

        /// <summary>
        /// 所在国家
        /// </summary>
        [MaxLength(128)]
        public string Country { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Char Gender { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        [MaxLength(128)]
        public string Lang { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(128)]
        public string NickName { get; set; }

        /// <summary>
        /// 所在省份
        /// </summary>
        [MaxLength(128)]
        public string Province { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(256)]
        public string Remark { get; set; }
    }
}
