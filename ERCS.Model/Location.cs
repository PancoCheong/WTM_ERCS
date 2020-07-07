using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    public class Location : TopBasePoco, ITreeData<Location>
    {
        [Display(Name = "名稱")]
        [Required(ErrorMessage = "名稱是必填欄位")]
        public string Name { get; set; }

        public List<Location> Children { get; set; }

        public Location Parent { get; set; }
        [Display(Name = "父級")]
        public Guid? ParentId { get; set; }
    }
}
