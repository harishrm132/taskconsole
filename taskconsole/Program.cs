using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace taskconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the service on the remote machine
            using (TaskService ts = new TaskService())
            {
                //Find task
                String taskname = "Windows_Time";
                Microsoft.Win32.TaskScheduler.Task t = ts.FindTask(taskname);

                if (t == null)
                {
                    // Create a new task definition and assign properties
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Does something";

                    // Add a trigger that, starting tomorrow, will fire every other week on Monday
                    // and Saturday and repeat every 10 minutes for the following 11 hours
                    WeeklyTrigger wt = new WeeklyTrigger();
                    wt.StartBoundary = DateTime.Today + TimeSpan.FromHours(12);
                    //wt.StartBoundary = DateTime.Today.AddDays(1);
                    wt.DaysOfWeek = DaysOfTheWeek.Monday | DaysOfTheWeek.Tuesday;
                    wt.WeeksInterval = 1;
                    //wt.Repetition.Duration = TimeSpan.FromHours(11);
                    //wt.Repetition.Interval = TimeSpan.FromMinutes(10);
                    td.Triggers.Add(wt);

                    // Create an action that will launch Notepad whenever the trigger fires
                    string filepath = "C:\\Users\\SIP011497\\Desktop\\02-Work load Management\\Repos\\TimekeeperTest\\bin\\Debug\\Timekeeper.exe";
                    td.Actions.Add(new ExecAction(filepath));

                    // Register the task in the root folder
                    ts.RootFolder.RegisterTaskDefinition(taskname, td);
                }
                else { }
            }

        }


        //using (TaskService sched = new TaskService())
        //{
        //    var task = sched.FindTask("UniqueTaskName", true);

        //    if (task != null)
        //    {
        //        ...
        //    }
        //}
    }
}
