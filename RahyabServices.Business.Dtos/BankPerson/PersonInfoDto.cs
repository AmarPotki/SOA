using System.Runtime.Serialization;
using RahyabServices.Common.Dto;
using RahyabServices.Common.Extensions;
namespace RahyabServices.Business.Dtos.BankPerson
{
    [DataContract]
    public class PersonInfoDto : IDto{
        [DataMember]
        public string CodeMahalKhedmat { get; set; }
        [DataMember]
        public string CodeMeli { get; set; }
        [DataMember]
        public string EducationTitle { get; set; }
        [DataMember]
        public short? EmploymentStatusId { get; set; }
        [DataMember]
        public string EmploymentStatusTitle { get; set; }
        [DataMember]
        public string Estekhdam { get; set; }
        [DataMember]
        public string FName { get; set; }
        [DataMember]
        public short? Genseiat { get; set; }
        [DataMember]
        public long? HashCode { get; set; }
        [DataMember]
        public string HisDate { get; set; }
        [DataMember]
        public int? JobCategoryCode { get; set; }
        [DataMember]
        public string JobCategoryTitle { get; set; }
        [DataMember]
        public string LName { get; set; }
        [DataMember]
        public short? MadrakTahsyly { get; set; }
        [DataMember]
        public string MahaleSodooreShenasnameh { get; set; }
        [DataMember]
        public string MahalKhedmat { get; set; }
        [DataMember]
        public string MobileNo { get; set; }
        [DataMember]
        public string NamePedar { get; set; }
        [DataMember]
        public string Neshany { get; set; }
        [DataMember]
        public int? SabeghehBymeh { get; set; }
        [DataMember]
        public string Semat { get; set; }
        [DataMember]
        public int? SematId { get; set; }
        [DataMember]
        public string ShomarehShenasnameh { get; set; }
        [DataMember]
        public int? StaffId { get; set; }
        [DataMember]
        public string StaffPureTitle { get; set; }
        [DataMember]
        public string TarikhEstekhdam { get; set; }
        [DataMember]
        public string TarikhShorooBekar { get; set; }
        [DataMember]
        public string TarykhTavalod { get; set; }
        [DataMember]
        public int? TedadMahKhedmat { get; set; }
        [DataMember]
        public short? TedadOladBarayeHoghoghDastMozd { get; set; }
        [DataMember]
        public int? TedadRouzKhedmat { get; set; }
        [DataMember]
        public short? TedadSalKhedmat { get; set; }
        [DataMember]
        public string TelefonTamas { get; set; }
        [DataMember]
        public short? VazeeyateTaahol { get; set; }
        [DataMember]
        public int? WorkSectionId { get; set; }
        [DataMember]
        public string WorkSectionTitle { get; set; }
        [DataMember]
        public int ShomarehPersenely { get; set; }
        [DataMember]
        public string WorkSectionTitleFormated { get; set; }
    }
}