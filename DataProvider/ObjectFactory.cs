using System;

namespace WpfApp.DataProvider
{
	/// <summary>
	/// Фабрика объектов
	/// </summary>
	public class ObjectFactory
	{
		private static readonly Lazy<ObjectFactory> InstanceLazy = new Lazy<ObjectFactory>(() => new ObjectFactory());

		private Func<Type, object> _factory;

		private bool _isInitialized;

		/// <summary>
		/// Экземпляр-одиночка фабрики объектов
		/// </summary>
		public static ObjectFactory Instance => InstanceLazy.Value;

		private ObjectFactory()
		{
		}

		/// <summary>
		/// Инициализировать фабрику объектов
		/// </summary>
		/// <param name="factory">Фабричный метод</param>
		public void Initialize(Func<Type, object> factory)
		{
			_factory = factory;
			_isInitialized = true;
		}

		/// <summary>
		/// Получить объект
		/// </summary>
		/// <typeparam name="T">Тип объекта</typeparam>
		/// <returns>Объект</returns>
		public T GetObject<T>() where T : class
		{
			return _isInitialized ? (T) _factory(typeof(T)) : null;
		}
	}
}