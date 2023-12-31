﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("LessonVideo")]
    public partial class LessonVideo : CommonEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("VideoID")]
        public int VideoId { get; set; }
        [MaxLength(200)]
        public string VideoName { get; set; } = null!;
        public string FileVideo { get; set; } = null!;
        [ForeignKey("Lesson")]
        [Column("LessonID")]
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;
    }
}
