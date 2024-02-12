using EmissorDePdf.Email;
using Hangfire;

namespace EmissorDePdf.API.HangfireJobs
{
    public static class JobsDoHangFire
    {
        public static void AddJobsDoHangfire(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<IEmailSender>("Enviando-email-com-pdf", (e) => e.SendEmailAsync(), Cron.Minutely);
        }
    }
}
