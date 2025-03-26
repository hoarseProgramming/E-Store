namespace EStore.Application.Events
{
    public static class CartPublisher
    {
        public static event Action CartStateChanged;

        public static async Task OnCartStateChanged()
        {
            CartStateChanged?.Invoke();
        }
    }
}
