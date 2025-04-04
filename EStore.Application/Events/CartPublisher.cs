namespace EStore.Application.Events
{
    public static class CartPublisher
    {
        public static event Action CartStateChanged;
        public static event Action MainLayoutCartChanged;

        public static async Task OnCartStateChanged()
        {
            CartStateChanged?.Invoke();
        }
        public static async Task OnMainLayoutCartChanged()
        {
            MainLayoutCartChanged?.Invoke();
        }
    }
}
