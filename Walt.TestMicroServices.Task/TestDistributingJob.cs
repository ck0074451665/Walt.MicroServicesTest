using System;
using System.Threading;
using System.Threading.Tasks;
using Walt.Framework.Quartz;

namespace Walt.TestMicroServices.Task
{

    [Quartz.PersistJobDataAfterExecution]
    [Quartz.DisallowConcurrentExecution]
    [DistributingAttributes()]
    public class TestDistributingJob : Quartz.IJob
    {
        public  virtual async  System.Threading.Tasks.Task Execute(Quartz.IJobExecutionContext context)
        {
            if(context.CancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("取消执行。");
                return ;
            }
            try
            {
                DistributingData disriObj = 
                context.JobDetail.JobDataMap.Get("distriData") as DistributingData;
                
                Console.WriteLine(@"%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%"+disriObj.DistributeFlag+disriObj.PageIndex.ToString());
            }
            catch(Exception ep)
            {
                Quartz.JobExecutionException jobep=new Quartz.JobExecutionException();
                //jobep.RefireImmediately=false;
                throw jobep;
            }
        }
    }

}