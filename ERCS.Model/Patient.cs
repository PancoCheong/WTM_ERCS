using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    public enum GenderEnum
    {
        [Display(Name = "男")]     //**key** can set attribute to Enum
        Male,
        [Display(Name = "女")]     
        Female
    }

    public enum PatientStatusEnum
    {
        [Display(Name = "無症狀")]     // can set attribute to Enum
        Asymptomatic,
        [Display(Name = "疑似")]
        Suspected,
        [Display(Name = "確診")]
        Diagnosed,
        [Display(Name = "治癒")]
        Cured,
        [Display(Name = "死亡")]
        Death
    }

    public class Patient : PersistPoco
    {
        // **key**
        // TopBasePoco <-- BasePoco <-- PresistPoco
        // BasePoco has 4 more properties than TopBasePoco: CreateTime, CreateBy, UpdateTime, UpdateBy
        // PersistPoco has 1 more property than BasePoco: IsValid  (avoid physically delete record, just marked invalid)

        [Display(Name = "病人名稱")]
        [Required(ErrorMessage = "病人名稱是必填欄位")]
        public string PatientName { get; set; }

        [Display(Name = "證件號碼")]
        [Required(ErrorMessage = "證件號碼是必填欄位")]
        [RegularExpression("^(\\d{18,18}|\\d{15,15}|\\d{17,17}x)", ErrorMessage = "證件號碼格式不正確")]
        public string IdNumber { get; set; }

        // **key**
        // search baidu: 身份证正则表达式
        // https://blog.csdn.net/weixin_44588757/article/details/89968629
        // omit / at the beginning and the end /

        [Display(Name = "性別")]
        [Required(ErrorMessage = "性別是必填欄位")]

        public GenderEnum? Gender { get; set; }     // ? means optional, it allows UI to show "please select a value" on drop-down list

        [Display(Name = "病人狀態")]
        [Required(ErrorMessage = "病人狀態是必填欄位")]
        public PatientStatusEnum? Status { get; set; }

        [Display(Name = "出生日期")]
        [Required(ErrorMessage = "出生日期是必填欄位")]
        public DateTime? Birthday { get; set; }     // ? means optional, it allows UI to show "please select date" on date picker

        //**key**
        [NotMapped]     // this property will not map to database, only available in code level
        public int Age { get
            {
                return DateTime.Now.Year - Birthday.Value.Year;
            }
        }

        // Location and LocationId are tied together to create a table relationship
        // one-to-many relationship: one City to many Hospital
        public Location Location { get; set; }      //create relationship with City
        [Display(Name = "中心地點")]
        [Required(ErrorMessage = "中心地點是必填欄位")]
        public Guid? LocationId { get; set; }    //Foreign Key is LocationId

        public Hospital Hospital { get; set; }      //create relationship with City
        [Display(Name = "醫院名稱")]
        [Required(ErrorMessage = "醫院名稱是必填欄位")]
        public Guid? HospitalId { get; set; }    //Foreign Key is LocationId


        public FileAttachment Photo { get; set; }      //create relationship with City
        [Display(Name = "相片")]
        public Guid? PhotoId { get; set; }    //Foreign Key is LocationId

    }
}
