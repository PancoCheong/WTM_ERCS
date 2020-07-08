using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    public class ControlCenter : TopBasePoco
    {
        [Display(Name = "中心名稱")]
        [Required(ErrorMessage = "中心名稱是必填欄位")]
        public string Name { get; set; }

        // Location and LocationId are tied together to create a table relationship
        // one-to-many relationship: one City to many Hospital
        [Display(Name = "中心地點")]
        public Location Location { get; set; }      //create relationship with City
        //[Display(Name = "中心地點")]
        //[Required(ErrorMessage = "中心地點是必填欄位")]
        public Guid? LocationId { get; set; }    //Foreign Key
    }
}
