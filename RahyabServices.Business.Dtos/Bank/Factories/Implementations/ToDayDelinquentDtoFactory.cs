using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Bank.Factories.Intefaces;

namespace RahyabServices.Business.Dtos.Bank.Factories.Implementations
{
    public class ToDayDelinquentDtoFactory : IToDayDelinquentDtoFactory
    {
        public ToDayDelinquentDto Create(string branchCode, string branchName)
        {
            return new ToDayDelinquentDto
            {
                BranchCode = branchCode,
                BranchName = branchName
            };
        }
    }
}
