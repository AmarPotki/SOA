using RahyabServices.Common.Dto;
namespace RahyabServices.Business.Dtos.Supplies{
    public class RespondAccountOwnerDto: IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        /// <summary>
        /// nam shakhe hoghoughi
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// no shakhsh
        /// </summary>
        public PartyType PartyType { get; set; }
        public IdentifierType IdentifierType { get; set; }
        public string Identifier { get; set; }
        /// <summary>
        /// shomare sabt baraye afrad hoghooghi , shomare shenasname baraye afrade haghighi
        /// </summary>
        public string IdNum { get; set; }
        /// <summary>
        /// tarikh tavalod baraye afrad haghighi , tarikh sabt sherkat baraye afrad hoghoughi
        /// </summary>
        public int BirthDate { get; set; }
        /// <summary>
        /// code shahr mahale sabt baraye hoghoughi
        /// </summary>
        public string CityCode { get; set; }
        public string UserName { get; set; }
    }
}