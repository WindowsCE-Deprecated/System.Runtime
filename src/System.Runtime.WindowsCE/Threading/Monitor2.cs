using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading
{
    public static class Monitor2
    {
        private static readonly Dictionary<object, List<AutoResetEvent>> _waiters
            = new Dictionary<object, List<AutoResetEvent>>();

        public static void Enter(object obj)
            => Monitor.Enter(obj);

        public static void Enter(object obj, ref bool lockTaken)
        {
            if (lockTaken)
                ThrowLockTakenException();

            try
            {
                Monitor.Enter(obj);
                lockTaken = true;
            }
            catch
            {
                lockTaken = false;
                throw;
            }
        }

        public static void Exit(object obj)
            => Monitor.Exit(obj);

        [Obsolete("Platform not supported")]
        public static bool IsEntered(object obj)
        {
            throw new PlatformNotSupportedException();
        }

        public static void Pulse(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            lock (_waiters)
            {
                if (!_waiters.ContainsKey(obj))
                    return;

                var queue = _waiters[obj];
                if (queue.Count > 0)
                    queue[0].Set();
            }

            Thread.Sleep(0);
        }

        public static void PulseAll(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            int counter = 0;
            lock (_waiters)
            {
                if (!_waiters.ContainsKey(obj))
                    return;

                var queue = _waiters[obj];
                counter = queue.Count;
                queue.ForEach(a => a.Set());
            }

            for (int i = 0; i < counter; i++)
                Thread.Sleep(0);
        }

        public static bool TryEnter(object obj)
            => Monitor.TryEnter(obj);

        public static void TryEnter(object obj, ref bool lockTaken)
        {
            if (lockTaken)
                ThrowLockTakenException();

            lockTaken = Monitor.TryEnter(obj);
        }

        public static bool TryEnter(object obj, int millisecondsTimeout)
        {
            bool lockTaken = false;
            TryEnter(obj, millisecondsTimeout, ref lockTaken);
            return lockTaken;
        }

        public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
        {
            if (lockTaken)
                ThrowLockTakenException();

            if (millisecondsTimeout == Timeout.Infinite)
            {
                Enter(obj, ref lockTaken);
                return;
            }

            var timeTrack = new Stopwatch();
            int growThreshold = 250;
            int timeoutMs = 1;
            timeTrack.Start();
            while (timeTrack.ElapsedMilliseconds < millisecondsTimeout)
            {
                if (Monitor.TryEnter(obj))
                {
                    lockTaken = true;
                    timeTrack.Stop();
                    return;
                }

                if (timeTrack.ElapsedMilliseconds >= growThreshold)
                {
                    growThreshold += 250;
                    timeoutMs *= 2;
                }

                Thread.Sleep(timeoutMs);
            }
        }

        public static bool TryEnter(object obj, TimeSpan timeout)
            => TryEnter(obj, MillisecondsTimeoutFromTimeSpan(timeout));

        public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
            => TryEnter(obj, MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);

        public static bool Wait(object obj)
            => Wait(obj, Timeout.Infinite);

        public static bool Wait(object obj, int millisecondsTimeout)
        {
            bool objUnlocked = false;
            bool waitersLocked = false;
            AutoResetEvent waitHandle = null;
            bool enqueued = false;
            bool gotSignal = false;
            try
            {
                Monitor.Exit(obj);
                objUnlocked = true;

                // Enqueue a new AutoResetEvent
                Monitor.Enter(_waiters);
                waitersLocked = true;

                List<AutoResetEvent> queue;
                if (_waiters.ContainsKey(obj))
                    queue = _waiters[obj];
                else
                {
                    queue = new List<AutoResetEvent>();
                    _waiters.Add(obj, queue);
                }

                waitHandle = new AutoResetEvent(false);
                queue.Add(waitHandle);
                enqueued = true;

                Monitor.Exit(_waiters);
                waitersLocked = false;
                // ----------------------------

                gotSignal = waitHandle.WaitOne(millisecondsTimeout, false);
            }
            finally
            {
                if (enqueued)
                {
                    if (!waitersLocked)
                    {
                        Monitor.Enter(_waiters);
                        waitersLocked = true;
                    }

                    List<AutoResetEvent> queue;
                    if (_waiters.TryGetValue(obj, out queue))
                        queue.Remove(waitHandle);
                }

                if (waitersLocked)
                    Monitor.Exit(_waiters);

                if (objUnlocked)
                    Monitor.Enter(obj);
            }

            return gotSignal;
        }

        public static bool Wait(object obj, TimeSpan timeout)
            => Wait(obj, MillisecondsTimeoutFromTimeSpan(timeout));


        private static void ThrowLockTakenException()
        {
            throw new ArgumentException("The lock is taken already", "lockTaken");
        }

        private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
        {
            long tm = (long)timeout.TotalMilliseconds;
            if (tm < -1 || tm > (long)int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(timeout));

            return (int)tm;
        }
    }
}

namespace Test
{
    using System;
    using System.Threading;

#if NET45
    public class Lock4
    {

        public object run(Closure4 closure)
        {
            lock (this)
            {
                return closure.run();
            }
        }

        public void snooze(long timeout)
        {
            Monitor.Wait(this, (int)timeout);
        }

        public void awake()
        {
            Monitor.Pulse(this);
        }
    }
#endif

    // Ref: https://www.pcreview.co.uk/threads/best-replacement-for-wait-notify-monitor-wait-monitor-pulse-on-the-compactframework.1299375/#post-3880240
    public class Lock4
    {
        private volatile Thread _lockedByThread;

        private volatile Thread _waitReleased;
        private volatile Thread _closureReleased;

        AutoResetEvent _waitEvent = new AutoResetEvent(false);
        AutoResetEvent _closureEvent = new AutoResetEvent(false);
        object _threadSafeObj = new object();

        public object run(Func<object> closure4)
        {
            EnterClosure();
            object ret;
            try { ret = closure4(); }
            finally { AwakeClosure(); }

            return ret;
        }

        public void Wait(long l)
        {
            AwakeClosure();
            waitWait();
            EnterClosure();
        }

        public void Pulse()
        {
            SendSignal();
        }

        private void SendSignal()
        {
            lock (_threadSafeObj)
            {
                _waitReleased = Thread.CurrentThread;
                _waitEvent.Set();
                Thread.Sleep(0);

                if (_waitReleased == Thread.CurrentThread)
                    _waitEvent.Reset();
            }
        }

        private void AwakeClosure()
        {
            lock (_threadSafeObj)
            {
                RemoveLock();
                _closureReleased = Thread.CurrentThread;
                _closureEvent.Set();
                Thread.Sleep(0);

                if (_closureReleased == Thread.CurrentThread)
                    _closureEvent.Reset();
            }
        }

        private void waitWait()
        {
            _waitEvent.WaitOne();
            _waitReleased = Thread.CurrentThread;
        }

        private void WaitClosureSignal()
        {
            _closureEvent.WaitOne();
            _closureReleased = Thread.CurrentThread;
        }

        private void EnterClosure()
        {
            while (_lockedByThread != Thread.CurrentThread)
            {
                while (!SetLock())
                {
                    WaitClosureSignal();
                }
            }
        }

        private bool SetLock()
        {
            lock (_threadSafeObj)
            {
                if (_lockedByThread == null)
                {
                    _lockedByThread = Thread.CurrentThread;
                    return true;
                }

                return false;
            }
        }

        private void RemoveLock()
        {
            lock (_threadSafeObj)
            {
                if (_lockedByThread == Thread.CurrentThread)
                    _lockedByThread = null;
            }
        }
    }
}