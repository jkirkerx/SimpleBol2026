using Microsoft.Extensions.DependencyInjection;

namespace SimpleBol.Classes.DI
{
    /// <summary>
    /// Owns both a WinForms instance and the dependency-injection scope that
    /// created it. Disposing this object releases the form and every scoped or
    /// disposable transient dependency created for that form.
    /// </summary>
    internal sealed class OwnedForm<TForm> : IDisposable where TForm : Form
    {
        private readonly IServiceScope scope;
        private bool disposed;

        internal OwnedForm(IServiceScope scope, TForm form)
        {
            this.scope = scope;
            Form = form;
        }

        public TForm Form { get; }

        public void Show()
        {
            Form.FormClosed += Form_FormClosed;
            Form.Show();
        }

        private void Form_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
            Form.FormClosed -= Form_FormClosed;

            if (!Form.IsDisposed)
            {
                Form.Dispose();
            }

            scope.Dispose();
        }
    }

    internal static class FormServiceProviderExtensions
    {
        public static OwnedForm<TForm> CreateOwnedForm<TForm>(this IServiceScopeFactory scopeFactory)
            where TForm : Form
        {
            var scope = scopeFactory.CreateScope();

            return CreateOwnedForm<TForm>(scope);
        }

        private static OwnedForm<TForm> CreateOwnedForm<TForm>(IServiceScope scope)
            where TForm : Form
        {

            try
            {
                return new OwnedForm<TForm>(scope, scope.ServiceProvider.GetRequiredService<TForm>());
            }
            catch
            {
                scope.Dispose();
                throw;
            }
        }
    }
}
