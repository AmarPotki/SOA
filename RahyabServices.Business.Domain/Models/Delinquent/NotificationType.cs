namespace RahyabServices.Business.Domain.Models.Delinquent
{
    public enum NotificationType{
        Call,
        Appointment,
        RejectRequestSplit,
        ApproveRequestSplit,
        ApproveRequestGivingAChance,
        RejectRequestGivingAChance,
        TenDaysLeft,
        StartLegalAction,
        CheckProcess,
        ApproveRequestClearing,
        RejectRequestClearing,
        ApproveRequestImpunityForCrimes,
        RejectRequestImpunityForCrimes
    }
}