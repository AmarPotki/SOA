using System.Linq;
using System.Threading.Tasks;
using RahyabServices.Business.Dtos.Ebanking;
using RahyabServices.Business.Services.Intefaces.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.Business.Services.Implementations.Sharepoint{
    public class EbankingService : IEbankingService{
        private readonly IThursdayRepository _thursdayRepository;
        private readonly IThursdayShiftRepository _thursdayShiftRepository;
        public EbankingService(IThursdayShiftRepository thursdayShiftRepository, IThursdayRepository thursdayRepository){
            _thursdayShiftRepository = thursdayShiftRepository;
            _thursdayRepository = thursdayRepository;
        }
        public async Task<string> GetThursdayShift(GetThursdayShiftDtq dtq){
            //var query = $"<View><Query><Where><And><Eq><FieldRef Name='Year' LookupId='TRUE' />" +
            //            $"<Value Type='Lookup'>{dtq.YearId}</Value>" +
            //            $"</Eq><And><Eq><FieldRef Name='Month'  LookupId='TRUE' />" +
            //            $"<Value Type='Lookup'>{dtq.MonthId}</Value></Eq>" +
            //            $"<Eq><FieldRef Name='Status'  LookupId='TRUE' />" +
            //            $"<Value Type='Lookup'>1</Value></Eq></And></And></Where></Query></View>";
            var query =
                $"<View><Query>   <Where><And><And><And><Eq><FieldRef Name='Year' LookupId='True' />" +
                $"<Value Type='Lookup'>{dtq.YearId}</Value></Eq><Eq><FieldRef Name='Month' LookupId='True' />" +
                $"<Value Type='Lookup'>{dtq.MonthId}</Value></Eq></And><Eq><FieldRef Name='Status' LookupId='True' />" +
                $"<Value Type='Lookup'>1</Value></Eq></And><Eq><FieldRef Name='WorkSectionID' />" +
                $"<Value Type='Number'>{dtq.WorkSectionId}</Value></Eq></And></Where></Query></View>";
            var items = _thursdayShiftRepository.GetItems(query);
            var queryDays = $"<View><Query><Where><And><Eq><FieldRef Name='Month' LookupId='True' />" +
                            $"<Value Type='Lookup'>{dtq.MonthId}</Value></Eq><Eq><FieldRef Name='Year' LookupId='True' />" +
                            $"<Value Type='Lookup'>{dtq.YearId}</Value></Eq></And></Where></Query></View>";
            var days = _thursdayRepository.GetItems(queryDays);
            var lst = (from ts in items
                from value in ts.Day.Values
                select new ThursDayShiftDto
                {
                    Day = value,
                    FullName = ts.Name + " " + ts.Family,
                    IsExist = true,
                    Id = ts.Id.Value,
                    Unit = ts.Unit,
                    PersonnelCode = ts.PersonnelCode
                }).ToList();
            var grs = lst
                .GroupBy(x => x.PersonnelCode)
                .Select(g => new
                {
                    PersonnelCode = g.Key,
                    g.FirstOrDefault(x => x.PersonnelCode == g.Key).FullName,
                    g.FirstOrDefault(x => x.PersonnelCode == g.Key).Id,
                    g.FirstOrDefault(x => x.PersonnelCode == g.Key).Unit,
                    Values = g.Select(t => new
                    {
                        t.Day,
                        t.IsExist
                    })
                });
            lst.AddRange(from day in days
                from gr in grs
                where gr.Values.All(x => x.Day != day.Title)
                select new ThursDayShiftDto
                {
                    Day = day.Title,
                    FullName = gr.FullName,
                    IsExist = false,
                    Id = gr.Id,
                    Unit = gr.Unit,
                    PersonnelCode = gr.PersonnelCode
                });
            var grsLst = lst.OrderBy(x => x.PersonnelCode)
                .GroupBy(x => x.PersonnelCode)
                .Select(g => new
                {
                    PersonnelCode = g.Key,
                    Description = g.FirstOrDefault(x => x.PersonnelCode == g.Key).FullName,
                    g.FirstOrDefault(x => x.PersonnelCode == g.Key).Id,
                    g.FirstOrDefault(x => x.PersonnelCode == g.Key).Unit,
                    Values = g.Select(t => new
                    {
                        t.Day,
                        t.IsExist
                    })
                });
            var str = "{\"data\":[";
            foreach (var dr in grsLst){
                str += "{\"نام کامل\":\"" + dr.Description + "\",";
                str += "\"Id\":\"" + dr.Id + "\",";
                str += "\"واحد\":\"" + dr.Unit + "\",";
                var values = dr.Values.OrderBy(f => f.Day);
                str = values.Aggregate(str,
                    (current, value) => current + ("\"" + value.Day + "\":" + "\"" + value.IsExist) + "\",");
                str = str.Remove(str.Length - 1);
                str += "},";
            }
            str = str.Remove(str.Length - 1);
            str += "]}";
            return str;
        }
    }
}