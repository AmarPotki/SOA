﻿using System.Collections;
namespace RahyabServices.Business.Domain.Kendo{
    /// <summary>
    ///     Describes the result of Kendo DataSource read operation.
    /// </summary>
    public class DataSourceResult{
        /// <summary>
        ///     Represents a single page of processed data.
        /// </summary>
        public IEnumerable Data { get; set; }
        /// <summary>
        ///     The total number of records available.
        /// </summary>
        public int Total { get; set; }
    }
}