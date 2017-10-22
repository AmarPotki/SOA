using System.Collections.Generic;
using System.Web;
using RahyabServices.Business.Dtos.Kendo;
namespace RahyabServices.Business.Contracts
{
    public class GridRequestParameters
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string FilterLogic { get; set; }

        public IEnumerable<SortDto> Sortings { get; set; }
        public IEnumerable<FilterDto> Filters { get; set; }

        public static GridRequestParameters Current
        {
            get
            {
                var p = new GridRequestParameters();
                p.Populate();
                return p;
            }
        }

        //TODO: pull default values from config
        internal void Populate()
        {
           
              //  var url =
               //     "https://wcfrahyabwebservice.ab.net/VipBankingRestService.svc/json/GetAllVips/i--ZtMPqpfez2cbcEhnJxKhmDwCenSfUzzLI_CdzHGnL4C7jg1sXsruN5GN1HLzV/?skip=0&take=10&filter[logic]=and&filter[filters][0][field]=MeanTurnover&filter[filters][0][operator]=gt&filter[filters][0][value]=100000&filter[filters][1][field]=FullName&filter[filters][1][operator]=contains&filter[filters][1][value]=حسن";
                HttpRequest curRequest = HttpContext.Current.Request;
                //this.Page = curRequest["page"].Parse<int>();
                //this.PageSize = curRequest["pageSize"].Parse(Configuration.Settings.GridDefaults.PageSize);
                //this.Skip = curRequest["skip"].Parse(0);
                //this.Take = curRequest["take"].Parse(Configuration.Settings.GridDefaults.QuerySize);
                this.FilterLogic = curRequest["filter[logic]"] ?? "AND";

                //build sorting objects
                var sorts = new List<SortDto>();
                var x = 0;
                while (x < 20)
                {
                    var sortDirection = curRequest["sort[" + x + "][dir]"];
                    if (sortDirection == null)
                    {
                        break;
                    }
                    var sortOn = curRequest["sort[" + x + "][field]"];
                    if (sortOn != null)
                    {
                        sorts.Add(new SortDto { Field = sortOn, Dir = sortDirection });
                    }
                    x++;
                }
                Sortings = sorts;

                //build filter objects
                var filters = new List<FilterDto>();
                x = 0;
                while (x < 20)
                {
                    var field = curRequest["filter[filters][" + x + "][field]"];
                    if (field == null)
                    {
                        break;
                    }

                    var val = curRequest["filter[filters][" + x + "][value]"] ?? string.Empty;

                    var strop = curRequest["filter[filters][" + x + "][operator]"];
                    if (strop != null)
                    {
                        filters.Add(new FilterDto
                        {
                            Operator = strop,
                            Field = field,
                            Value = val,
                            Logic = FilterLogic

                        });
                    }
                    x++;
                
                Filters = filters;
            }
        }
    }
    public class FilterInfo
    {
        public string Field { get; set; }
        public FilterOperations Operator { get; set; }
        public string Value { get; set; }

        public static FilterOperations ParseOperator(string theOperator)
        {
            switch (theOperator)
            {
                //equal ==
                case "eq":
                case "==":
                case "isequalto":
                case "equals":
                case "equalto":
                case "equal":
                    return FilterOperations.Equals;
                //not equal !=
                case "neq":
                case "!=":
                case "isnotequalto":
                case "notequals":
                case "notequalto":
                case "notequal":
                case "ne":
                    return FilterOperations.NotEquals;
                // Greater
                case "gt":
                case ">":
                case "isgreaterthan":
                case "greaterthan":
                case "greater":
                    return FilterOperations.Greater;
                // Greater or equal
                case "gte":
                case ">=":
                case "isgreaterthanorequalto":
                case "greaterthanequal":
                case "ge":
                    return FilterOperations.GreaterOrEquals;
                // Less
                case "lt":
                case "<":
                case "islessthan":
                case "lessthan":
                case "less":
                    return FilterOperations.LessThan;
                // Less or equal
                case "lte":
                case "<=":
                case "islessthanorequalto":
                case "lessthanequal":
                case "le":
                    return FilterOperations.LessThanOrEquals;
                case "startswith":
                    return FilterOperations.StartsWith;

                case "endswith":
                    return FilterOperations.EndsWith;
                //string.Contains()
                case "contains":
                    return FilterOperations.Contains;
                case "doesnotcontain":
                    return FilterOperations.NotContains;
                default:
                    return FilterOperations.Contains;
            }
        }
    }
    public class SortingInfo
    {
        public string SortOrder { get; set; }
        public string SortOn { get; set; }
    }
    public enum FilterOperations
    {
        Equals,
        NotEquals,
        Greater,
        GreaterOrEquals,
        LessThan,
        LessThanOrEquals,
        StartsWith,
        EndsWith,
        Contains,
        NotContains,
    }
}