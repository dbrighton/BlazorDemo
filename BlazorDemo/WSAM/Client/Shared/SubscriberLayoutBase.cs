using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Client.Shared;

public class SubscriberLayoutBase : LayoutComponentBase, IDisposable
{
    public bool Disposed { get; set; }

    [Inject] public IActionSubscriber ActionSubscriber { get; set; }

    public void Dispose()
    {
        Disposed = true;
    }

    public void SubscribeToAction<TAction>(Action<TAction> callback)
    {
        ActionSubscriber.SubscribeToAction(this,
            (Action<TAction>)(action =>
            {
                if (Disposed) return;

                callback(action);
            }));
    }
}