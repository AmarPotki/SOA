using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using AutoMapper;
using RahyabServices.Business.Domain.Models.Delinquent;
using RahyabServices.Business.Domain.Models.Delinquent.Log;
using RahyabServices.Business.Domain.Models.State;
using RahyabServices.Business.Dtos.Delinquent.Log;
using RahyabServices.Business.Dtos.Delinquent.Log.Notice;
using RahyabServices.Business.Facades.Interfaces;
using RahyabServices.Common.Cryptography;
using RahyabServices.DataAccess.Repositories.Delinquent.Interfaces;
namespace RahyabServices.Business.Services.State
{
    public class LetterStateHandler : DelinquentState
    {
        private readonly IStateRepository _stateRepository;
        private readonly IHrFacade _hrFacade;
        private readonly ILogBaseRepository _logBaseRepository;
        private readonly ICryptographer _cryptographer;
        public LetterStateHandler(AddNoticeLogDto addNoticeLogDto){
            HistoryCustomerDelinquentId = addNoticeLogDto.CustomerDelinquentId;
            _cryptographer = AutofacHostFactory.Container.Resolve<ICryptographer>();
            _stateRepository = AutofacHostFactory.Container.Resolve<IStateRepository>();
            _hrFacade = AutofacHostFactory.Container.Resolve<IHrFacade>();
            _logBaseRepository = AutofacHostFactory.Container.Resolve<ILogBaseRepository>();
            var personnelCode = _hrFacade.GetPersonnelCode(_cryptographer.Decrypt(addNoticeLogDto.AuthorUserName));
            Task.Run(() => InitializeAsync(addNoticeLogDto, personnelCode)).Wait();
        }
        private async Task InitializeAsync(AddNoticeLogDto addNoticeLogDto, string personnelCode)
        {
            var noticeLog = new NoticeLog
            {
                Author = personnelCode,
                Description = addNoticeLogDto.Description,
                Created = DateTime.Now,
                DocumentUrl = addNoticeLogDto.DocumentUrl,
                LetterDate = addNoticeLogDto.LetterDate,
                LetterNumber = addNoticeLogDto.LetterNumber,
            };

            noticeLog.SetCustomerDelinquentId(addNoticeLogDto.CustomerDelinquentId);
            await _logBaseRepository.SaveAsync(noticeLog);
            var letterState = Mapper.Map<LetterStateHandler, LetterState>(this);
            await _stateRepository.SaveAsync(letterState);
            Id = letterState.Id;
        }
        public Task Initialization { get; private set; }
        public override Task Handler(CustomerDelinquent customerDelinquent){
            throw new System.NotImplementedException();
        }
    }
}