using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.BankPerson{
    public class BranchManagerDto : IDto{
        [DataMember]
        public bool IsBranchManager { get; set; }
        [DataMember]
        public string CodeMahalKhedmat { get; set; }
        [DataMember]
        public int ShomarehPersenely { get; set; }

    }
}