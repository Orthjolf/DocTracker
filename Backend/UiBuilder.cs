using System;
using System.CodeDom;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp.DbMock;

namespace WpfApp.Backend
{
	public class UiBuilder
	{
		public StackPanel BuildMainPanel()
		{
			var label = new Label
			{
				Content = "Хранилища",
				Width = UiConstants.SideBarWidth,
				HorizontalAlignment = HorizontalAlignment.Left,
				FontSize = 28,
				Background = new SolidColorBrush(UiConstants.UiPrimaryColor)
			};

			var listBox = new ListBox
			{
				Name = "Storages",
				Width = UiConstants.SideBarWidth,
				Height = UiConstants.MainWindowSize.Height - 100,
				HorizontalAlignment = HorizontalAlignment.Left,
				BorderBrush=new SolidColorBrush(Colors.Gray),
				BorderThickness = new Thickness(1)
			};
			foreach (var storage in StoragesRepository.Instance().StoragesList)
			{
				listBox.Items.Add(storage.Id + " " + storage.Name);
			}

			var addButton = new Button
			{
				Content = "Добавить",
				Width = UiConstants.SideBarWidth / 2
			};

			var removeButton = new Button
			{
				Content = "Удалить",
				Width = UiConstants.SideBarWidth / 2
			};

			return new StackPanel
			{
				Name = "MainLayout",
				Children =
				{
					label,
					listBox,
					addButton,
					removeButton
				}
			};
		}

		private StackPanel BuildStoragePanel()
		{
			throw new NotImplementedException();
		}

		private StackPanel BuildBoxPanel()
		{
			throw new NotImplementedException();
		}
	}
}