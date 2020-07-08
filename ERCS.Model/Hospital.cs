using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    public enum HospitalLevel
    {
        [Display(Name = "三級醫院")]    //**key** can set attribute to Enum
        Class3,
        [Display(Name = "二級醫院")]
        Class2,
        [Display(Name = "一級醫院")]
        Class1
    }
    public class Hospital : TopBasePoco
    {
        [Display(Name = "醫院名稱")]
        [Required(ErrorMessage = "醫院名稱是必填欄位")]
        public string Name { get; set; }

        [Display(Name = "醫院級別")]
        [Required(ErrorMessage = "醫院級別是必填欄位")]
        public HospitalLevel? Level { get; set; }   // ? means optional, it allows UI to show "please select a value" on drop-down list

        // Location and LocationId are tied together to create a table relationship
        // one-to-many relationship: one City to many Hospital
        [Display(Name = "醫院地點")]
        public Location Location { get; set; }      //create relationship with City
        //[Display(Name = "醫院地點")]
        //[Required(ErrorMessage = "醫院地點是必填欄位")]
        public Guid? LocationId { get; set; }    //Foreign Key
    }
}
