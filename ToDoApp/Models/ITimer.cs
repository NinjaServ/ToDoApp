using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ToDoApp.Models
{
    public interface ITimer
    {
        event EventHandler Tick;

        void Start();

        void Stop();
    }

    public class DispatcherTimerWrapper : ITimer
    {
        private readonly DispatcherTimer timer;

        public DispatcherTimerWrapper(TimeSpan interval)
        {
            this.timer = new DispatcherTimer { Interval = interval };
        }

        public event EventHandler Tick
        {
            add { this.timer.Tick += value; }

            remove { this.timer.Tick -= value; }
        }

        public void Start()
        {
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }
    }
}
