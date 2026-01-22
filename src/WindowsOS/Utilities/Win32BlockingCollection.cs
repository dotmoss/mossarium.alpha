namespace WindowsOS.Utils;

public unsafe class Win32BlockingCollection<T>
{
    public Win32BlockingCollection() 
    {
        Handle = Kernel32.CreateEventA(default, false, false, null);
    }

    Queue<T> queue = new Queue<T>();

    public nint Handle { get; private set; }

    public void Add(T item)
    {
        lock (queue)
            queue.Enqueue(item);
        Kernel32.SetEvent(Handle);
    }

    public void Add(IEnumerable<T> items)
    {
        lock (queue)
        {
            foreach (var item in items)
                queue.Enqueue(item);
        }
    }

    public bool TryTake(out T? value)
    {
        if (queue.Count == 0)
        {
            value = default;
            return false;
        }

        lock (queue)
        {
            return queue.TryDequeue(out value);
        }
    }

    public T Take()
    {
        if (queue.Count != 0)
        {
            lock (queue)
            {
                var value = queue.Dequeue();
                return value;
            }
        }

        Kernel32.WaitForSingleObject(Handle, uint.MaxValue);
        lock (queue)
        {
            var value = queue.Dequeue();
            return value;
        }
    }
}