using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSULS_SYSTEM.Models
{
    public partial class TB_MeetingAgenda
    {
        public int ID { get; set; }
        [StringLength(10)]
        [Display(Name = "Meeting ID")]
        public string Meeting_ID { get; set; }
        [StringLength(10)]
        [Display(Name = "Agenda ID")]
        public string Agenda_ID { get; set; }
        [StringLength(200)]
        [Display(Name = "Agenda Title")]
        public string Agenda_Title { get; set; }
        [StringLength(200)]
        [Display(Name = "Attachment")]
        public string Attachment { get; set; }
        [Display(Name = "Active")]
        public bool? Active { get; set; }
        [StringLength(50)]
        [Display(Name = "Create by")]
        public string Createby { get; set; }
        [Column(TypeName = "datetime")]
        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
    }
}
