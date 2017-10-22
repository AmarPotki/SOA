using System.Collections.Generic;
using System.Linq;
namespace RahyabServices.Business.Dtos.Kendo{
    public class FilterDto
    {

        /// <summary>
        ///     Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        ///     Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        ///     Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        ///     Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
        /// </summary>
        public string Logic { get; set; }
    }
}