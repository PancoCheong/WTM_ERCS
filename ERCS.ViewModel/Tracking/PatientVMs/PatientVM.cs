using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.PatientVMs
{
    public partial class PatientVM : BaseCRUDVM<Patient>
    {
        // 4. add 2 entities and rename all 3 entities - AllProvinces, AllCities, AllDistricts
        public List<ComboSelectListItem> AllProvinces { get; set; }
        public List<ComboSelectListItem> AllCities { get; set; }
        public List<ComboSelectListItem> AllDistricts { get; set; }

        // backward compatiable with others functions like edit
        public List<ComboSelectListItem> AllLocations { get; set; }

        public List<ComboSelectListItem> AllHospitals { get; set; }
        public List<ComboSelectListItem> AllVirusess { get; set; }
        [Display(Name = "病毒")]
        public List<Guid> SelectedVirusesIDs { get; set; }

        // 2. add ProvinceId and CityId properties for Create.cshtml to reference
        public Guid? ProvinceId { get; set; }
        public Guid? CityId { get; set; }

        public PatientVM()
        {
            SetInclude(x => x.Location);
            SetInclude(x => x.Hospital);
            SetInclude(x => x.Viruses);
        }

        protected override void InitVM()
        {
            // DC.Set - load data from Location table
            // GetSelectListItems - WTM method, 
            //   it return data in ComboSelectListItem format, easier to bind data to checkbox/combobox/transfer/radio etc
            //
            // 1. select only province 1st
            // 4. rename AllLocations to AllProvinces
            AllProvinces = DC.Set<Location>().Where(x=>x.ParentId == null).GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            // 9. preseve previous selection for displaying on UI. A. after input error, B. on edit screen
            if(Entity.LocationId != null) //District ID
            {
                // TODO: how to get all data in less query, 4 database round-trips now
                CityId = DC.Set<Location>().Where(a => a.ID == Entity.LocationId).Select(a => a.ParentId).SingleOrDefault();
                ProvinceId = DC.Set<Location>().Where(a => a.ID == CityId).Select(a => a.ParentId).SingleOrDefault();

                AllCities = DC.Set<Location>().Where(x => x.ParentId == ProvinceId).GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
                AllDistricts = DC.Set<Location>().Where(x => x.ParentId == CityId).GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            }


            AllHospitals = DC.Set<Hospital>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllVirusess = DC.Set<Virus>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.VirusName);
            SelectedVirusesIDs = Entity.Viruses?.Select(x => x.VirusId).ToList();
        }

        public override void DoAdd()
        {
            Entity.Viruses = new List<PatientVirus>();
            if (SelectedVirusesIDs != null)
            {
                foreach (var id in SelectedVirusesIDs)
                {
                    Entity.Viruses.Add(new PatientVirus { VirusId = id });
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            Entity.Viruses = new List<PatientVirus>();
            if(SelectedVirusesIDs != null )
            {
                SelectedVirusesIDs.ForEach(x => Entity.Viruses.Add(new PatientVirus { ID = Guid.NewGuid(), VirusId = x }));
            }

            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
