using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RahyabServices.Business.Domain.Models.TatCharity;
using RahyabServices.Business.Dtos.TatCharity;
using RahyabServices.Business.Services.Intefaces.TatCharity;
using RahyabServices.Business.SharepointAutoMapper.Models;
using RahyabServices.Common.Convertors;
using RahyabServices.DataAccess.Repositories.TatCharity.Interfaces;
namespace RahyabServices.Business.Services.Implementations.TatCharity{
    public class TatService : ITatService{
        private readonly ITatApplicantListRepository _applicantListRepository;
        private readonly ITatLoanListRepository _tatLoanListRepository;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        private readonly ITatLoanFundsListRepository _tatLoanFundsListRepository;
        private readonly IPortalLoanFundsListRepository _portalLoanFundsListRepository;
        private readonly ITatPensionFundsListRepository _tatPensionFundsListRepository;
        private readonly ITatPensionListRepository _tatPensionListRepository;
        private readonly IPortalPensionFundsListRepository _portalPensionFundsListRepository;
        public TatService(ITatApplicantListRepository applicantListRepository, IDateTimeConvertor dateTimeConvertor, ITatLoanListRepository tatLoanListRepository, ITatLoanFundsListRepository tatLoanFundsListRepository, IPortalLoanFundsListRepository portalLoanFundsListRepository, IPortalPensionFundsListRepository portalPensionFundsListRepository, ITatPensionFundsListRepository pensionFundsListRepository, ITatPensionListRepository tatPensionListRepository){
            _applicantListRepository = applicantListRepository;
            _dateTimeConvertor = dateTimeConvertor;
            _tatLoanListRepository = tatLoanListRepository;
            _tatLoanFundsListRepository = tatLoanFundsListRepository;
            _portalLoanFundsListRepository = portalLoanFundsListRepository;
            _portalPensionFundsListRepository = portalPensionFundsListRepository;
            _tatPensionFundsListRepository = pensionFundsListRepository;
            _tatPensionListRepository = tatPensionListRepository;
        }
        public IEnumerable<TatApplicantDto> GetTatApplicantsByTitle(string title){
            var applicants = _applicantListRepository.GetTatApplicantByTitle(title);
            return applicants.Select(item => new TatApplicantDto
            {
                Id = item.Id,
                Title = item.Title,
                Birthday =DateTime.MinValue== item.Birthday ? "": _dateTimeConvertor.GetPersianDate(item.Birthday),
                FatherTitle = item.FatherTitle,
                FileNo = item.FileNo,
                NationalID = item.NationalID
            }).ToList();
        }
        public IEnumerable<TatApplicantDto> GetTatApplicantsByNationalId(string nid){
            var applicants = _applicantListRepository.GetTatApplicantByNationalId(nid);
            return applicants.Select(item => new TatApplicantDto
            {
                Id = item.Id,
                Title = item.Title,
                Birthday = DateTime.MinValue == item.Birthday ? "" : _dateTimeConvertor.GetPersianDate(item.Birthday),
                FatherTitle = item.FatherTitle,
                FileNo = item.FileNo,
                NationalID = item.NationalID
            }).ToList();
        }
        public IEnumerable<TatApplicantDto> GetTatApplicantsByFileNo(string fn){
            var applicants = _applicantListRepository.GetTatApplicantByFileNo(fn);
            return applicants.Select(item => new TatApplicantDto
            {
                Id = item.Id,
                Title = item.Title,
                Birthday = DateTime.MinValue == item.Birthday ? "" : _dateTimeConvertor.GetPersianDate(item.Birthday),
                FatherTitle = item.FatherTitle,
                FileNo = item.FileNo,
                NationalID = item.NationalID
            }).ToList();
        }
        public IEnumerable<TatLoanDto> GetUserLoans(string applicantId){
            var loans = _tatLoanListRepository.GetApplicantLoans(applicantId);
            return loans.Select(item => new TatLoanDto
            {
                Id = item.Id,
                Price = item.Price,
                InstallmentCount = item.InstallmentCount,
                InstallmentDueDate = DateTime.MinValue == item.InstallmentDueDate ? "" : _dateTimeConvertor.GetPersianDate(item.InstallmentDueDate),
                InstallmentPrice = item.InstallmentPrice,
                InstallmentStartDate = DateTime.MinValue == item.InstallmentStartDate ? "" : _dateTimeConvertor.GetPersianDate(item.InstallmentStartDate),

            }).ToList();
        }
        public int GetPaidLoanFundsCount(string loanId){
            return _tatLoanFundsListRepository.GetPaidLoanFundsCount(loanId);
        }

        public int AddPortalFundLoan(AddPortalFundDtc dtc){
            var loan = Mapper.Map<AddPortalFundDtc, PortalLoanFunds>(dtc);            
            return _portalLoanFundsListRepository.Save(loan);
        }
        public int AddTatFundLoan(AddTatFundDtc dtc){
            var loan = new TatLoanFunds
            {BranchCode = dtc.BranchCode,DocNo = dtc.DocNo,Payer = dtc.Payer,PaymentAmount = dtc.PaymentAmount,PaymentDate = DateTime.Now,BranchName = dtc.BranchName,Applicant = new LookupFieldMapper {ID = dtc.ApplicantId,Value = "متقاضی"},Loan = new LookupFieldMapper {ID = dtc.LoanId,Value = "وام"} };
            return _tatLoanFundsListRepository.Save(loan);
        }
        public IEnumerable<TatPensionDto> GetUserPensions(string applicantId){
            var loans = _tatPensionListRepository.GetApplicantPensions(applicantId);
            return loans.Select(item => new TatPensionDto
            {
                Id = item.Id,
                PensionPrice =item.PensionPrice,
                AccountNo = item.AccountNo,
                PaymentDate = DateTime.MinValue == item.PaymentDate ? "" : _dateTimeConvertor.GetPersianDate(item.PaymentDate),
                PensionCount = item.PensionCount                

            }).ToList();
        }
        public int GetPaidPensionFundsCount(string pensionId){
            return _tatPensionFundsListRepository.GetPaidPensionFundsCount(pensionId);
        }
        public int AddPortalFundPension(AddPortalPensionFundDtc dtc){
            var pension = Mapper.Map<AddPortalPensionFundDtc, PortalPensionFunds>(dtc);
            return _portalPensionFundsListRepository.Save(pension);
        }
        public int AddTatFundPension(AddTatPensionFundDtc dtc){
            var loan = new TatPensionFundsList
            { BranchCode = dtc.BranchCode, DocNo = dtc.DocNo, Payer = dtc.Payer, PaymentAmount = dtc.PaymentAmount, PaymentDate = DateTime.Now, BranchName = dtc.BranchName, Applicant = new LookupFieldMapper { ID = dtc.ApplicantId, Value = "متقاضی" }, Pension = new LookupFieldMapper { ID = dtc.PensionId, Value = "وام" } };
            return _tatPensionFundsListRepository.Save(loan);
        }
    }
}