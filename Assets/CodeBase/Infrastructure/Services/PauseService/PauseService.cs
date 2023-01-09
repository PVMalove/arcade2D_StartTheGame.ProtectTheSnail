using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.PauseService
{
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> _handlers = new();

        public void Register(IPauseHandler pauseHandler) => 
            _handlers.Add(pauseHandler);

        public void Unregister(IPauseHandler pauseHandler) => 
            _handlers.Remove(pauseHandler);

        public void SetPause(bool isPaused)
        {
            foreach (IPauseHandler handler in _handlers) 
                handler.OnPauseChanged(isPaused);
        }
    }
}