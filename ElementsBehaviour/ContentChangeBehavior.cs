using System.ComponentModel;

namespace MauiApp1.ElementsBehaviour;

internal class ContentChangeBehavior : Behavior<ContentView>
{
    public event EventHandler ContentChanged;

    protected override void OnAttachedTo(ContentView bindable)
    {
        bindable.PropertyChanged += OnContentViewPropertyChanged;
        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(ContentView bindable)
    {
        bindable.PropertyChanged -= OnContentViewPropertyChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnContentViewPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ContentView.Content))
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
