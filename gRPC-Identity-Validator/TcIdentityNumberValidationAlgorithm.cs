using System;
using System.Linq;

namespace gRPC_Identity_Validator
{
    public static class TcIdentityNumberValidationAlgorithm
    {
        public static IdentityValidationResult Validate(string tcIdentityNumber)
        {
            if (string.IsNullOrWhiteSpace(tcIdentityNumber) || tcIdentityNumber.Length != 11)
                return new IdentityValidationResult()
                {
                    IsSuccess = false,
                    Message = "Geçersiz Girdi"
                };

            if (tcIdentityNumber.Any(t => !Char.IsDigit(t)))
                return new IdentityValidationResult()
                {
                    Message = "Hatalı Girdi",
                    IsSuccess = false
                };


            Int64 ATCNO, BTCNO, TcNo;
            long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;

            TcNo = Int64.Parse(tcIdentityNumber);

            ATCNO = TcNo / 100;
            BTCNO = TcNo / 100;

            C1 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C2 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C3 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C4 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C5 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C6 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C7 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C8 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            C9 = ATCNO % 10;
            ATCNO = ATCNO / 10;
            Q1 = (10 - ((C1 + C3 + C5 + C7 + C9) * 3 + C2 + C4 + C6 + C8) % 10) % 10;
            Q2 = (10 - ((C2 + C4 + C6 + C8 + Q1) * 3 + C1 + C3 + C5 + C7 + C9) % 10) % 10;

            var validationResult = (BTCNO * 100) + Q1 * 10 + Q2 == TcNo;

            return new IdentityValidationResult()
            {
                Message = validationResult ? "Doğrulama Başarılı!" : "Doğrulama Başarısız",
                IsSuccess = validationResult
            };
        }
    }
}