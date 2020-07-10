using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Data.VirusVMs
{
    public partial class VirusVM : BaseCRUDVM<Virus>
    {
        public List<ComboSelectListItem> AllPatientss { get; set; }
        [Display(Name = "患者")]
        public List<int> SelectedPatientsIDs { get; set; }

        public VirusVM()
        {
            SetInclude(x => x.Patients);
        }

        protected override void InitVM()
        {
            AllPatientss = DC.Set<Patient>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PatientName);
            SelectedPatientsIDs = Entity.Patients?.Select(x => x.PatientId).ToList();
        }

        public override void DoAdd()
        {
            Entity.Patients = new List<PatientVirus>();
            if (SelectedPatientsIDs != null)
            {
                foreach (var id in SelectedPatientsIDs)
                {
                    Entity.Patients.Add(new PatientVirus { PatientId = id });
                }
            }
           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            Entity.Patients = new List<PatientVirus>();
            if(SelectedPatientsIDs != null )
            {
                SelectedPatientsIDs.ForEach(x => Entity.Patients.Add(new PatientVirus { ID = Guid.NewGuid(), PatientId = x }));
            }

            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
