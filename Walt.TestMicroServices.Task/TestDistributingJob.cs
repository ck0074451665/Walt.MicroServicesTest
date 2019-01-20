using System;
using System.Threading;
using System.Threading.Tasks;
using Walt.Framework.Quartz;

namespace Walt.TestMicroServices.Task
{

    [Quartz.PersistJobDataAfterExecution]
    [Quartz.DisallowConcurrentExecution]
    [DistributingAttributes(0)]
    public class TestDistributingJob : Quartz.IJob
    {
        public  virtual async  System.Threading.Tasks.Task Execute(Quartz.IJobExecutionContext context)
        {
            try
            {
            //int currentIndex=context.JobDetail.JobDataMap.GetInt("currentIndex");
            Console.WriteLine(@"%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            System.Threading.Thread.Sleep(5000);
            }
            catch(Exception ep)
            {
                Quartz.JobExecutionException jobep=new Quartz.JobExecutionException();
                jobep.RefireImmediately=false;
                throw jobep;
            }
        }
    }

}