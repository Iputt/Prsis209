using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps.module.Model
{
    /// <summary>
    /// 调剂信息表
    /// </summary>
    [Table("ps_data_adjust")]
    public class Adjust: PsRoot
    {
        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// 学校
        /// </summary>
        [MaxLength(128)]
        public string College { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        [MaxLength(128)]
        public string Major { get; set; }

        /// <summary>
        /// 学习方式
        /// </summary>
        [MaxLength(128)]
        public string LearnStyle { get; set; }

        /// <summary>
        /// 招生人数
        /// </summary>
        public int Enrolment { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? Released{ get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        [MaxLength(128)]
        public string ContactType { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [MaxLength(512)]
        public string Content { get; set; }

    }
}
