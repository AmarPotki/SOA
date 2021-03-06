﻿using RahyabServices.Business.Domain.Models.Sharepoint;
using RahyabServices.DataAccess.Core;
using RahyabServices.DataAccess.Core.Sharepoint;
using RahyabServices.DataAccess.Repositories.Sharepoint.Interfaces;
namespace RahyabServices.DataAccess.Repositories.Sharepoint.Implementations{
    public class RenewalLogListRepository : SharepointRepositoryBase<RenewalLogList>, IRenewalLogListRepository{
        public RenewalLogListRepository(IDataContextFactory databaseFactory) : base(databaseFactory){}
    }
}