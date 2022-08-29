using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainofResponsibility
{
    internal class ChainofResponsibilityClient
    {
        static void Main(String[] args)
        {
            ILeaveRequestHandler supervisorx = new Supervisor();

            ILeaveRequestHandler projectManagerx = new ProjectManager();
            supervisorx.SetNextHandler(projectManagerx);

            ILeaveRequestHandler hrx = new HR();
            projectManagerx.SetNextHandler(hrx);

            LeaveRequest p = new LeaveRequest("SUMAN", 5);
            supervisorx.HandleRequest(p);
            p = new LeaveRequest( "PREETI", 2);
            supervisorx.HandleRequest(p);
            p = new LeaveRequest("DISHA", 10);
            supervisorx.HandleRequest(p);


        }

    }
    public abstract class ILeaveRequestHandler
    {
        protected ILeaveRequestHandler nextHandler;
        public void SetNextHandler(ILeaveRequestHandler nextHandler)
        {
            this.nextHandler = nextHandler;
        }
        public abstract void HandleRequest(LeaveRequest leave);
    }
    public class Supervisor : ILeaveRequestHandler
    {
        public override void HandleRequest(LeaveRequest leave)
        {
            if (leave.LeaveDays <= 3)
            {
                Console.WriteLine("{0} approved request# {1}Days By Supervisor",
                    leave.Employee, leave.LeaveDays);
            }
            else if (nextHandler != null)
            {
               nextHandler.HandleRequest(leave);
            }
        }
    }
    public class ProjectManager : ILeaveRequestHandler
    {
        public override void HandleRequest(LeaveRequest leave)
        {
            if (leave.LeaveDays <= 5)
            {
                Console.WriteLine("{0} approved request# {1}Days By ProjectManager",
                    leave.Employee, leave.LeaveDays);
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(leave);
            }
        }
    }
    public class HR : ILeaveRequestHandler
    {
       public override void HandleRequest(LeaveRequest leave)
        {
            if (leave.LeaveDays > 5)
            {
                Console.WriteLine("{0} approved request# {1}Days by HR",
                    leave.Employee, leave.LeaveDays);
            }
            else
            {
                Console.WriteLine(
                    "Request# {0} is Rejected",
                    leave.LeaveDays);
            }

        }
    }
    public class LeaveRequest

    {
        string employee;
        int leaveDays;
        public LeaveRequest(string employee, int leaveDays)
        {
            this.employee = employee;
            this.leaveDays = leaveDays;
        }
        public string Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }
        public int LeaveDays
        {
            get
            {
                return leaveDays;
            }
            set
            {
                leaveDays = value;
            }



        }
    }
}



