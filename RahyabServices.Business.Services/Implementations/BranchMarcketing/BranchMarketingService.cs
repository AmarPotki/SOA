using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Domain.Models.Sharepoint.BranchMarketing;
using RahyabServices.Business.Dtos.Bank;
using RahyabServices.Business.Dtos.BranchMarketing;
using RahyabServices.Business.Services.Intefaces.BranchMarketing;
using RahyabServices.Business.SharepointAutoMapper.Models;
using RahyabServices.Common.Convertors;
using RahyabServices.Common.Exceptions;
using RahyabServices.DataAccess.Repositories.Bank.Interfaces;
using RahyabServices.DataAccess.Repositories.BranchMarketing.Interfaces;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.BranchMarcketing{
    public class BranchMarketingService : IBranchMarketingService{
        private readonly ILastBalRepository _lastBalRepository;
        private readonly IMainRevertCustsRepository _mainRevertCustsRepository;
        private readonly ICommunicationCustomerRepository _communicationCustomerRepository;
        private readonly IDateTimeConvertor _dateTimeConvertor;
        public BranchMarketingService(ILastBalRepository lastBalRepository, IMainRevertCustsRepository mainRevertCustsRepository,ICommunicationCustomerRepository communicationCustomerRepository, IDateTimeConvertor dateTimeConvertor)
        {
            _lastBalRepository = lastBalRepository;
            _mainRevertCustsRepository = mainRevertCustsRepository;
            _communicationCustomerRepository = communicationCustomerRepository;
            _dateTimeConvertor = dateTimeConvertor;
        }
        public async Task<LastBalAcountsDto> GetLastBal(GetLastBalCustomerDto customerDto){
            var lastbal = await _lastBalRepository.GetLastBal(customerDto.CustomerNumber);
            var branchValue = customerDto.BranchAmount;
            var lastbal90 = lastbal*90/100;
           // var lastbal110= lastbal * 110 / 100;
            return new LastBalAcountsDto
            {
                Amount = lastbal,
                IsCorect = branchValue >= lastbal90
            };
            //if (branchValue >= lastbal90)
            //{
            //    return new LastBalAcountsDto
            //    {
            //        Amount = lastbal,
            //        IsCorect = true

            //    };

            //}
            //else
            //{
            //    return new LastBalAcountsDto
            //    {
            //        Amount = lastbal,
            //        IsCorect = false

            //    };
            //}
           
        }

        public async Task<ResutlDeleteItemDto> RemoveItem(GetDeleteItemDtc getDeleteItemDto){
            var id = Convert.ToInt64(getDeleteItemDto.SPListItem);
            var result =
                await
                    _mainRevertCustsRepository.QueryAsync(
                        async f => await f.FirstOrDefaultAsync(x => x.SPListItem == id));
            result.IsDeleted = true;
            await _mainRevertCustsRepository.SaveAsync(result);
            return new ResutlDeleteItemDto
            {
                IsDelete = true,
            };
        }

        public async Task<ResultApproveDtc> ApproveItems(GetApproveItemsDto getApproveItemDto){

            var items = GetApproveItems(getApproveItemDto.CampainId);
            try{
                foreach (var item in items){
                    item.State = 1;
                    _communicationCustomerRepository.Update(item);
                }
                return new ResultApproveDtc
                {
                    Issuccess = true
                };
            }
            catch (Exception e){
                return new ResultApproveDtc
                {
                    Issuccess = false
                };
            }
        }

        public async Task<ResultItemsDto> GetAndSaveItems(){
           
            var items = await _mainRevertCustsRepository.GetItems();
            if (items.Count() != 0){
                foreach (var item in items){
                    var communicationCustomer = new CommunicationCustomer();
                    communicationCustomer.CustomerID = item.CustomerID;
                    //مانده حساب در زمان تعریف مشتری یا کمپین
                    communicationCustomer.AccountBalance = item.AccountBalance !=null ? item.AccountBalance.Value :0.0;
                    communicationCustomer.Branch = item.Branch;
                    communicationCustomer.Rank = item.Rank;
                    communicationCustomer.CustType = item.CustType;
                    communicationCustomer.CustomerName = item.CustomerName;
                    communicationCustomer.CustomerTell = item.CustomerTell;
                    //آخرین موجودی حساب مشتری
                    communicationCustomer.Latestaccountbalance = item.Latestaccountbalance != null ? item.Latestaccountbalance.Value : 0.0;
                    communicationCustomer.Facility = item.Facility != null ? item.Facility.Value : 0.0 ;
                    communicationCustomer.CampainName = new LookupFieldMapper { ID = int.Parse(item.CampainId) };
                    communicationCustomer.RECENCY = item.RECENCY != null ? item.RECENCY.Value : 0.0 ;
                    communicationCustomer.FREQUENCY = item.FREQUENCY != null ? item.FREQUENCY.Value : 0.0;
                    communicationCustomer.CheckingUnit = item.CheckingUnit;
                    communicationCustomer.cashdesk = item.cashdesk!=null ? item.cashdesk.Value:0.0;
                    var itemId = _communicationCustomerRepository.Save(communicationCustomer);
                    item.Main.SPListItem = itemId;
                    await _mainRevertCustsRepository.SaveAsync(item.Main);
                }
                return new ResultItemsDto
                {
                    Issuccess = true,

                };
            }
            else{
                return new ResultItemsDto
                {
                    Issuccess = false,

                };
            }
          




        }

        public async Task<ResultItemsDto> CheckSuccessCustomers(){
            var query = @"<View><Query><Where>
      <And>
         <And>
            <Eq>
               <FieldRef Name='State' />
               <Value Type='Number'>2</Value>
            </Eq>
            <Eq>
               <FieldRef Name='Status' LookupId='True' />
               <Value Type='integer'>5</Value>
            </Eq>
         </And>
         <IsNull>
            <FieldRef Name='NextDayAmount' />
         </IsNull>
      </And>
   </Where></Query></View>";
         
            var items = _communicationCustomerRepository.GetItems(query).ToList();
            if (items != null && items.Any()){
                var actionPersianDate = _dateTimeConvertor.GetPersianDateWithOutSlashAndYear(items[0].ActionDate.Value);
                //string[] actionDates = items.Select(x => x.ActionDate.ToString()).ToArray();
                //ArrayList actionPersianDates=new ArrayList();
                //foreach (var a in actionDates){
                //   var strdate=_dateTimeConvertor.GetPersianDateWithOutSlashAndYear(Convert.ToDateTime(a));
                //    actionPersianDates.Add(strdate);
                //}

                var lastbals =await _lastBalRepository.GetLastBal(items.Select(x => x.CustomerID).ToArray(), actionPersianDate);
                foreach (var item in items) {
                    var lastbal = lastbals.FirstOrDefault(x => x.CustomerNumber == item.CustomerID);
                    var lastbalValue = lastbal.TotalAccountNumber;
                    var branchValue = item.Amount;
                    var lastbal10 = lastbalValue * 10 / 100;
                    var branchValuePluse = lastbalValue + lastbal10;
                    if (branchValue > Convert.ToDouble(branchValuePluse))
                    {
                        var i = _communicationCustomerRepository.GetItemById(Convert.ToInt32(item.Id));
                        i.IsSuccess = false;
                        i.NextDayAmount = Convert.ToDouble(lastbalValue);
                        _communicationCustomerRepository.Update(i);
                    }
                    else
                    {
                        var i = _communicationCustomerRepository.GetItemById(Convert.ToInt32(item.Id));
                        i.IsSuccess = true;
                        i.NextDayAmount = Convert.ToDouble(lastbalValue);
                        _communicationCustomerRepository.Update(i);
                    }
                }
            }
            return new ResultItemsDto();


        }

        private IEnumerable<CommunicationCustomer> GetApproveItems(string campainId){
            var query = @"<View><Query>
<Where>
      <And>
         <And>
            <Eq>
               <FieldRef Name='CampainName' LookupId='True' />
               <Value Type='integer'>"+ campainId + @"</Value>
            </Eq>
            <Eq>
               <FieldRef Name='Isdeleted' />
               <Value Type='Boolean'>0</Value>
            </Eq>
         </And>
         <Eq>
            <FieldRef Name='State' />
            <Value Type='Number'>0</Value>
         </Eq>
      </And>
   </Where>
   
</Query></View>";
            return _communicationCustomerRepository.GetItems(query);
                


        }

    }
}