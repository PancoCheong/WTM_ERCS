using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    public enum VirusTypeEnum
    {
        DNA, RNA
    }
    public class Virus : TopBasePoco    // a record of the table
    {
        [Display(Name = "病毒名稱")]
        [Required(ErrorMessage = "病毒名稱是必填欄位")]
        public string VirusName { get; set; }   

        [Display(Name = "病毒號碼")]
        [Required(ErrorMessage = "病毒號碼是必填欄位")]
        [StringLength(10, ErrorMessage = "病毒號碼最多10個字符")]    //not only validation, it also affect data field in database: varchar(10)
        public string VirusCode { get; set; }

        [Display(Name = "病毒描述")]
        public string Remark { get; set; }

        [Display(Name = "病毒種類")]
        [Required(ErrorMessage = "病毒種類是必填欄位")]
        public VirusTypeEnum? VirusType { get; set; } //Enum must have value by default

    }
}
