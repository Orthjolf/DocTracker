using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace WpfApp.Extensions
{
	public static class EnumExtension
	{
		/// <summary>
		/// Возвращает описание значения енума, заданного аттрибутом Description
		/// </summary>
		/// <param name="value">Значение енума</param>
		/// <returns>Описание</returns>
		public static string GetDescription(this System.Enum value)
		{
			return value.GetType()
				.GetMember(value.ToString())
				.FirstOrDefault()
				?.GetCustomAttribute<DescriptionAttribute>()
				?.Description;
		}
	}
}