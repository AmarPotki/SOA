using FluentScheduler;
using TaskScheduler.Job;
namespace TaskScheduler{
    public class SchedulerRegister : Registry{
        public SchedulerRegister(){
            Schedule<SayadRetryJob>().ToRunNow().AndEvery(5).Minutes();
            Schedule<SayadStateJob>().ToRunNow().AndEvery(30).Minutes();
           //i Schedule<IranNaraJob>().ToRunNow().AndEvery(21).Minutes();
            Schedule<UpdateActiveDirectoryUserInfo>().ToRunEvery(1).Days().At(20, 15);
            Schedule<HrDocPermissionJob>().ToRunNow().AndEvery(30).Minutes();
            Schedule<BranchMarketingJob>().ToRunEvery(1).Days().At(7, 15);
            Schedule<BranchMarketingCheckItemJob>().ToRunEvery(1).Days().At(10, 0);

        }
    }
}