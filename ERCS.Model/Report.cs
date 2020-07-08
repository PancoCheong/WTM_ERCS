using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    public class Report : TopBasePoco
    {
        [Display(Name = "體溫")]
        [Required(ErrorMessage = "體溫是必填欄位")]
        [Range(30,50, ErrorMessage = "體溫必須是30到50度之間")]
        public float Temperature { get; set; }

        [Display(Name = "備註")]
        public string Remark { get; set; }

        [Display(Name = "患者名稱")]
        public Patient Patient { get; set; }
        //[Display(Name = "患者名稱")]
        //[Required(ErrorMessage = "患者名稱是必填欄位")]
        public int? PatientId { get; set; } //Foreign Key (GUID is overrided by integer)
    }
}
