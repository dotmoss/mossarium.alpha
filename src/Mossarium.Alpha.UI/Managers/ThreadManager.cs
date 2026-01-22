using WindowsOS;
using WindowsOS.Utils;
using CancelableAction = (System.Action<System.Threading.CancellationToken> Action, System.Threading.CancellationToken Token);

namespace Mossarium.Alpha.UI.Managers;

public unsafe static class ThreadManager
{
    public static readonly Win32BlockingCollection<Action> UIJobs = new Win32BlockingCollection<Action>();
    public static readonly Win32BlockingCollection<Action> ConsumerJobs = new Win32BlockingCollection<Action>();
    public static readonly Win32BlockingCollection<CancelableAction> ConsumerCancelableJobs = new Win32BlockingCollection<CancelableAction>();

    static ThreadManager()
    {
        StartNew("Consumer Thread", ConsumerThreadBody, 256);
    }

    public static void StartNew(string name, Action entryPoint, uint stackKbSize)
    {
        var start = new ThreadStart(entryPoint);
        var stackSize = (int)((1 << 10) * stackKbSize);
        var thread = new Thread(start, stackSize)
        {
            Name = name
        };

        thread.Start();
    }

    static void ConsumerThreadBody()
    {
        var handles = stackalloc nint[2];

        const int ConsumerJobsId = 0;
        handles[0] = ConsumerJobs.Handle;

        const int ConsumerCancelableJobsId = 1;
        handles[1] = ConsumerCancelableJobs.Handle;

        while (true)
        {
            var result = Kernel32.WaitForMultipleObjectsEx(2, handles, false, uint.MaxValue, true);

            if (result == ConsumerJobsId)
            {
                while (ConsumerJobs.TryTake(out var job))
                {
                    job!.Invoke();
                }
            }
            else if (result == ConsumerCancelableJobsId)
            {
                while (ConsumerCancelableJobs.TryTake(out var job))
                {
                    var (action, token) = job;
                    action.Invoke(token);
                }
            }
        }
    }
}