namespace RahyabServices.Common.Convertors
{
    public class VariableConventor : IVariableConventor
    {
        public double ConvertDoubleDecimal(decimal? decimalVal){
            return decimalVal.HasValue ? System.Convert.ToDouble(decimalVal) : 0;
        }
    }
}