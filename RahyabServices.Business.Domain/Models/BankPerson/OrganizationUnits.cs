using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RahyabServices.Business.Domain.Models.BankPerson{
    [Table("ORGANIZATION_UNITS")]
    public class OrganizationUnits:IBankPersonEntity{
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        [Column("IS_ACTIVE")]
        public string IsActive { get; set; }
        [Column("UNIT_TYPE")]
        public string UnitType { get; set; }

        long IBankPersonEntity.Id
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}