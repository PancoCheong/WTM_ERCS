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
        public List<ComboSelectListItem> AllLocations { get; set; }
        public List<ComboSelectListItem> AllHospitals { get; set; }
        public List<ComboSelectListItem> AllVirusess { get; set; }
        [Display(Name = "病毒")]
        public List<Guid> SelectedVirusesIDs { get; set; }

        public PatientVM()
        {
            SetInclude(x => x.Location);
            SetInclude(x => x.Hospital);
            SetInclude(x => x.Viruses);
        }

        protected override void InitVM()
        {
            AllLocations = DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
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
