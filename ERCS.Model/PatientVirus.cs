﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Attributes;

namespace ERCS.Model
{
    [MiddleTable] //WTM Framework attribute to define many-to-many relationship

    // Many-to-Many Relationship
    // it is just 2 one-to-many relationships,
    // which requires this 3rd table (middle table) to store the relationship information
    // Patient and Virus tables has a LIST that points to this table
    public class PatientVirus : TopBasePoco
    {
        //[Display(Name = "患者名稱")]
        public Patient Patient { get; set; }
        //[Display(Name = "患者名稱")]
        //[Required(ErrorMessage = "患者名稱是必填欄位")]
        public int? PatientId { get; set; } //Foreign Key (GUID is overrided by integer)

        //[Display(Name = "病毒名稱")]
        public Virus Virus { get; set; }
        //[Display(Name = "病毒名稱")]
        //[Required(ErrorMessage = "病毒名稱是必填欄位")]
        public Guid? VirusId { get; set; } //Foreign Key
    }
}
