using System;
using System.Text.RegularExpressions;
namespace RahyabServices.Business.Services.Implementations.Bank{
    public enum AccountType{
        Sepordeh = 0,
        Tashilat = 1
    }
    public enum IbanStatusMessages{
        Success = 0,
        InvalidLength = 1,
        InvalidIranId = 2,
        InvalidIranIdMustBeUpperCh = 3,
        InvalidBankId = 4,
        InvalidAccountNumber = 5,
        InvalidControlDigit = 6
    }
    public class IbanConvertor{
        public static string BmiId = "062";
        public static string IranCode = "IR";
        public static string IranDigitCode = "1827";
        public static string ConvertAccountToIban(long accountNo, AccountType accType, int? branchId = null){
            if (accountNo.ToString().Length <= 10 && (branchId == null || branchId == 0)) throw new Exception("كد شعبه براي حسابهاي غير متمركز الزامي مي باشد");
            if (accountNo.ToString().Length == 11 || accountNo.ToString().Length > 13) throw new Exception("شماره حساب صحيح نمي باشد");
            if (accountNo.ToString().Length > 11 && branchId != null && branchId > 0) branchId = 0;
            if (accountNo.ToString().Length > 11 && !IsValidSibaCheckDigit(accountNo)) throw new Exception("شماره حساب  معتبر نمي باشد، رقم كنترلي  با شماره حساب متناسب نمي باشد");
            var d19 = GetFirstAccountDigit(accType, branchId);
            var d181 = GetAccountNoDigits(accountNo, branchId);
            var bban = BmiId + d19 + d181; //22 Digit
            var cd = GetControlDigit(bban); // 2 Digit
            var cc = IranCode; // 2 Digit
            var iban = cc + cd + bban; // 26 Digit
            return iban;
        }
        public static bool IsValidSibaCheckDigit(long accountNo){
            if (accountNo.ToString().Length < 12) throw new Exception("شماره حساب وارد شده از نوع سيبا نمي باشد");
            var sibaNo = $"{accountNo,13:0000000000000}";
            var sumNo = Convert.ToInt32(sibaNo[11].ToString())*5;
            sumNo += Convert.ToInt32(sibaNo[10].ToString())*7;
            sumNo += Convert.ToInt32(sibaNo[9].ToString())*13;
            sumNo += Convert.ToInt32(sibaNo[8].ToString())*17;
            sumNo += Convert.ToInt32(sibaNo[7].ToString())*19;
            sumNo += Convert.ToInt32(sibaNo[6].ToString())*23;
            sumNo += Convert.ToInt32(sibaNo[5].ToString())*29;
            sumNo += Convert.ToInt32(sibaNo[4].ToString())*31;
            sumNo += Convert.ToInt32(sibaNo[3].ToString())*37;
            sumNo += Convert.ToInt32(sibaNo[2].ToString())*41;
            sumNo += Convert.ToInt32(sibaNo[1].ToString())*43;
            sumNo += Convert.ToInt32(sibaNo[0].ToString())*47;
            var res = sumNo%11;
            if (res == 1) return false;
            var p = 11 - res;
            if (p == 11 && Convert.ToInt32(sibaNo[12].ToString()) != 0) // if p=11 then checkDigit is 0 
                return false;
            return p == 11 || p == Convert.ToInt32(sibaNo[12].ToString());
        }
        public static Account ConvertIbantoAccount(string iban){
            iban = iban.Replace(" ", "");
            var status = IsValidIbanFormat(iban);
            if (status != IbanStatusMessages.Success) throw new Exception(status.ToString());
            var bmiAcc = new Account();
            if (iban[7] == '0' || iban[7] == '2') // حساب متمركز
            {
                bmiAcc.BranchId = 0;
                bmiAcc.AccountNumber = Convert.ToInt64(iban.Substring(8));
                bmiAcc.AccountType = iban[7] == '0' ? AccountType.Sepordeh : AccountType.Tashilat;
            }
            else // حساب غير متمركز
            {
                bmiAcc.AccountType = iban[7] == '1' ? AccountType.Sepordeh : AccountType.Tashilat;
                bmiAcc.AccountNumber = Convert.ToInt64(iban.Substring(16));
                bmiAcc.BranchId = Convert.ToInt32(iban.Substring(8, 8));
            }
            return bmiAcc;
        }
        public static IbanStatusMessages IsValidIbanFormat(string iban){
            if (iban == null || iban.Length != 26) return IbanStatusMessages.InvalidLength;
            if (iban.Substring(0, 2) != IranCode &&
                string.Equals(iban.Substring(0, 2), IranCode, StringComparison.CurrentCultureIgnoreCase)) return IbanStatusMessages.InvalidIranIdMustBeUpperCh;
            if (iban.Substring(0, 2) != IranCode) return IbanStatusMessages.InvalidIranId;

            //if (IBAN.Substring(4, 3) != BMI_ID)
            //   return IBANStatusMessages.InvalidBankID;
            if (!IsDecimal(iban.Substring(2))) return IbanStatusMessages.InvalidAccountNumber;
            if (!IsValidControlDigit(iban)) return IbanStatusMessages.InvalidControlDigit;
            return IbanStatusMessages.Success;
        }
        public static string GetIbanPrintFormat(string iban){
            return iban.Substring(0, 4) + " " + iban.Substring(4, 4) + " " + iban.Substring(8, 4) + " " +
                   iban.Substring(12, 4) + " " + iban.Substring(16, 4) + " " + iban.Substring(20, 4) + " " +
                   iban.Substring(24);
        }
        private static char GetFirstAccountDigit(AccountType accType, int? branchId){
            var firstDigit = '0'; // متمركز و سپرده
            if (branchId == null || branchId.Value == 0) {
                if (accType == AccountType.Tashilat) firstDigit = '2'; //متمركز و تسهيلات
            }
            else{
                if (accType == AccountType.Sepordeh) firstDigit = '1'; // غير متمركز و سپرده
                else firstDigit = '3'; // غير متمركز و تسهيلات
            }
            return firstDigit;
        }
        private static string GetAccountNoDigits(long accountNo, int? branchId){
            var accountDigits = "";
            if (branchId == null || branchId == 0) // متمركز
            {
                accountDigits = $"{accountNo,18:000000000000000000}";
            }
            else // غير متمركز
            {
                var accountNoStr = $"{accountNo,10:0000000000}";
                var branchStr = $"{branchId,8:00000000}";
                accountDigits = branchStr + accountNoStr;
            }
            return accountDigits;
        }
        private static string GetControlDigit(string bban){
            //string middleCode = "IR"+"00"+BBAN;
            //string middleCode = BBAN +"IR"+"00";
            var middleCodeStr = bban + IranDigitCode + "00";
            var mCode = Convert.ToDecimal(middleCodeStr);
            var cd = Convert.ToInt32(98 - mCode%97);
            return $"{cd,2:00}";
        }
        private static bool IsValidControlDigit(string iban){
            //string mCode = IBAN.Substring(4) + "IR" + IBAN.Substring(2, 2);
            var mCode = Convert.ToDecimal(
                iban.Substring(4) + IranDigitCode + iban.Substring(2, 2));
            var cd = Convert.ToInt32(mCode%97);
            return cd == 1;
        }
        private static bool IsDecimal(string numStr){
            var rx = new Regex(@"^\d*$");
            return rx.IsMatch(numStr.Trim()) && Convert.ToDecimal(numStr) > 0;
        }
    }
    public class Account{
        public AccountType AccountType { get; set; } = AccountType.Sepordeh;
        public long AccountNumber { get; set; }
        public int BranchId { get; set; }
    }
}