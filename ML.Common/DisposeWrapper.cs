using System;
using System.ServiceModel;

namespace ML.Common
{
	/// <summary>
	/// Wrapper class for an object that implements IDisposable
	/// </summary>
	/// <typeparam name="T">the object that implements IDisposable</typeparam>
	/// <remarks>Implementation of the IDisposable pattern: http://msdn.microsoft.com/en-us/library/b1yfkh5e.aspx </remarks>
	public class DisposeWrapper<T> : IDisposable
	{
		private readonly T _tvalue;
		private bool _disposed = false;

		public T Value
		{
			get { return _tvalue; }
		}

		public DisposeWrapper(T tvalue)
		{
			_tvalue = tvalue;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~DisposeWrapper()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_tvalue is IClientChannel clientChannel)
                {
                    if (clientChannel.State == CommunicationState.Faulted)
                    {
                        clientChannel.Abort();
                    }
                    else
                    {
                        clientChannel.Close();
                    }
                }

                if (_tvalue is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _disposed = true;
        }
	}

	public static partial class std
    {
		public static DisposeWrapper<T> NewDispose<T>(T value) => new DisposeWrapper<T>(value);
	}
}
