using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WalkingTec.Mvvm.Core;

namespace ERCS.Model
{
    // Self-Reference Table: 
    //     1. District 区 / Country 县 / Autonomous County 自治县 / Country-level City 市, 市辖区 
    //        (eg. 香洲区 /     阳山县 / 连南瑶族自治县            / 英德市, 中山市辖区[石岐])
    // --> 2. Prefecture-level City 地市级城市 (eg. 珠海市)
    // --> 3. Sub-provincial City / Province (eg. 深圳市, 广东省)
    public class Location : TopBasePoco, ITreeData<Location> //Tree Data - Self Reference Table (ParentId --> ID)
    {
        [Display(Name = "名稱")]
        [Required(ErrorMessage = "名稱是必填欄位")]
        public string Name { get; set; }

        // one-to-many child-locations
        public List<Location> Children { get; set; }

        [Display(Name = "父級")]
        public Location Parent { get; set; }
        //[Display(Name = "父級")]
        public Guid? ParentId { get; set; }
    }
}
